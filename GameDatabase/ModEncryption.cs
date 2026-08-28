using System;
using System.IO;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace GameDatabase
{
    internal static class ModEncryption
    {
        internal const uint Header = 0xDADABA5E;
        internal const int FormatVersion = 1;
        internal const int WrappedKeySize = 384;
        internal const int NonceSize = 12;
        internal const int AuthenticationTagSize = 16;

        internal static byte[] Encrypt(byte[] plainText)
        {
            if (plainText == null)
                throw new ArgumentNullException(nameof(plainText));

            var aesKey = new byte[32];
            var nonce = new byte[NonceSize];
            byte[] wrappedKey = null;
            byte[] associatedData = null;
            byte[] cipherOutput = null;
            byte[] publicKeyData = null;

            try
            {
                var random = new SecureRandom();
                random.NextBytes(aesKey);
                random.NextBytes(nonce);

                publicKeyData = Convert.FromBase64String(ModPublicKey.SubjectPublicKeyInfoBase64);
                var publicKey = PublicKeyFactory.CreateKey(publicKeyData);
                var rsa = new OaepEncoding(
                    new RsaBlindedEngine(),
                    new Sha256Digest(),
                    new Sha256Digest(),
                    null);
                rsa.Init(true, publicKey);
                wrappedKey = rsa.ProcessBlock(aesKey, 0, aesKey.Length);
                if (wrappedKey.Length != WrappedKeySize)
                    throw new InvalidDataException("Unexpected wrapped mod key length.");

                associatedData = CreateAssociatedData(wrappedKey, nonce);
                var gcm = new GcmBlockCipher(new AesEngine());
                gcm.Init(true, new AeadParameters(
                    new KeyParameter(aesKey),
                    AuthenticationTagSize * 8,
                    nonce,
                    associatedData));

                cipherOutput = new byte[gcm.GetOutputSize(plainText.Length)];
                var outputLength = gcm.ProcessBytes(plainText, 0, plainText.Length, cipherOutput, 0);
                outputLength += gcm.DoFinal(cipherOutput, outputLength);
                if (outputLength != plainText.Length + AuthenticationTagSize)
                    throw new InvalidDataException("Unexpected encrypted mod length.");

                var encryptedFile = new byte[
                    sizeof(uint) + sizeof(int) + WrappedKeySize + NonceSize + AuthenticationTagSize + plainText.Length];
                var offset = 0;
                WriteUInt32(encryptedFile, ref offset, Header);
                WriteInt32(encryptedFile, ref offset, FormatVersion);
                Copy(wrappedKey, encryptedFile, ref offset);
                Copy(nonce, encryptedFile, ref offset);
                Buffer.BlockCopy(cipherOutput, plainText.Length, encryptedFile, offset, AuthenticationTagSize);
                offset += AuthenticationTagSize;
                Buffer.BlockCopy(cipherOutput, 0, encryptedFile, offset, plainText.Length);
                return encryptedFile;
            }
            finally
            {
                Clear(aesKey);
                Clear(nonce);
                Clear(wrappedKey);
                Clear(associatedData);
                Clear(cipherOutput);
                Clear(publicKeyData);
            }
        }

        private static byte[] CreateAssociatedData(byte[] wrappedKey, byte[] nonce)
        {
            var data = new byte[sizeof(uint) + sizeof(int) + wrappedKey.Length + nonce.Length];
            var offset = 0;
            WriteUInt32(data, ref offset, Header);
            WriteInt32(data, ref offset, FormatVersion);
            Copy(wrappedKey, data, ref offset);
            Copy(nonce, data, ref offset);
            return data;
        }

        private static void WriteUInt32(byte[] destination, ref int offset, uint value)
        {
            destination[offset++] = (byte)value;
            destination[offset++] = (byte)(value >> 8);
            destination[offset++] = (byte)(value >> 16);
            destination[offset++] = (byte)(value >> 24);
        }

        private static void WriteInt32(byte[] destination, ref int offset, int value)
        {
            WriteUInt32(destination, ref offset, unchecked((uint)value));
        }

        private static void Copy(byte[] source, byte[] destination, ref int offset)
        {
            Buffer.BlockCopy(source, 0, destination, offset, source.Length);
            offset += source.Length;
        }

        private static void Clear(byte[] data)
        {
            if (data != null)
                Array.Clear(data, 0, data.Length);
        }
    }
}
