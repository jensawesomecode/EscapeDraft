using UnityEngine;

public class SfxManager : MonoBehaviour
{
    [SerializeField] private AudioSource sfxSource;

    public void PlayOneShot(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip, volume);
    }
}
