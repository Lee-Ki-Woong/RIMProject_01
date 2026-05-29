using UnityEngine;

public static class ComponentExtension
{
    public static void Log(this Component component, string text)
    {
        Debug.Log($"{component.gameObject.name} : " + text);
    }

    public static void LogWarning(this Component component, string text)
    {
        Debug.LogWarning($"{component.gameObject.name} : " + text);
    }

    public static void LogError(this Component component, string text)
    {
        Debug.LogError($"{component.gameObject.name} : " + text);
    }
}
