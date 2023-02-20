using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string id;
    public AudioClip clip;
    public bool loop;
    public Vector2 pitchRange = new Vector2(1f, 1f);
    [Range(0f, 1f)] public float volume = 1;

    public void Play()
    {
        float pitch = Random.Range(pitchRange.x, pitchRange.y);
        source.pitch = pitch;
        source.Play();
    }

    public void Stop()
    {
        source.Stop();
    }

    public void Pause()
    {
        source.Pause();
    }

    [HideInInspector] public AudioSource source;
}
