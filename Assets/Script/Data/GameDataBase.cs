using System;


[Serializable]
public class GameDataBase
{
    public string Id;
}

[Serializable]
public class UIData : GameDataBase
{
    public string[] Texts;
    public System.Action[] Actions;
}

[Serializable]
public class CharacterData : GameDataBase
{
    public string Name;
    public string OtherName;
    public string Class;
    public string Description;
    public int MaxHp;
    public string[] SkillList;
    public string UltimateSkill;
    public string CharacterIconPath;
    public string PlayerObjectPath;
}
