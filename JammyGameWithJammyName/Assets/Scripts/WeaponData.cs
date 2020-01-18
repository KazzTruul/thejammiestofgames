using UnityEngine;

[CreateAssetMenu]
public class WeaponData : ScriptableObject
{
    public AudioClip[] HitSounds;
    public AudioClip[] MissSounds;
    public int Damage;
}
