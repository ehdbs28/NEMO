using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFx : Poolable
{
    private void OnEnable()
    {
        Invoke("PushAndStop", 0.5f);
    }

    public override void Reset()
    {
          
    }

    private void PushAndStop()
    {
        GetComponent<ParticleSystem>().Stop();
        PoolManager.Instance.Push(this);
    }
}
