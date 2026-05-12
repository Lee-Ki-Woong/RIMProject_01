using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // [SerializeField]
    [SerializeField] private AudioSource BGMSound;
    [SerializeField] private AudioSource SFXSound;

    // [Field]
    public static SoundManager Instance {  get; private set; }

    // [Life Cycle]
    private void Awake()
    {
        if(Instance == null) Instance = this;
    }

    public void PlayBGM()
    {
        GameUtil.PlayAudioSource(BGMSound, "Sound/OpeningBGM", true).Forget();
    }
}
