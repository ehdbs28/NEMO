using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance = null;

    [SerializeField] private Image _gameOverPanel;
    [SerializeField] private GameObject _player;
    [SerializeField] private TextMeshProUGUI _timeTxt;
    [SerializeField] private TextMeshProUGUI _scoreTxt;
    [SerializeField] private TextMeshProUGUI _waveTxt;

    private int _score = 0;
    private int _second = 0, _minute = 0, _hour = 0;

    private void Start()
    {
        StartCoroutine(ShowTime());
    }

    private void Update()
    {
        _waveTxt.text = $"F 키를 누르면 웨이브가 시작됩니다.\n다음웨이브:{ MonsterSpawnManager.Instance.Wave + 1}";
        _waveTxt.gameObject.SetActive(MonsterSpawnManager.Instance.IsWaving ? false : true);
        _timeTxt.text = $"{_hour.ToString("D2")}:{_minute.ToString("D2")}:{_second.ToString("D2")}";
        _scoreTxt.text = $"{_score.ToString("D5")}";
    }

    public void GameOver()
    {
        GameManager.Instance.GameOver = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        _player.SetActive(false);
        _gameOverPanel.gameObject.SetActive(true);
    }

    IEnumerator ShowTime()
    {
        while(!GameManager.Instance.GameOver)
        {
            yield return new WaitForSecondsRealtime(1f);
            _second++;
            if(_second == 60)
            {
                _second = 0;
                _minute++;
                if(_minute == 60)
                {
                    _minute = 0;
                    _hour++;
                }
            }
        }
    }

    public void AddScore(int score)
    {
        _score += score;
    }
}
