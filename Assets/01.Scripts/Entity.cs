using System;
using UnityEngine;

public class Entity : MonoBehaviour, IDamage
{
    public float startHealth = 100f;
    public float Health { get; protected set; }
    public bool Dead { get; protected set; }
    public event Action OnDeath;

    protected virtual void OnEnable()
    {
        Dead = false;
        Health = startHealth;
    }

    public void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        Health -= damage;

        if(Health <= 0 && !Dead)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        OnDeath?.Invoke();
        Dead = true;
    }
}
