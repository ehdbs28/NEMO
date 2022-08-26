using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField] private List<Poolable> _poolingList;
    [SerializeField] private GameObject[] _bars;

    private bool _gameOver = false;
    public bool GameOver { get => _gameOver; set => _gameOver = value; }

    private bool _isShop = false;
    public bool IsShop { get => _isShop; set => _isShop = value; }

    private Animator _chestAnim;

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

        GunManager.Instance = gameObject.GetComponent<GunManager>();
        CameraManager.Instance = gameObject.AddComponent<CameraManager>();
        UIManager.Instance = gameObject.GetComponent<UIManager>();
        MonsterSpawnManager.Instance = gameObject.GetComponent<MonsterSpawnManager>();
        ItemManager.Instance = gameObject.AddComponent<ItemManager>();
        PlayerManager.Instance = gameObject.AddComponent<PlayerManager>();
    }

    private void Start()
    {
        _chestAnim = GameObject.Find("Chest").GetComponent<Animator>();
    }

    private void Update()
    {
        foreach(GameObject bar in _bars)
        {
            bar.SetActive(MonsterSpawnManager.Instance.IsWaving);
        }
        _chestAnim.SetBool("IsShop", _isShop);
        if(Input.GetKeyDown(KeyCode.R) && _gameOver)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
