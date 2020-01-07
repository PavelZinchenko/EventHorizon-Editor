using GameDatabase.EditorModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GameDatabase.Controls
{
    public partial class BarrelsCollectionEditor : Controls.CollectionEditor
    {
        public BarrelsCollectionEditor()
        {
            InitializeComponent();
        }

        public Barrel SelectedBarrel()
        {
            if (_selectedItemId < 0 || _selectedItemId >= _collection.Length)
            {
                return null;
            }
            return _collection.GetValue(_selectedItemId) as Barrel;
        }

        public void SetSelectedObject(Barrel value)
        {
            if (_selectedItemId < 0 || _selectedItemId >= _collection.Length)
            {
                return;
            }
            _collection.SetValue(value, _selectedItemId);
            OnDataChanged(this,EventArgs.Empty);
        }

        public void SetSelectedId(int id)
        {
            _selectedItemId = id;
            CheckRadioButton();
        }

        public int GetSelectedItemId()
        {
            return _selectedItemId;
        }

        public new void OnDataChanged(object sender, EventArgs e)
        {
            base.OnDataChanged(this, EventArgs.Empty);
        }

        public IEnumerable<Barrel> GetBarrels()
        {
            return _collection.Cast<Barrel>();
        }

        public void clickUp()
        {
            moveUpButton_Click(this, EventArgs.Empty);
        }

        public void clickDown()
        {
            moveDownButton_Click(this, EventArgs.Empty);
        }

        public void clickClone()
        {
            cloneButton_Click(this, EventArgs.Empty);
        }
    }
}
