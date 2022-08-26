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
        _currentAmmo.text = $"{GunManager.Instance.CurrentGun.CurrentAmmo}";
        _remainAmmo.text = $"{GunManager.Instance.CurrentGun.RemainAmmo}";
    }
}
