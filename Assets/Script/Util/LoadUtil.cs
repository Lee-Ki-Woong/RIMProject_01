using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public static class LoadUtil
{
    public static class Sync
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

    public static class Async
    {
        public static async UniTask<Sprite> LoadSpriteAsync(string address)
        {
            Sprite sprite = await ResourceManager.Instance.LoadAssetAsync<Sprite>(address);

            if (sprite == null)
            {
                return null;
            }

            return sprite;
        }

        public static async UniTask<TMP_FontAsset> LoadFontAssetAsync(string address)
        {
            TMP_FontAsset fontAsset = await ResourceManager.Instance.LoadAssetAsync<TMP_FontAsset>(address);

            if (fontAsset == null)
            {
                return null;
            }

            return fontAsset;
        }
    }
}
