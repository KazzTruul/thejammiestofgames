using UnityEngine;

public class BossCharacter : CharacterBase
{
    [SerializeField]
    private int _maxBloodPressure;
    [SerializeField]
    private int _maxSuspicion;
    [SerializeField]
    private int _initialSuspicion;

    private int _currentSuspicion;

    private void Start()
    {
        _currentSuspicion = _initialSuspicion;
    }

    protected override void Attack()
    {
        throw new System.NotImplementedException();
    }

    protected override void Die()
    {
        throw new System.NotImplementedException();
    }

    protected override void HandleIncomingDamageEffects(int damage)
    {
        throw new System.NotImplementedException();
    }
}
