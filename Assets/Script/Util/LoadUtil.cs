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

    public static async UniTask<GameObject> LoadPrefab(string address, CancellationToken token)
    {
        GameObject gameOjbect = await ResourceManager.Instance.LoadAsset<GameObject>(address, token);
        if (gameOjbect == null) LogError(address);

        return gameOjbect;
    }

    public static async UniTask<AudioClip> LoadAudioClip(string address, CancellationToken token)
    {
        AudioClip audioClip = await ResourceManager.Instance.LoadAsset<AudioClip>(address, token);
        if (audioClip == null) LogError(address);

        return audioClip;
    }

    private static void LogError(string address)
    {
        Debug.LogError($"{address}가 없습니다!! 다시 확인해주세요");
    }
}
