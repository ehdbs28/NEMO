using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : SellAble
{
    private int _price = 5;
    private string _name = "최대체력증가";

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

            _price += 5;
        }
        else
        {
            StartCoroutine(SellFail());
        }
    }
}
