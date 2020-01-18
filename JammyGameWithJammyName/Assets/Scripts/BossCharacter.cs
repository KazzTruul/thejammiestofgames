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
    private int _suspicionPerDamage = 2;

    private int _currentSuspicion;

    private void Start()
    {
        _currentSuspicion = _initialSuspicion;
    }

    protected override void TakeDamage(int damage)
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
}
