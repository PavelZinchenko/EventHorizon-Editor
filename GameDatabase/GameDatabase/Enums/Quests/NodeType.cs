namespace GameDatabase.Enums.Quests
{
    public enum NodeType
    {
        Undefined = 0,
        ComingSoon = 1,

        ShowDialog = 10,

        Switch = 15,
        Random = 16,

        AttackFleet = 20,
        AttackOccupants = 21,

        DestroyOccupants = 25,
        SuppressOccupants = 26,

        Retreat = 30,

        ObtainItem = 35,
        RemoveItem = 36,
        Trade = 37,

        CompleteQuest = 40,
        FailQuest = 41,
        CancelQuest = 42,
        StartQuest = 43,

        SetCharacterRelations = 50,
        SetFactionRelations = 51,
        ChangeCharacterRelations = 55,
        ChangeFactionRelations = 56,
    }
}
