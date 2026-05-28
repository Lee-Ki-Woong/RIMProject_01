using UnityEngine;

public static class LoadUtil
{
    public static GameObject LoadPrefab(string address)
    {
        GameObject prefab = ResourceManager.Instance.LoadAssetSync<GameObject>(address);

        if (prefab == null)
        {
            return null;
        }

        return prefab;
    }
}
