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
    [SerializeField]
    private int _suspicionPerSecond;

    private int _currentSuspicion;

    private bool _preparingAttack;

    private PlayerCharacter _player;

    protected override bool _dontTurn => Mathf.Abs(transform.position.x - _player.transform.position.x) <  _attackRange * 0.1f;

    private void Start()
    {
        _currentSuspicion = _initialSuspicion;
        _player = FindObjectOfType<PlayerCharacter>();
        StartCoroutine(IncreaseSuspicionOverTime());
        UIManager.Instance.SetBossSuspicion(_currentSuspicion, _maxSuspicion);
        UIManager.Instance.SetBossHealth(CurrentHealth, MaxHealth);
    }

    private IEnumerator IncreaseSuspicionOverTime()
    {
        yield return new WaitForSeconds(1);
        IncreaseSuspicion(_suspicionPerSecond);
        StartCoroutine(IncreaseSuspicionOverTime());
    }

    protected override void Update()
    {
        if (_dontTurn)
        {
            return;
        }

        if (!IsAttacking && !_preparingAttack)
        {
            if (Vector3.Distance(transform.position, _player.transform.position) <= _attackRange)
            {
                StartCoroutine(PrepareAttack());
            }
            else
            {
                _rigidbody.velocity = new Vector2(_movementSpeed * (IsFacingLeft() ? -1 : 1), _rigidbody.velocity.y);
            }
        }

        base.Update();
    }

    private IEnumerator PrepareAttack()
    {
        _preparingAttack = true;
        _animator.SetTrigger("PrepareAttack");
        yield return new WaitForSeconds(_attackPreparationTime);
        _preparingAttack = false;
        Attack();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        UIManager.Instance.SetBossHealth(CurrentHealth, MaxHealth);
        
        int randomInt = Random.Range(0, 99);
        if (randomInt > 90)
        {
            DialogueManager.Instance.DialogueTyp = DialogueManager.DialogueType.BossTakesDamage;
        }
    }

    private void IncreaseSuspicion(int amount)
    {
        int previousSuspicion = _currentSuspicion;
        _currentSuspicion += amount;
        UIManager.Instance.SetBossSuspicion(_currentSuspicion, _maxSuspicion);

        if (_currentSuspicion >= _maxSuspicion)
        {
            Die(CauseOfDeath.Suspicious);
            return;
        }

        int previousGrade = Mathf.RoundToInt((float)previousSuspicion / (float)_maxSuspicion / 3);
        int currentGrade = Mathf.RoundToInt((float)_currentSuspicion / (float)_maxSuspicion / 3);

        if (currentGrade > previousGrade)
        {
            DialogueManager.DialogueType type = DialogueManager.DialogueType.None;
            switch (currentGrade)
            {
                case 1:
                    type = DialogueManager.DialogueType.Suspicious_Grade1;
                    break;
                case 2:
                    type = DialogueManager.DialogueType.Suspicious_Grade2;
                    break;
                case 3:
                    type = DialogueManager.DialogueType.Suspicious_Grade3;
                    break;
            }

            DialogueManager.Instance.DialogueTyp = type;
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
        DialogueManager.Instance.DialogueTyp = DialogueManager.DialogueType.BossDies;
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
