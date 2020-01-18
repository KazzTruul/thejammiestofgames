using System.Collections;
using UnityEngine;

public class PlayerCharacter : CharacterBase
{
    [SerializeField]
    private float _dashTime;
    [SerializeField]
    private float _dashSpeed;
    [SerializeField]
    private float _dashCooldown;

    private Vector2 _dashDirection;
    private float _horizontalSpeed;
    private float _verticalSpeed;
    private bool _isDashing;

    private void Update()
    {
        _horizontalSpeed = Input.GetAxis("Horizontal");
        _verticalSpeed = Input.GetAxis("Vertical");
    }

    private void Dash()
    {
        StartCoroutine(DashTimer());
    }

    private IEnumerator DashTimer()
    {
        _isDashing = true;

        _dashDirection = new Vector2(Mathf.Round(_horizontalSpeed), Mathf.Round(_verticalSpeed));

        float dashStartTime = Time.time;
        while (Time.time < dashStartTime + _dashTime)
        {
            _rigidbody.AddForce(_dashDirection * _dashSpeed);
            yield return new WaitForEndOfFrame();
        }

        _isDashing = false;
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
