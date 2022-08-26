using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class SellAble : MonoBehaviour
{
    protected int _price;
    private string _name;
    protected bool _isSellAble = false;
    public bool IsSellAble { get => _isSellAble; set => _isSellAble = value; }

    [SerializeField] protected TextMeshProUGUI _infoTxt;

    public abstract void Sell();

    protected void SetUp(int price, string name)
    {
        _price = price;
        _name = name;
    }

    protected void ShowInfo()
    {
        _infoTxt.text = $"- {_name} ({_price}¿ø) -";
    }

    protected IEnumerator SellSuccess()
    {
        _infoTxt.color = Color.green;
        yield return new WaitForSeconds(0.3f);
        _infoTxt.color = Color.yellow;
    }

    protected IEnumerator SellFail()
    {
        _infoTxt.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        _infoTxt.color = Color.yellow;
    }
}
