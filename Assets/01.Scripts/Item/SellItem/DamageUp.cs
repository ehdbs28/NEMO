using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUp : SellAble
{
    private void Start()
    {
        SetUp(5, "데미지업");
    }

    private void Update()
    {
        _infoTxt.gameObject.SetActive(_isSellAble);
        ShowInfo();
        if(_isSellAble && Input.GetKeyDown(KeyCode.E))
        {
            Sell();
        }
    }

    public override void Sell()
    {
        StopAllCoroutines();
        if(ItemManager.Instance.CoinCount >= _price)
        {
            StartCoroutine(SellSuccess());
            ItemManager.Instance.CoinCount -= _price;
            PlayerManager.Instance.UpIncrease("Damage", 20);
            GunManager.Instance.CurrentGun.InitSetting();

            _price += 5;
        }
        else
        {
            StartCoroutine(SellFail());
        }
    }
}
