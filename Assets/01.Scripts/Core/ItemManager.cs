using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance = null;

    private int _coinCount = 9999;
    public int CoinCount { get => _coinCount; set => _coinCount = value; }

    public void CreateItem(string itemName, Vector3 pos, Quaternion rotation)
    {
        int percentage = Random.Range(1, 101);

        if(percentage <= PlayerManager.Instance.PercentageIncrease)
        {
            Item item = PoolManager.Instance.Pop(itemName) as Item;
            item.transform.position = new Vector3(pos.x, pos.y + 1, pos.z);
            item.transform.rotation = rotation;
        }
    }
}
