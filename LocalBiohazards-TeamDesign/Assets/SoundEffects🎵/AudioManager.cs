using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    AudioSource m_AudioSource;
    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }
    public void PlayOneShot(AudioClip AC)
    {
        m_AudioSource.PlayOneShot(AC);
    }
}
