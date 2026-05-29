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
