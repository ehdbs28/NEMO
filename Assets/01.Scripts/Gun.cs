using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Gun : MonoBehaviour
{
    public enum State
    {
        Ready,
        Empty,
        Reloading
    }

    [SerializeField] private GunInfo _gunData;
    
    private Animator _anim;
    private State _state;
    public State state { get; set; }

    private Transform _firePos;
    private LineRenderer _line;

    private float _lastShotTime;

    private float _startAmmo;
    public float StartAmmo { get => _startAmmo; }
    private float _maxAmmo;
    private float _currentAmmo;
    public float CurrentAmmo { get => _currentAmmo; }

    private void Awake()
    {
        _anim = GameObject.Find("Player/Hands").GetComponent<Animator>();
        _firePos = transform.Find("FirePos").transform;
        _line = GetComponent<LineRenderer>();
        _startAmmo = _gunData.startAmmoRemain;
        _maxAmmo = _gunData.maxCapacity;
        _currentAmmo = _maxAmmo;
    }

    private void Update()
    {
        if(_currentAmmo <= 0)
        {
            _state = State.Empty;
        }
    }

    public void Fire(Action OnShotAnim)
    {
        if(_state == State.Ready && Time.time >= _lastShotTime + _gunData.shotDelay && _currentAmmo != 0)
        {
            _lastShotTime = Time.time;
            Shot();
            OnShotAnim?.Invoke();
        }
    }

    private void Shot()
    {
        _currentAmmo--;

        RaycastHit hit;
        Vector3 hitPos = Vector3.zero;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, _gunData.fireDistance) && hit.transform.CompareTag("Platform"))
        {
            hitPos = hit.point;
        }
        else
        {
            hitPos = Camera.main.transform.position + Camera.main.transform.forward * _gunData.fireDistance;
        }

        StartCoroutine(ShotEffect(hitPos));
    }

    public void Reload()
    {
        if (_state == State.Empty && _startAmmo >= 0)
        {
            _state = State.Reloading;
        }
    }

    public void ReloadComplete() //Animation Event
    {
        float decreaseAmmo;

        if (_startAmmo - _maxAmmo >= _maxAmmo) decreaseAmmo = _maxAmmo - _currentAmmo;
        else decreaseAmmo = _startAmmo - _currentAmmo;

        _currentAmmo += decreaseAmmo;
        _startAmmo -= decreaseAmmo;
        _state = State.Ready;
    }

    IEnumerator ShotEffect(Vector3 hitPos)
    {
        _line.SetPosition(0, _firePos.position);
        _line.SetPosition(1, hitPos);
        _line.enabled = true;
        yield return new WaitForSeconds(0.03f);
        _line.enabled = false;
    }
}
