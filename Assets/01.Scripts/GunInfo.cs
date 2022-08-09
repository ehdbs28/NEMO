using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GunInfo", menuName = "SO/Gun Information")]
public class GunInfo : ScriptableObject
{
    //public AudioClip shotClip;
    //public AudioClip reloadClip;

    public float damage;

    public int startAmmoRemain;
    public int maxCapacity;

    public float reloadTime;
    public float shotDelay;

    public float fireDistance;
}
