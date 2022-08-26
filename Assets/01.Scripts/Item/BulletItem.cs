using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletItem : Item
{
    private int _bullet = 30;

    public override void UseItem()
    {
        GunManager.Instance.CurrentGun.RemainAmmo += _bullet;
        PoolManager.Instance.Push(this);
    }

    private void Update()
    {
        ItemMove();
    }

    public override void Reset()
    {
        
    }
}
