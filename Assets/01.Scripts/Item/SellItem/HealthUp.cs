using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : SellAble
{
    private void Start()
    {
        SetUp(5, "최대체력증가");
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
            PlayerManager.Instance.UpIncrease("Health", 10);

            _price += 5;
        }
        else
        {
            StartCoroutine(SellFail());
        }
    }
}
