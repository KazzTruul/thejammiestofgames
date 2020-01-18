using UnityEngine;

[CreateAssetMenu]
public class AudioData : ScriptableObject
{
    public AudioClip[] AttackAudioClips;
    public AudioClip DeathAudioClip;
    public AudioClip[] TakeDamageAudioClips;
    public AudioClip[] DialogAudio;
}
