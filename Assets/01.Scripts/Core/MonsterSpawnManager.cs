using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnManager : MonoBehaviour
{
    public static MonsterSpawnManager Instance = null;

    [SerializeField] private MonsterData[] _monsterDatas;
    [SerializeField] private Transform[] _spawnPoints;

    private List<Monster> _monsterList = new List<Monster>();
    private int _wave;

    private bool _isWaving = false;
    public bool IsWaving { get => _isWaving; set => _isWaving = value; }

    private void Update()
    {
        if(GameManager.Instance != null && GameManager.Instance.GameOver)
        {
            return;
        }

        if(_monsterList.Count <= 0)
        {
            _isWaving = false;
            if(Input.GetKeyDown(KeyCode.F) && _isWaving == false)
            {
                SpawnWave();
            }
        }
    }

    private void SpawnWave()
    {
        _isWaving = true;

        _wave++;

        int spawnCount = Mathf.RoundToInt(_wave * 1.5f);

        for(int i = 0; i < spawnCount; i++)
        {
            CreateMonster();
        }
    }

    private void CreateMonster()
    {
        MonsterData monsterData = _monsterDatas[Random.Range(0, _monsterDatas.Length)];
        Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];

        Monster monster = PoolManager.Instance.Pop("Slime") as Monster;
        monster.transform.position = spawnPoint.position;
        monster.transform.rotation = spawnPoint.rotation;

        monster.SetUp(monsterData);
        _monsterList.Add(monster);

        monster.OnDie += () => _monsterList.Remove(monster);
        monster.OnDie += () => UIManager.Instance.AddScore(100);
    }
}
