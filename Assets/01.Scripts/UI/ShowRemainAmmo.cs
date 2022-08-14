using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowRemainAmmo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentAmmo;
    [SerializeField] private TextMeshProUGUI _remainAmmo;

    private void Update()
    {
        _currentAmmo.text = $"{GunSwapManager.Instance.CurrentGun.CurrentAmmo}";
        _remainAmmo.text = $"{GunSwapManager.Instance.CurrentGun.StartAmmo}";
    }
}
