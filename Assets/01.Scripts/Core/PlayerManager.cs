using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance = null;

    private float _damageIncrease = 1f;
    private float _ammoIncrease = 1f;
    private float _healthIncrease = 0f;
    private float _percentageIncrease = 30f;

    public float DamageIncrease { get => _damageIncrease; }
    public float AmmoIncrease { get => _ammoIncrease; }
    public float PercentageIncrease { get => _percentageIncrease; }
    public float HealthIncrease { get => _healthIncrease; }

    public void UpIncrease(string type, float increase)
    {
        if (type == "Damage") _damageIncrease *= (1 + (increase / 100));
        else if (type == "Ammo") _ammoIncrease *= (1 + (increase / 100));
        else if (type == "Percentage" && _percentageIncrease < 100) _percentageIncrease += increase;
        else if (type == "Health") _healthIncrease += increase;
    }
}
