using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : SellAble
{
    private PlayerHealth _playerHealth;

    private void Start()
    {
        _playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        SetUp(15, "체력회복");
    }

    private void Update()
    {
        _infoTxt.gameObject.SetActive(_isSellAble);
        ShowInfo();
        if (_isSellAble && Input.GetKeyDown(KeyCode.E))
        {
            Sell();
        }
    }

    public override void Sell()
    {
        StopAllCoroutines();
        if (ItemManager.Instance.CoinCount >= _price)
        {
            StartCoroutine(SellSuccess());
            ItemManager.Instance.CoinCount -= _price;
            _playerHealth.Heal(30);
        }
        else
        {
            StartCoroutine(SellFail());
        }
    }
}
