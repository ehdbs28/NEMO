using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField] private List<Poolable> _poolingList;

    private bool _gameOver = false;
    public bool GameOver { get => _gameOver; set => _gameOver = value; }

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if(Instance != null)
        {
            Debug.LogError("Multiple GameManager Instance has running !");
        }
        Instance = this;

        PoolManager.Instance = new PoolManager(transform);
        foreach(Poolable p in _poolingList)
        {
            PoolManager.Instance.CreatePool(p, 10);
        }

        GunSwapManager.Instance = gameObject.GetComponent<GunSwapManager>();
        CameraManager.Instance = gameObject.AddComponent<CameraManager>();
        UIManager.Instance = gameObject.GetComponent<UIManager>();
        MonsterSpawnManager.Instance = gameObject.GetComponent<MonsterSpawnManager>();
    }
}
