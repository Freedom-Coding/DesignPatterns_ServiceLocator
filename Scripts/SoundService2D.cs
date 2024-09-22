using UnityEngine;

namespace ServiceLocatorPattern
{
    public class SoundService2D : MonoBehaviour, ISoundService
    {
        [SerializeField] private AudioSource audioSource;

        public void PlaySound(AudioClip clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}