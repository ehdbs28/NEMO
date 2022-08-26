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
    public List<Gun> GunList { get => _gunList; }

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
        Swap("AK47");
    }

    public void Swap(string type)
    {
        if (type == "AK47") _currentGun = _ak47;
        else if (type == "SCARH") _currentGun = _scarH;
        else if (type == "G36C") _currentGun = _g36c;
        else if (type == "M16") _currentGun = _m16;

        GunSwap();

        _currentGun.InitSetting();
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
