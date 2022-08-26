using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunItem : Item
{
    private void Update()
    {
        if (!GameManager.Instance.IsShop) PoolManager.Instance.Push(this);
    }

    public override void Reset()
    {
        
    }

    public override void UseItem()
    {
        GunManager.Instance.Swap(gameObject.name.Replace("Item", ""));
        PoolManager.Instance.Push(this);
    }
}
