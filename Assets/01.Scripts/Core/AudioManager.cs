using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;

    [SerializeField] private AudioMixer _master;
    public AudioMixer Master { get => _master; }

    [SerializeField] private AudioMixerGroup _chestGroup;
    [SerializeField] private AudioMixerGroup _itemGroup;
    [SerializeField] private AudioMixerGroup _doorGroup;

    [SerializeField] private AudioClip[] _bgmClips;
    [SerializeField] private AudioClip[] _sfxClips;
    private Dictionary<string, AudioClip> _clips = new Dictionary<string, AudioClip>();
    public Dictionary<string, AudioClip> Clips { get => _clips; }

    private AudioSource _bgm;

    private AudioSource _etcSource;
    public AudioSource EtcSource { get => _etcSource; }

    private void Awake()
    {
        _bgm = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        foreach(AudioClip ac in _sfxClips)
        {
            _clips.Add(ac.name, ac);
        }
        _etcSource = GetComponentInChildren<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(BarAudio());
    }

    private void Update()
    {
        PlayBGM();
    }

    public void PlaySFX(AudioSource source, AudioClip clip)
    {
        source.PlayOneShot(clip);
    }

    public void PlayBGM()
    {
        if (!GameManager.Instance.GameOver)
        {
            _bgm.clip = MonsterSpawnManager.Instance.IsWaving ? _bgmClips[1] : _bgmClips[0];
            if (!_bgm.isPlaying) _bgm.Play();
        }
    }

    public void GameOverSound()
    {
        _bgm.Stop();
        _bgm.PlayOneShot(_clips["GameOver"]);
    }

    public void UseItemSound()
    {
        _etcSource.outputAudioMixerGroup = _itemGroup;
        PlaySFX(_etcSource, _clips["UseItem"]);
    }

    public void ItemSellSound()
    {
        _etcSource.outputAudioMixerGroup = _itemGroup;
        PlaySFX(_etcSource, _clips["ItemSuccess"]);
    }

    public void ItemSellFailSound()
    {
        _etcSource.outputAudioMixerGroup = _itemGroup;
        PlaySFX(_etcSource, _clips["ItemFail"]);
    }

    public void ChestAudio()
    {
        _etcSource.outputAudioMixerGroup = _chestGroup;
        PlaySFX(_etcSource, _clips["ChestOpen"]);
    }

    public void BarOpen()
    {
        _etcSource.outputAudioMixerGroup = _doorGroup;
        PlaySFX(_etcSource, _clips["DoorUnlock"]);
    }

    public void BarClose()
    {
        _etcSource.outputAudioMixerGroup = _doorGroup;
        PlaySFX(_etcSource, _clips["DoorLock"]);
    }

    IEnumerator BarAudio()
    {
        while (!GameManager.Instance.GameOver)
        {
            yield return new WaitUntil(() => MonsterSpawnManager.Instance.IsWaving);
            BarClose();
            yield return new WaitUntil(() => !MonsterSpawnManager.Instance.IsWaving);
            BarOpen();
        }
    }
}
