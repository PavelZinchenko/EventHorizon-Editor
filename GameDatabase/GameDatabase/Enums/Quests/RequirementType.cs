namespace GameDatabase.Enums.Quests
{
    public enum RequirementType
    {
        Empty = 0,
        Any = 1,
        All = 2,
        None = 3,
        HaveItem = 5,
        PlayerPosition = 6,
        RandomStarSystem = 7,
        AggressiveOccupants = 8,

        QuestCompleted = 9,
        QuestActive = 10,

        CharacterRelations = 15,
        FactionRelations = 16,

        Faction = 20,

        HaveArtifact = 25,

        ComeBack = 30,
    }
}
