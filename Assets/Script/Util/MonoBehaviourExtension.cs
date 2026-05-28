using UnityEngine;

public static class MonoBehaviourExtension
{
    public static void Log(this MonoBehaviour monoBehaviour, string text)
    {
        Debug.Log($"{monoBehaviour.gameObject.name} : " + text);
    }

    public static void LogWarning(this MonoBehaviour monoBehaviour, string text)
    {
        Debug.LogWarning($"{monoBehaviour.gameObject.name} : " + text);
    }

    public static void LogError(this MonoBehaviour monoBehaviour, string text)
    {
        Debug.LogError($"{monoBehaviour.gameObject.name} : " + text);
    }
}
