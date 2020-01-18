using UnityEngine;

public class PlayerCharacter : CharacterBase
{
    [SerializeField]
    private float _dashTime;
    [SerializeField]
    private float _dashSpeed;

    private Vector3 _dashDirection;

    private void Update()
    {
        float horizontalSpeed = Input.GetAxis("Horizontal");
        float verticalSpeed = Input.GetAxis("Vertical");
    }

    private void Dash()
    {

    }

    private void Jump()
    {

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
