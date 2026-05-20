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
    public string Description;
    public int Hp;
    public int BaseDamage;
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