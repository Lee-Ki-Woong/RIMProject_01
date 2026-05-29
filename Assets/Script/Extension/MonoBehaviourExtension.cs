using UnityEngine;

public static class MonoBehaviourExtension
{
    public static void ActiveTrue(this MonoBehaviour monoBehaviour)
    {
        monoBehaviour.gameObject.SetActive(true);
    }

    public static void ActiveFalse(this MonoBehaviour monoBehaviour)
    {
        monoBehaviour.gameObject.SetActive(false);
    }

    public static bool TryGetInstantiate(this MonoBehaviour monoBehaviour, GameObject prefab, Transform parent, out GameObject instance)
    {
        if (prefab == null)
        {
            monoBehaviour.LogError("게임 오브젝트 생성에 실패하였습니다!! 프리팹이 null입니다!!");
            instance = null;
            return false;
        }

        if (parent == null)
        {
            monoBehaviour.LogError($"게임 오브젝트의 생성에 실패하였습니다!! 부모트랜스폼이 null입니다!!");
            instance = null;
            return false;
        }

        instance = Object.Instantiate(prefab, parent);
        return true;
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