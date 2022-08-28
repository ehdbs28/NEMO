using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class SellAble : MonoBehaviour
{
    protected bool _isSellAble = false;
    public bool IsSellAble { get => _isSellAble; set => _isSellAble = value; }

    public TextMeshProUGUI _infoTxt;

    public abstract void Sell();

    protected void ShowInfo(int price, string name)
    {
        _infoTxt.text = $"- {name} ({price}¿ø) -";
    }

    protected IEnumerator SellSuccess()
    {
        AudioManager.Instance.ItemSellSound();

        _infoTxt.color = Color.green;
        yield return new WaitForSeconds(0.3f);
        _infoTxt.color = Color.yellow;
    }

    protected IEnumerator SellFail()
    {
        AudioManager.Instance.ItemSellFailSound();

        _infoTxt.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        _infoTxt.color = Color.yellow;
    }
}
