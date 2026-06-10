using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public static class LoadUtil
{
    public static class Sync
    {
        public static GameObject LoadPrefab(string address)
        {
            GameObject prefab = LoadGeneric<GameObject>(address);
            
            if(prefab == null)
            {
                prefab = LoadGeneric<GameObject>(AddressUtil.Sync.Prefab.BasePrefab);
            }

            return prefab;
        }

        public static TextAsset LoadTextAsset(string address)
        {
            TextAsset textAsset = LoadGeneric<TextAsset>(address);
            
            return textAsset;
        }

        public static T LoadGeneric<T>(string address) where T : Object
        {
            T asset = ResourceManager.Instance.LoadAssetSync<T>(address);
            
            if (asset == null)
            {
                LogError(address);
                return null;
            }

            return asset;
        }
    }

    public static class Async
    {
        public static async UniTask<Sprite> LoadSpriteAsync(string address)
        {
            Sprite sprite = await LoadGenericAsync<Sprite>(address);
            
            if(sprite == null)
            {
                sprite = await LoadGenericAsync<Sprite>(AddressUtil.Async.Sprite.BaseSprite);
            }

            return sprite;
        }

        public static async UniTask<TMP_FontAsset> LoadFontAssetAsync(string address)
        {
            TMP_FontAsset fontAsset = await LoadGenericAsync<TMP_FontAsset>(address);
            
            if(fontAsset == null)
            {
                fontAsset = await LoadGenericAsync<TMP_FontAsset>(AddressUtil.Async.Font.BaseFont);
            }

            return fontAsset;
        }

        public static async UniTask<GameObject> LoadPrefabAsync(string address)
        {
            GameObject prefab = await LoadGenericAsync<GameObject>(address);
            
            if(prefab == null)
            {
                prefab = await LoadGenericAsync<GameObject>(AddressUtil.Async.Prefab.BasePrefab);
            }

            return prefab;
        }

        public static async UniTask<T> LoadGenericAsync<T>(string address) where T : Object
        {
            T asset = await ResourceManager.Instance.LoadAssetAsync<T>(address);
            
            if(asset == null)
            {
                LogError(address);
                return null;
            }

            return asset;
        }
    }

    private static void LogError(string address)
    {
        Debug.LogError($"{address} 경로에 리소스가 없습니다!!");
    }
}
