using UnityEngine;

namespace ServiceLocatorPattern
{
    public interface ISoundService
    {
        void PlaySound(AudioClip clip);
    }
}