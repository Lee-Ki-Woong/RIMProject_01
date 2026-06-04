using System.Collections.Generic;
using UnityEngine;

public class GameObjectManager : BaseManager<GameObjectManager>
{
    public Character OnFieldCharacter { get; private set; }
    public Character FirstSlotCharacter { get; private set; }
    public Character SecondSlotCharacter { get; private set; }
    public Character ThirdSlotCharacter { get; private set; }

    public List<Character> CharacterList { get; private set; } = new();






    protected override void Awake()
    {
        base.Awake();
    }
}
