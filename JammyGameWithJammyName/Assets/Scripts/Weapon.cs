using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private float _lifetime;
    [SerializeField]
    private WeaponData _weaponData;

    private AudioSource _audioSource;

    private SpriteRenderer _spriteRenderer;

    private CharacterBase _owner;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void SetOwner(CharacterBase newOwner)
    {
        if (_owner != null)
        {
            return;
        }

        _owner = newOwner;
    }

    public void SetWeaponSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }

    private void OnEnable()
    {
        StartCoroutine(HideWeapon());
    }

    private IEnumerator HideWeapon()
    {
        yield return new WaitForSeconds(_lifetime);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        CharacterBase hitCharacter = other.GetComponent<CharacterBase>();
        if (hitCharacter == null || hitCharacter == _owner)
        {
            _audioSource.PlayOneShot(_weaponData.MissSounds[Random.Range(0, _weaponData.MissSounds.Length - 1)]);
            return;
        }

        _audioSource.PlayOneShot(_weaponData.HitSounds[Random.Range(0, _weaponData.HitSounds.Length - 1)]);
        hitCharacter.TakeDamage(_weaponData.Damage);
    }
}
