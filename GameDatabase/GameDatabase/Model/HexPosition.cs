using System;

namespace GameDatabase.Model
{
    public struct HexPosition
    {
        public HexPosition (int position = 0)
        {
            _position = position;
        }

        public static bool operator ==(HexPosition first, HexPosition second)
        {
            return first._position == second._position;
        }

        public static bool operator !=(HexPosition first, HexPosition second)
        {
            return first._position != second._position;
        }

        public static implicit operator int(HexPosition position)
        {
            return position._position;
        }

        public static HexPosition FromVector2(Vector2 value, Vector2 scale)
        {
            var y = (int)Math.Round(value.y / scale.y);
            var offset = y % 2 == 0 ? 0.0f : 0.5f;
            var x = (int)Math.Round(value.x / scale.x - offset);
            return new HexPosition(PositionToId(x, y));
        }

        public Vector2 ToVector2(Vector2 scale)
        {
            int x, y;
            IdToPosition(_position, out x, out y);
            var offset = y % 2 == 0 ? 0.0f : 0.5f;
            return new Vector2((x + offset) * scale.x, y * scale.y);
        }

        public static int PositionToId(int x, int y)
        {
            var distance = Math.Max(Math.Abs(x), Math.Abs(y));
            var length = distance * 2 + 1;
            var id = length * length - 1;

            if (y == -distance)
                id -= distance + x;
            else if (y == distance)
                id -= distance + length + x;
            else if (x == -distance)
                id -= 2 * length + (y + distance - 1) * 2;
            else if (x == distance)
                id -= 2 * length + (y + distance - 1) * 2 + 1;

            return id;
        }

        public static void IdToPosition(int id, out int x, out int y)
        {
            var length = (int)Math.Floor(Math.Sqrt(id)) + 1;
            if (length % 2 == 0) length++;
            var distance = (length - 1) / 2;

            id = length * length - 1 - id;

            if (id < length)
            {
                y = -distance;
                x = id - distance;
            }
            else if (id < 2 * length)
            {
                y = distance;
                x = id - distance - length;
            }
            else
            {
                x = (id - 2 * length) % 2 == 0 ? -distance : distance;
                y = (id - 2 * length) / 2 - distance + 1;
            }
        }

        private readonly int _position;
    }
}
