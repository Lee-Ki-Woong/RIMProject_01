using Cysharp.Threading.Tasks;
using UnityEngine;

public static class LoadUtil
{
    public static async UniTask<Sprite> LoadSprite(string address)
    {
        Sprite sprite = await ResourceManager.Instance.LoadAsset<Sprite>(address);
        return sprite;
    }

    public static async UniTask<GameObject> LoadPrefab(string address)
    {
        GameObject gameOjbect = await ResourceManager.Instance.LoadAsset<GameObject>(address);
        return gameOjbect;
    }

    public static async UniTask<AudioSource> LoadAudioSource(string address)
    {
        AudioSource audioSource = await ResourceManager.Instance.LoadAsset<AudioSource>(address);
        return audioSource;
    }
}
