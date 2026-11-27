using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 30;
    [SerializeField] private float invulnerableTime = 0.6f;
    [SerializeField] private float knockbackForce = 8f;

    private int current;
    private float invulnerableUntil;
    private Rigidbody2D body;

    public int Current => current;
    public int Max     => maxHealth;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        current = maxHealth;
    }

    public void Configure(int max)
    {
        maxHealth = max;
        current = maxHealth;
    }

    public void ResetHealth()
    {
        current = maxHealth;
        OnHealthChanged();
    }

    public void TakeDamage(int amount, Vector2 sourcePosition)
    {
        if (Time.time < invulnerableUntil) return;
        if (current <= 0) return;

        invulnerableUntil = Time.time + invulnerableTime;
        current = Mathf.Max(0, current - amount);
        OnHealthChanged();

        if (body != null && knockbackForce > 0f)
        {
            float dir = Mathf.Sign(transform.position.x - sourcePosition.x);
            if (dir == 0f) dir = 1f;
            body.linearVelocity = new Vector2(dir * knockbackForce, body.linearVelocity.y);
        }

        if (current <= 0) Die();
        else              OnDamaged();
    }

    protected virtual void OnDamaged()       { }
    protected virtual void Die()             { }
    protected virtual void OnHealthChanged() { }
}