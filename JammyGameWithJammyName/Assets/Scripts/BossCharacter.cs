using System.Collections;
using UnityEngine;

public class BossCharacter : CharacterBase
{
    [SerializeField]
    private int _maxBloodPressure;
    [SerializeField]
    private int _maxSuspicion;
    [SerializeField]
    private int _initialSuspicion;
    [SerializeField]
    private int _suspicionPerDamage;
    [SerializeField]
    private float _attackPreparationTime;
    [SerializeField]
    private float _attackRange;

    private int _currentSuspicion;

    private bool _preparingAttack;

    private PlayerCharacter _player;

    private void Start()
    {
        _currentSuspicion = _initialSuspicion;
        _player = FindObjectOfType<PlayerCharacter>();
    }

    protected override void Update()
    {
        if (!IsAttacking && !_preparingAttack && Vector3.Distance(transform.position, _player.transform.position) < _attackRange)
        {
            StartCoroutine(PrepareAttack());
        }

        base.Update();
    }

    private void FixedUpdate()
    {
        
    }

    private IEnumerator PrepareAttack()
    {
        _preparingAttack = true;
        //_animator.SetTrigger("PrepareAttack");
        yield return new WaitForSeconds(_attackPreparationTime);
        _preparingAttack = false;
        Attack();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        UIManager.Instance.SetBossHealth(CurrentHealth, MaxHealth);
    }

    private void IncreaseSuspicion(int amount)
    {
        _currentSuspicion += amount;
        UIManager.Instance.SetBossSuspicion(_currentSuspicion, _maxSuspicion);

        if (_currentSuspicion >= _maxSuspicion)
        {
            Die(CauseOfDeath.Suspicious);
        }
    }

    private void DecreaseSuspicion(int amount)
    {
        _currentSuspicion -= amount;
        UIManager.Instance.SetBossSuspicion(_currentSuspicion, _maxSuspicion);
    }

    protected override void Die(CauseOfDeath causeOfDeath)
    {
        base.Die(causeOfDeath);
        UIManager.Instance.GameOver(false);
    }

    protected override void HandleIncomingDamageEffects(int damage)
    {
        DecreaseSuspicion(damage * _suspicionPerDamage);
    }

    protected override bool IsFacingLeft()
    {
        return _player.transform.position.x <= transform.position.x;
    }
}
