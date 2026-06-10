using UnityEngine;

public class BaseManager<T> where T : class
{
    public static T Instance { get; protected set; }

    protected BaseManager()
    {
        if (Instance == null)
        {
            Instance = this as T;
        }
    }

    public void Log(string text)
    {
        Debug.Log($"{this} : " + text);
    }

    public void LogWarning(string text)
    {
        Debug.LogWarning($"{this} : " + text);
    }

    public void LogError(string text)
    {
        Debug.LogError($"{this} : " + text);
    }
}
