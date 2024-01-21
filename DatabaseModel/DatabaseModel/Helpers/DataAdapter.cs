using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EditorDatabase
{
    public interface IProperty
    {
        string Name { get; }
        Type Type { get; }
        bool IsReadOnly { get; }
        object Value { get; set; }
    }

    public interface IDataAdapter
    {
		event Action LayoutChangedEvent;
		event Action DataChangedEvent;
        IEnumerable<IProperty> Properties { get; }
    }

    public sealed class DataAdapter : IDataAdapter
    {
        public DataAdapter(object data)
        {
            _data = data;
        }

		public event Action DataChangedEvent;
		event Action IDataAdapter.LayoutChangedEvent { add { } remove { } }

		public IEnumerable<IProperty> Properties
        {
            get
            {
                return _data.GetType().GetFields().
                    Where(f => f.IsPublic && !f.IsStatic).
                    Select(item => new Property(_data, item, DataChangedEvent));
            }
        }

        private readonly object _data;
    }

    public class Property : IProperty
    {
        public Property(object data, FieldInfo fieldInfo, Action dataChangedAction)
        {
            if (fieldInfo == null) throw new ArgumentException();

            _data = data;
            _fieldInfo = fieldInfo;
            _dataChangedAction = dataChangedAction;
        }

        public string Name => _fieldInfo.Name;
        public Type Type => _fieldInfo.FieldType;
        public bool IsReadOnly => _fieldInfo.IsInitOnly;

        public object Value
        {
            get { return _fieldInfo.GetValue(_data); }
            set
            {
                _fieldInfo.SetValue(_data, value); 
                _dataChangedAction?.Invoke();
            }
        }

        private readonly object _data;
        private readonly FieldInfo _fieldInfo;
        private readonly Action _dataChangedAction;
    }
}
