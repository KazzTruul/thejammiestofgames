﻿using UnityEngine;

[CreateAssetMenu]
public class AudioData : ScriptableObject
{
    public AudioClip[] AttackAudioClips;
    public AudioClip[] DeathAudioClips;
    public AudioClip[] TakeDamageAudioClips;
    public AudioClip[] DialogAudio;
}