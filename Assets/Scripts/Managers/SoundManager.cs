using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource SFX;

    [Header("Sounds")]
    [SerializeField] private AudioClip shootAudioClip;
    [SerializeField] private AudioClip boomAudioClip;

    public static SoundManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void PlaySound(SoundTypes sound)
    {
        switch (sound)
        {
            case SoundTypes.Shoot:
                SFX.clip = shootAudioClip;
                break;
            case SoundTypes.Boom:
                SFX.clip = boomAudioClip;
                break;

            default:
                SFX.clip = null;
                break;
        }

        SFX.Play();
    }

    public enum SoundTypes
    {
        Shoot,
        Boom
    }

}