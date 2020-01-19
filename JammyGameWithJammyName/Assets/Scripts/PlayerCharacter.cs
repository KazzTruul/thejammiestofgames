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
    [SerializeField]
    private float _jumpTime;
    [SerializeField]
    private float _additionalGravity;

    private float _dashDirection;
    private float _horizontalSpeed;
    private float _verticalSpeed;
    private bool _isDashing;

    protected override void Update()
    {
        _horizontalSpeed = Input.GetAxis("Horizontal");
        _verticalSpeed = Input.GetAxis("Vertical");

        if (!IsAirborne && !_isDashing && !IsAttacking && !Invulnerable)
        {
            if (Input.GetButtonDown("Fire"))
            {
                Attack();
            }

            if (Input.GetButtonDown("Jump"))
            {
                InstaJump();
            }

            if (Input.GetButtonDown("Dash"))
            {
                Dash();
            }
        }

        if (IsAirborne)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y - _additionalGravity);
        }

        base.Update();
    }

    private void FixedUpdate()
    {
        if (!IsAirborne && !_isDashing && !IsAttacking)
        {
            Move(_horizontalSpeed);
        }
    }

    private void Dash()
    {
        StartCoroutine(DashTimer());
    }

    private IEnumerator DashTimer()
    {
        _isDashing = true;

        _dashDirection = Mathf.Round(_horizontalSpeed);

        float dashStartTime = Time.time;
        while (Time.time < dashStartTime + _dashTime)
        {
            _rigidbody.AddForce(new Vector2(_dashDirection * _dashSpeed, 0f));
            yield return new WaitForEndOfFrame();
        }

        _isDashing = false;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        UIManager.Instance.SetHeroHealth(CurrentHealth, MaxHealth);
    }

    private void InstaJump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
    }
    
    protected override void Die(CauseOfDeath causeOfDeath)
    {
        base.Die(causeOfDeath);
        UIManager.Instance.GameOver(true);
    }

    protected override void HandleIncomingDamageEffects(int damage)
    {
        return;
    }

    protected override bool IsFacingLeft()
    {
        return _horizontalSpeed < 0f;
    }
}
