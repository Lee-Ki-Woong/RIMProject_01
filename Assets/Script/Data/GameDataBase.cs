using UnityEngine;

public class GameDataBase
{
    public string Id { get; set; }
}

public class UIData : GameDataBase
{
    public string[] Texts { get; set; }
    public System.Action[] Actions { get; set; }
}

public class CharacterData : GameDataBase
{
    public string Name { get; set; }
    public string OtherName { get; set; }
    public string Class { get; set; }
    public string Description { get; set; }
    public int MaxHp { get; set; }
    public string[] SkillList { get; set; }
    public string UltimateSkill { get; set; }
    public string CharacterIconPath { get; set; }
    public string PlayerObjectPath { get; set; }
}
