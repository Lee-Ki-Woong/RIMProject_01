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
    public int MoveSpeed;
    public string[] SkillList;
    public string UltimateSkill;
    public string CharacterIconPath;
    public string CharacterStandPath;
    public string PlayerObjectPath;
}

[Serializable]
public class SkillData : GameDataBase
{
    public string Name;
    public string Description;
    public int Damage;
    public int Cooldown;

    public string SkillIconPath;
}

[Serializable]
public class EnemyData : GameDataBase
{
    public string Name;
    public int MaxHp;
    public int Damage;
    public int MoveSpeed;
    public string MonsterObjectPath;
}
