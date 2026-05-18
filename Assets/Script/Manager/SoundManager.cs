using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // [Instance]
    public static SoundManager Instance { get; private set; }


    // [SerializeField]
    [SerializeField] private AudioSource BGMSound;
    [SerializeField] private AudioSource SFXSound;


    // [Life Cycle]
    private void Awake()
    {
        if(Instance == null) Instance = this;
    }

    public void PlayBGM()
    {
        GameUtil.PlayAudioSource(BGMSound, "Sound/OpeningBGM", true, this.destroyCancellationToken).Forget();
    }
}
