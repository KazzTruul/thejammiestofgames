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

    private float _dashDirection;
    private float _horizontalSpeed;
    private float _verticalSpeed;
    private bool _isDashing;
    private bool _isAirborne;

    protected override void Update()
    {
        _horizontalSpeed = Input.GetAxis("Horizontal");
        _verticalSpeed = Input.GetAxis("Vertical");

        if (!_isAirborne && !_isDashing && !IsAttacking)
        {
            if (Input.GetButtonDown("Fire"))
            {
                Attack();
            }

            if (Input.GetButtonDown("Jump"))
            {
                StartCoroutine(Jump());
            }

            if (Input.GetButtonDown("Dash"))
            {
                Dash();
            }

            Move(_horizontalSpeed);
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

    private IEnumerator Jump()
    {
        float jumpStartTime = Time.time;
        while (Time.time < jumpStartTime + _jumpTime)
        {
            _rigidbody.AddForce(new Vector2(0f, _jumpForce));
            yield return new WaitForEndOfFrame();
        }
    }

    protected override void Die(CauseOfDeath causeOfDeath)
    {
        base.Die(causeOfDeath);
        UIManager.Instance.GameOver(true);
    }

    protected override void HandleIncomingDamageEffects(int damage)
    {

    }

    protected override bool IsFacingLeft()
    {
        return _horizontalSpeed < 0f;
    }
}
