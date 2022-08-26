using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : Poolable
{
    private Transform _player;

    private Vector3 _dir;
    private float _acceleration;

    public abstract void UseItem();

    private void Awake()
    {
        _player = GameObject.Find("Player").transform;

        _acceleration = Random.Range(3, 6);
    }

    protected void ItemMove()
    {
        _dir = (_player.position - transform.position).normalized;

        float distance = Vector3.Distance(_player.position, transform.position);

        if (distance <= 5.0f)
        {
            transform.position += _dir * _acceleration * Time.deltaTime;
        }
    }
}
