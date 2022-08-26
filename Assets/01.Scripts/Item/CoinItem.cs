using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : Item
{
    private int _coin;

    private void OnEnable()
    {
        _coin = Random.Range(30, 100);
    }

    private void Update()
    {
        ItemMove();    
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
