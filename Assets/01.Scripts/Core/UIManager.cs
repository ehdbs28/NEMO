using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance = null;

    [SerializeField] private Image _gameOverPanel;
    [SerializeField] private GameObject _player;
    [SerializeField] private TextMeshProUGUI _timeTxt;
    [SerializeField] private TextMeshProUGUI _scoreTxt;
    [SerializeField] private TextMeshProUGUI _waveTxt;
    [SerializeField] private TextMeshProUGUI _coinTxt;
    [SerializeField] private Image _escPanel;
    [SerializeField] private Image _settingPanel;
    [SerializeField] private Slider _bgmSlider;
    [SerializeField] private Slider _sfxSlider;

    private int _score = 0;
    private int _second = 0, _minute = 0, _hour = 0;

    private bool _isSetting = false;
    public bool IsSetting { get => _isSetting; }

    private void Start()
    {
        StartCoroutine(ShowTime());
    }

    private void Update()
    {
        _coinTxt.text = $"{ItemManager.Instance.CoinCount.ToString("D4")}";
        _waveTxt.text = $"F 키를 누르면 웨이브가 시작됩니다.\n다음웨이브:{ MonsterSpawnManager.Instance.Wave + 1}";
        _waveTxt.gameObject.SetActive(MonsterSpawnManager.Instance.IsWaving ? false : true);
        _timeTxt.text = $"{_hour.ToString("D2")}:{_minute.ToString("D2")}:{_second.ToString("D2")}";
        _scoreTxt.text = $"{_score.ToString("D5")}";

        if (Input.GetKeyDown(KeyCode.Escape) && !GameManager.Instance.GameOver && !_settingPanel.gameObject.activeSelf)
        {
            if (!_isSetting)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
            }
            if (_isSetting)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1;
            }
            _isSetting = !_isSetting;
        }
        _escPanel.gameObject.SetActive(_isSetting);

        AudioSetting();
    }

    private void AudioSetting()
    {
        if (_bgmSlider.value == _bgmSlider.minValue) AudioManager.Instance.Master.SetFloat("BGM", -80);
        else AudioManager.Instance.Master.SetFloat("BGM", _bgmSlider.value);
        if (_sfxSlider.value == _sfxSlider.minValue) AudioManager.Instance.Master.SetFloat("SFX", -80);
        else AudioManager.Instance.Master.SetFloat("SFX", _sfxSlider.value);
    }

    public void Continue()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;

        _isSetting = false;
    }

    public void Setting(bool value)
    {
        _settingPanel.gameObject.SetActive(value);
    }

    public void Exit()
    {
        SceneManager.LoadScene("Title");
    }

    public void GameOver()
    {
        GameManager.Instance.GameOver = true;

        AudioManager.Instance.GameOverSound();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        _player.SetActive(false);
        _gameOverPanel.gameObject.SetActive(true);
        _gameOverPanel.transform.Find("Score").GetComponent<TextMeshProUGUI>().text = $"점수 : {_score.ToString("D5")}";
        _gameOverPanel.transform.Find("Time").GetComponent<TextMeshProUGUI>().text = $"시간 : {_hour.ToString("D2")}:{_minute.ToString("D2")}:{_second.ToString("D2")}";
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
