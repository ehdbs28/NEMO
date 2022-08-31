using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : SellAble
{
    private int _price = 5;
    private string _name = "�ִ�ü������";
    private PlayerHealth _playerHealth;

    private void Start()
    {
        _playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (_infoTxt != null)
        {
            ShowInfo(_price, _name);

            if (_isSellAble && Input.GetKeyDown(KeyCode.E))
            {
                Sell();
            }
        }
    }

    public override void Sell()
    {
        StopAllCoroutines();
        if (ItemManager.Instance.CoinCount >= _price)
        {
            StartCoroutine(SellSuccess());
            ItemManager.Instance.CoinCount -= _price;
            PlayerManager.Instance.UpIncrease("Health", 10);
            _playerHealth.HealthUp();

            _price += 5;
        }
        else
        {
            StartCoroutine(SellFail());
        }
    }
}
