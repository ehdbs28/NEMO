using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : Item
{
    private int _coin;
    private Transform _player;

    private Vector3 _dir;
    private float _acceleration;

    private void Awake()
    {
        _player = GameObject.Find("Player").transform;
    }

    private void OnEnable()
    {
        _coin = Random.Range(30, 100);
    }

    private void Update()
    {
        CoinMove();    
    }

    private void CoinMove()
    {
        _dir = (_player.position - transform.position).normalized;

        _acceleration = 3f;

        float distance = Vector3.Distance(_player.position, transform.position);

        if(distance <= 5.0f)
        {
            transform.position += _dir * _acceleration * Time.deltaTime;
        }
    }

    public override void UseItem()
    {
        ItemManager.Instance.CoinCount += _coin;
        PoolManager.Instance.Push(this);
    }

    public override void Reset()
    {
        
    }
}
