using System.Collections;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    [SerializeField]
    private int _maxHealth;
    [SerializeField]
    protected float _movementSpeed;
    [SerializeField]
    protected Animator _animator;
    [SerializeField]
    private float _invulnerabilityTime;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioData _audioData;
    [SerializeField]
    protected SpriteData _spriteData;
    [SerializeField]
    protected SpriteRenderer _characterImage;

    private float _currentHealth;
    private bool _invulnerable;

    protected virtual void Attack()
    {
        _animator.SetTrigger("Attack");
    }

    private void TakeDamage(int damage)
    {
        HandleIncomingDamageEffects(damage);

        if(_invulnerable)
        {
            return;
        }

        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Die();
            return;
        }

        _animator.SetTrigger("IncomingDamage");

        StartCoroutine(Invulnerability());
    }

    protected abstract void HandleIncomingDamageEffects(int damage);

    private IEnumerator Invulnerability()
    {
        _invulnerable = true;
        yield return new WaitForSeconds(_invulnerabilityTime);
        _invulnerable = false;
    }

    protected virtual void Die()
    {
        _animator.SetTrigger("Die");
    }
}
