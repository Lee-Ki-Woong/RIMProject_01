using Cysharp.Threading.Tasks;
using UnityEngine;

public static class LoadUtil
{
    public static async UniTask<Sprite> LoadSprite(string address)
    {
        Sprite sprite = await ResourceManager.Instance.LoadAsset<Sprite>(address);
        if (sprite == null) LogError(address);

        return sprite;
    }

    public static async UniTask<GameObject> LoadPrefab(string address)
    {
        GameObject gameOjbect = await ResourceManager.Instance.LoadAsset<GameObject>(address);
        if (gameOjbect == null) LogError(address);

        return gameOjbect;
    }

    public static async UniTask<AudioClip> LoadAudioClip(string address)
    {
        AudioClip audioClip = await ResourceManager.Instance.LoadAsset<AudioClip>(address);
        if (audioClip == null) LogError(address);

        return audioClip;
    }

    private static void LogError(string address)
    {
        Debug.LogError($"{address}가 없습니다!! 다시 확인해주세요");
    }
}
