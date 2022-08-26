using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance = null;

    private float _damageIncrease = 0f;
    private float _ammoIncrease = 0f;
    private float _healthIncrease = 0f;
    private float _percentageIncrease = 30f;
    private float _speedIncrease = 0f;

    public float DamageIncrease { get => _damageIncrease; }
    public float AmmoIncrease { get => _ammoIncrease; }
    public float PercentageIncrease { get => _percentageIncrease; }
    public float HealthIncrease { get => _healthIncrease; }
    public float SpeedIncrease { get => _speedIncrease; }

    public void UpIncrease(string type, float increase)
    {
        if (type == "Damage")
        {
            _damageIncrease += increase;
        }
        else if (type == "Ammo")
        {
            _ammoIncrease += increase;
        }
        else if (type == "Percentage" && _percentageIncrease < 100)
        {
            _percentageIncrease += increase;
        }
        else if (type == "Health")
        {
            _healthIncrease += increase;
        }
        else if (type == "Speed")
        {
            _speedIncrease += increase;
            _speedIncrease = Mathf.Clamp(_speedIncrease, 0, 5);
        }
    }
}
