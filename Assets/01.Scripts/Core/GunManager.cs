using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public static GunManager Instance = null;

    [SerializeField] private Gun _ak47;
    [SerializeField] private Gun _scarH;
    [SerializeField] private Gun _g36c;
    [SerializeField] private Gun _m16;

    private List<Gun> _gunList = new List<Gun>();

    private Gun _currentGun;
    public Gun CurrentGun
    {
        get => _currentGun;
    }

    private void Awake()
    {
        _gunList.Add(_ak47);
        _gunList.Add(_scarH);
        _gunList.Add(_g36c);
        _gunList.Add(_m16);
    }

    private void Start()
    {
        SwapAk47();
    }

    private void Update()
    {
        //test
        if (Input.GetKeyDown(KeyCode.V))
        {
            SwapAk47();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            SwapScarH();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            SwapG36C();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            SwapM16();
        }
    }

    public void SwapAk47()
    {
        _currentGun = _ak47;
        GunSwap();
    }

    public void SwapScarH()
    {
        _currentGun = _scarH;
        GunSwap();
    }

    public void SwapG36C()
    {
        _currentGun = _g36c;
        GunSwap();
    }

    public void SwapM16()
    {
        _currentGun = _m16;
        GunSwap();
    }

    private void GunSwap()
    {
        foreach(Gun item in _gunList)
        {
            if(item == _currentGun)
            {
                item.gameObject.SetActive(true);
                continue;
            }
            item.gameObject.SetActive(false);
        }
    }
}
