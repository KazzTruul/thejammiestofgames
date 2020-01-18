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
    [SerializeField]
    protected Rigidbody2D _rigidbody;
    [SerializeField]
    private float _attackAnimationTime;
    [SerializeField]
    private float _attackCooldownTime;
    [SerializeField]
    private GameObject _leftWeaponObject;
    [SerializeField]
    private GameObject _rightWeaponObject;
    [SerializeField]
    private float _maxMoveSpeed;

    private int _currentHealth;
    private bool _invulnerable;
    private bool _isAttacking;
    private bool _isAirborne;

    private BoxCollider2D _boxCollider;

    protected int CurrentHealth => _currentHealth;
    protected int MaxHealth => _maxHealth;
    protected bool IsAttacking => _isAttacking;
    protected bool IsAirborne => _isAirborne;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Update()
    {
        _isAirborne = !_boxCollider.IsTouchingLayers(LayerMask.GetMask("Floors"));
        _characterImage.transform.localScale = new Vector3(IsFacingLeft() ? -1f : 1f, 1f, 1f);
    }

    protected virtual void Move(float velocity)
    {
        bool isMoving = velocity != 0f;
        _rigidbody.velocity = new Vector2(isMoving ? velocity * _movementSpeed * Time.deltaTime : 0f, _rigidbody.velocity.y);
        _animator.SetBool("IsMoving", isMoving);
    }

    protected virtual void Attack()
    {
        if (_isAttacking)
        {
            return;
        }

        _animator.SetTrigger("Attack");

        if (IsFacingLeft())
        {
            _leftWeaponObject.SetActive(true);
        }
        else
        {
            _rightWeaponObject.SetActive(true);
        }
        StartCoroutine(AttackCooldown());
    }

    public virtual void TakeDamage(int damage)
    {
        HandleIncomingDamageEffects(damage);

        if (_invulnerable)
        {
            return;
        }

        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Die(CauseOfDeath.Damage);
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

    private IEnumerator AttackCooldown()
    {
        _isAttacking = true;

        yield return new WaitForSeconds(_attackCooldownTime);

        if (_leftWeaponObject.activeInHierarchy)
        {
            _leftWeaponObject.SetActive(false);
        }

        _isAttacking = false;
    }

    protected virtual void Die(CauseOfDeath causeOfDeath)
    {
        switch (causeOfDeath)
        {
            case CauseOfDeath.Damage:
                _animator.SetTrigger("DamageDeath");
                break;

            case CauseOfDeath.HeartAttack:
                _animator.SetTrigger("HeartAttackDeath");
                break;

            case CauseOfDeath.Suspicious:
                _animator.SetTrigger("SuspicionDeath");
                break;
        }

        GameStateManager.SetGameState(GameState.GameOver);
    }

    protected abstract bool IsFacingLeft();
}
