using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSwapManager : MonoBehaviour
{
    [SerializeField] private Gun[] _guns;

    private Dictionary<GunMode, Gun> _gunList;

    private void Awake()
    {
        _gunList.Add(GunMode.AK47, _guns[0]);
        _gunList.Add(GunMode.ScarH, _guns[1]);
        _gunList.Add(GunMode.DesertEagle, _guns[2]);
        _gunList.Add(GunMode.G36C, _guns[3]);
        _gunList.Add(GunMode.M16, _guns[4]);
    }

    private void Update()
    {
        
    }
}
