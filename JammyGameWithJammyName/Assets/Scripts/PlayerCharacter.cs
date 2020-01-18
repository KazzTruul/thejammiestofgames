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
    [SerializeField]
    private float _jumpForce;

    private Vector2 _dashDirection;
    private float _horizontalSpeed;
    private float _verticalSpeed;
    private bool _isDashing;

    protected override void Update()
    {
        _horizontalSpeed = Input.GetAxis("Horizontal");
        _verticalSpeed = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Fire"))
        {
            Attack();
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (Input.GetButtonDown("Dash"))
        {
            Dash();
        }

        base.Update();
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

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        UIManager.Instance.SetHeroHealth(CurrentHealth, MaxHealth);
    }

    private void Jump()
    {
        _rigidbody.AddForce(new Vector2(0f, _jumpForce));
    }

    protected override void Die(CauseOfDeath causeOfDeath)
    {
        base.Die(causeOfDeath);
        UIManager.Instance.GameOver(true);
    }

    protected override void HandleIncomingDamageEffects(int damage)
    {
        throw new System.NotImplementedException();
    }

    protected override bool IsFacingLeft()
    {
        return _horizontalSpeed < 0f;
    }
}
