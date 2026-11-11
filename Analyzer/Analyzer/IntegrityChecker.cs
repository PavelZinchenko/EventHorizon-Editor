using EditorDatabase;
using EditorDatabase.DataModel;
using EditorDatabase.Enums;
using EditorDatabase.Model;
using EditorDatabase.Serializable;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer.Analyzer
{
    public class IntegrityChecker
    {
        public IntegrityChecker(Database database)
        {
            _database = database;
        }

        public IEnumerable<string> LookForDeadLinks()
        {
            var list = Enumerable.Empty<object>();

            try
            {
                list = _database.GetAllItems().ToArray();
            }
            catch (DatabaseException e)
            {
                return Enumerable.Repeat(e.Message + "\n" + e.InnerException?.Message, 1);
            }

            return list.SelectMany(item => LookForDeadLinks(item)).Concat(LookForFrontalShieldFallbacks()).Append("done");
        }

        // The new combat simulator takes the frontal shield's shape from the first collider outline (3 or more points) of
        // its ColliderSpriteObject record. A circle, a capsule or an empty list falls back to the built-in polygon.
        private IEnumerable<string> LookForFrontalShieldFallbacks()
        {
            foreach (var id in _database.GetItemList(typeof(Device)))
            {
                var device = _database.GetDevice(id.Value);
                if (device.DeviceClass != DeviceClass.PartialShield || device.Prefab.IsNull) continue;

                var prefab = new GameObjectPrefabSerializable();
                _database.GetGameObjectPrefab(device.Prefab.Value).Save(prefab);
                if (prefab.Type != ObjectPrefabType.ColliderSpriteObject) continue;
                if (prefab.Collider?.FirstOrDefault(shape => shape.Points?.Length >= 3) != null) continue;

                yield return $"frontal shield fallback - device {device.Id.Value} uses the built-in polygon: prefab " +
                    $"{device.Prefab.Value} has no collider outline with 3 or more points";
            }
        }

        private IEnumerable<string> LookForDeadLinks(object data, int depth = 50)
        {
            if (depth == 0)
            {
                yield return "Infinite recursion detected. Object skipped - " + data.GetType().Name;
                yield break;
            }

            if (data == null)
                yield break;

            foreach (var field in data.GetType().GetFields())
            {
                if (field.IsInitOnly) continue;

                if (typeof(IItemId).IsAssignableFrom(field.FieldType))
                {
                    var value = field.GetValue(data);
                    if (value == null) continue;
                    var id = (IItemId)value;
                    if (id.Value == 0) continue;

                    var link = _database.GetItemId(id.ItemType, id.Value);
                    if (link.IsNull)
                        yield return "dead link - " + id.Name + " (" + id.Value + ")";

                    continue;
                }

                if (field.FieldType.IsClass)
                {
                    var value = field.GetValue(data);
                    foreach (var result in LookForDeadLinks(value, depth - 1))
                        yield return result;

                    continue;
                }
            }
        }

        private readonly Database _database;
    }
}
