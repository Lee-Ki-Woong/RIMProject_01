using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public static class GameUtil
{
    public static async UniTaskVoid PlayAudioSource(AudioSource audioSource, string address, bool isLoop, CancellationToken token)
    {
        AudioClip audioClip = await LoadUtil.LoadAudioClip(address, token);

        if (isLoop)
        {
            audioSource.clip = audioClip;
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
}
