using System;
using System.IO;
using System.Linq;
using EditorDatabase.Model;

namespace EditorDatabase.Storage
{
    public class DatabaseStorage : IDataStorage
    {
        public DatabaseStorage(string path)
        {
            var info = new DirectoryInfo(path);
            Name = info.Name;
            Id = info.Name;

            var modInfo = info.GetFiles("mod", SearchOption.AllDirectories).FirstOrDefault();
            if (modInfo != null)
            {
                var data = File.ReadAllText(modInfo.FullName).Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                if (data.Length == 2 && data[1].IndexOfAny(Path.GetInvalidFileNameChars()) == -1)
                {
                    Name = data[0];
                    Id = data[1];
                }
            }

            _path = path;
        }

        public void LoadContent(IContentLoader loader)
        {
            var info = new DirectoryInfo(_path);
            var itemCount = 0;
            foreach (var fileInfo in info.GetFiles("*", SearchOption.AllDirectories))
            {
                var file = fileInfo.FullName;
                var path = file.Substring(info.FullName.Length + 1);

                if (fileInfo.Extension.Equals(".png", StringComparison.OrdinalIgnoreCase) ||
                    fileInfo.Extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase) ||
                    fileInfo.Extension.Equals(".jpeg", StringComparison.OrdinalIgnoreCase))
                {
                    loader.LoadImage(new ImageData(file));
                }
                else if (fileInfo.Extension.Equals(".wav", StringComparison.OrdinalIgnoreCase))
                {
                    // TODO
                }
                else if (fileInfo.Extension.Equals(".xml", StringComparison.OrdinalIgnoreCase))
                {
                    var xmlData = File.ReadAllText(file);
                    loader.LoadLocalization(Path.GetFileNameWithoutExtension(file), xmlData);
                }
                else if (fileInfo.Extension.Equals(".json", StringComparison.OrdinalIgnoreCase))
                {
                    var data = File.ReadAllText(file);
                    loader.LoadJson(path, data);
                    itemCount++;
                }
            }

            if (itemCount == 0)
                throw new FileNotFoundException("No database files found in " + _path);
        }

        public void SaveJson(string name, string data)
        {
            var fullName = Path.Combine(_path, name);
            File.WriteAllText(EnsurePathExists(fullName), data);
        }

        public string Name { get; }
        public string Id { get; }

        private static string EnsurePathExists(string path)
        {
            var directory = Directory.GetParent(path).FullName;
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            return path;
        }

        private readonly string _path;
    }
}
