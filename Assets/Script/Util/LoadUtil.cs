using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public static class LoadUtil
{
    public static async UniTask<Sprite> LoadSprite(string address, CancellationToken token)
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

    public static async UniTask<AudioClip> LoadAudioClip(string address, CancellationToken token)
    {
        AudioClip audioClip = await ResourceManager.Instance.LoadAsset<AudioClip>(address, token);
        if (audioClip == null) LogError(address);

        return audioClip;
    }

    public static GameObject LoadPrefab(string path)
    {
        GameObject gameObject = (GameObject)Resources.Load(path);
        if (gameObject == null) LogError(path);

        return gameObject;
    }

    private static void LogError(string address)
    {
        Debug.LogError($"{address}가 없습니다!! 다시 확인해주세요");
    }
}
