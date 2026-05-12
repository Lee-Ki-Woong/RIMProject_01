using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance { get; private set; }





    private void Awake()
    {
        if(Instance == null) Instance = this;
    }



}
