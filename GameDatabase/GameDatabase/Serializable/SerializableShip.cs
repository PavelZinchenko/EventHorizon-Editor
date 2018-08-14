using System;
using System.ComponentModel;
using GameDatabase.Enums;
using GameDatabase.Model;

namespace GameDatabase.Serializable
{
    [Serializable]
    public class SerializableShip : SerializableItem
    {
        public ShipCategory ShipCategory;

        public string Name;
        public int Faction;
        public SizeClass SizeClass;
        [DefaultValue("")]
        public string IconImage;
        public float IconScale;
        [DefaultValue("")]
        public string ModelImage;
        public float ModelScale;
        public Vector2 EnginePosition;
        [DefaultValue("")]
        public string EngineColor;
        public float EngineSize;

        public float EnergyResistance;
        public float KineticResistance;
        public float HeatResistance;
        public bool Regeneration;
        public float BaseWeightModifier;
        public int[] BuiltinDevices;

        [DefaultValue("")]
        public string Layout;
        public SerializableBarrel[] Barrels;
    }
}
