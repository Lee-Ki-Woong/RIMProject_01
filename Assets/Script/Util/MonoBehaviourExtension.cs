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


    public static void ActiveTrue(this MonoBehaviour monoBehaviour)
    {
        monoBehaviour.gameObject.SetActive(true);
    }

    public static void ActiveFalse(this MonoBehaviour monoBehaviour)
    {
        monoBehaviour.gameObject.SetActive(false);
    }


    public static bool ComponentChecking<T>(this MonoBehaviour monoBehaviour, ref T component) where T : Component
    {
        if (component == null)
        {
            if (component = monoBehaviour.gameObject.GetComponent<T>())
            {
                monoBehaviour.LogWarning($"{typeof(T).Name}가 인스펙터에서 할당되지 않아 임시로 GetComponent를 사용하여 할당하였습니다!!");
                return true;
            }

            monoBehaviour.LogError($"{typeof(T).Name}가 인스펙터에서 할당되지 않았습니다!! 확인해주세요!! 임시로 이 오브젝트를 비활성화합니다!!");
            monoBehaviour.ActiveFalse();
            return false;
        }

        return true;
    }
}