using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    // [Instance]
    public static GameDataManager Instance { get; private set; }


    // [Field]
    public Dictionary<string, CharacterData> m_characterDataList { get; private set; } = new();




    // [Life Cycle]
    private void Awake()
    {
        if(Instance == null) Instance = this;
    }


    //[LoadData]
    private Dictionary<string, T> LoadData<T>(string path) where T : GameDataBase
    {
        string resourcePath = $"Json/{path}";

        TextAsset loadTextAsset = Resources.Load<TextAsset>(resourcePath);

        if(loadTextAsset == null)
        {
            Debug.LogError($"{resourcePath} 경로에 리소스가 없습니다! 다시 확인해주세요!!");
            return new Dictionary<string, T>();
        }
        
        try
        {
            string jsonData = loadTextAsset.text;




        }
        catch(System.Exception e)
        {
            Debug.LogException(e);
        }
        
        
        return new Dictionary<string, T>();
    }

}
