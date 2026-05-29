using UnityEngine;

public static class GameObjectExtension
{
    public static bool ComponentChecking<T>(this GameObject gameObject, ref T component) where T : Component
    {
        if (component == null)
        {
            if (component = gameObject.GetComponent<T>())
            {
                Debug.LogWarning($"{typeof(T).Name}가 인스펙터에서 할당되지 않아 임시로 GetComponent를 사용하여 할당하였습니다!!");
                return true;
            }

            Debug.LogError($"{typeof(T).Name}가 인스펙터에서 할당되지 않았습니다!! 확인해주세요!! 임시로 이 오브젝트를 비활성화합니다!!");
            gameObject.SetActive(false);
            return false;
        }

        return true;
    }
}
