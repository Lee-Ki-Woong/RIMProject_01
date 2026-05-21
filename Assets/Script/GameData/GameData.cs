using System.Collections.Generic;
using System;

[Serializable]
public class GameDataBase
{
    public string Id;
}

[Serializable]
public class CharacterData : GameDataBase
{
    public string Name;
    public string OtherName;
    public string Class;
    public string Description;

    public int Hp;

    public string[] SkillList;
    public string UtSkill;

    public string Icon_path;
    public string GameObject_path;
}

[Serializable]
public class DialogueListData : GameDataBase
{
    public List<string> DialogueList;
}

[Serializable]
public class DialougeData : GameDataBase
{
    public string CharacterName;
    public string Description;
    public string NextDialougeId;
}