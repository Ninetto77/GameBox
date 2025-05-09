using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(0f, 3f)]
    public float pitch;

    public bool loop;
    public bool playOnAwake;
    public bool isSFX;

    [HideInInspector]
    public AudioSource source;
}
