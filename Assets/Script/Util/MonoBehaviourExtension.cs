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

    public static GameObject InstantiateGameObject(this MonoBehaviour monoBehaviour, GameObject gameObject, Transform parent)
    {
        if(gameObject == null)
        {
            monoBehaviour.LogError("게임 오브젝트 생성에 실패하였습니다!! 게임 오브젝트가 null입니다!!");
            return null;
        }

        if (parent == null)
        {
            monoBehaviour.LogError($"게임 오브젝트의 생성에 실패하였습니다!! 부모 트랜스폼이 null입니다!!");
            return null;
        }

        GameObject instantiateGameObject = Object.Instantiate(gameObject, parent);

        return instantiateGameObject;
    }
}
