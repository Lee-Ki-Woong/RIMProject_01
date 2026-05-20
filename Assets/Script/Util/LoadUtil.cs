using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public static class LoadUtil
{
    // [Load Async]
    public static async UniTask<Sprite> LoadSpriteAsync(string address, CancellationToken token)
    {
        Sprite sprite = await ResourceManager.Instance.LoadAsset<Sprite>(address, token);
        if (sprite == null) LogError(address);

        return sprite;
    }

    public static async UniTask<GameObject> LoadPrefabAsync(string address, CancellationToken token)
    {
        GameObject GameObject = await ResourceManager.Instance.LoadAsset<GameObject>(address, token);
        if (GameObject == null) LogError(address);

        return GameObject;
    }

    public static async UniTask<AudioClip> LoadAudioClipAsync(string address, CancellationToken token)
    {
        AudioClip audioClip = await ResourceManager.Instance.LoadAsset<AudioClip>(address, token);
        if (audioClip == null) LogError(address);

        return audioClip;
    }


    // [Load Sync]
    public static GameObject LoadPrefab(string path)
    {
        GameObject gameObject = (GameObject)Resources.Load(path);
        if (gameObject == null) LogError(path);

        return gameObject;
    }


    // [Error]
    private static void LogError(string address)
    {
        Debug.LogError($"{address}가 없습니다!! 다시 확인해주세요");
    }
}
