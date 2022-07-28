using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public float _shotDelay;
    public float _shotShakeValue;
    
    public abstract void Shot();
}
