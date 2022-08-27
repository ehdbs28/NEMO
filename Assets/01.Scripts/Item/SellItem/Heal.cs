using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : SellAble
{
    private int _price = 15;
    private string _name = "체력회복";

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
            _playerHealth.Heal(Mathf.RoundToInt(_playerHealth._startHealth / 10));
        }
        else
        {
            StartCoroutine(SellFail());
        }
    }
}
