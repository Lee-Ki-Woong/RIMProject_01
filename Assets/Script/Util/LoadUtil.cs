using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public static class LoadUtil
{
    public static class Sync
    {
        public static GameObject LoadPrefab(string address)
        {
            return LoadGeneric<GameObject>(address);
        }

        public static T LoadGeneric<T>(string address) where T : Object
        {
            T asset = ResourceManager.Instance.LoadAssetSync<T>(address);
            if (asset == null)
            {
                return null;
            }
            return asset;
        }
    }

    public static class Async
    {
        public static async UniTask<Sprite> LoadSpriteAsync(string address)
        {
            return await LoadGenericAsync<Sprite>(address);
        }

        public static async UniTask<TMP_FontAsset> LoadFontAssetAsync(string address)
        {
            return await LoadGenericAsync<TMP_FontAsset>(address);
        }

        public static async UniTask<T> LoadGenericAsync<T>(string address) where T : Object
        {
            T asset = await ResourceManager.Instance.LoadAssetAsync<T>(address);
            if (asset == null)
            {
                return null;
            }
            return asset;
        }
    }
}
