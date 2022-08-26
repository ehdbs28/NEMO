using System;
using UnityEngine;

public class Entity : Poolable, IDamage
{
    public float _startHealth = 100f;
    public float _health { get; protected set; }
    public bool _dead { get; protected set; }
    public Action OnDie = null;

    protected virtual void OnEnable()
    {
        _dead = false;
        _health = _startHealth;
    }

    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        _health -= damage;

        if(_health <= 0 && !_dead)
        {
            Die();
        }
    }

    public virtual void Heal(float heal)
    {
        if (!_dead)
        {
            _health += heal;
            if(_health > _startHealth)
            {
                _health = _startHealth;
            }
        }
    }

    public virtual void Die()
    {
        OnDie?.Invoke();
        _dead = true;
    }

    public override void Reset()
    {
        
    }
}
