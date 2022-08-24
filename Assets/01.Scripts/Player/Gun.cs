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
    
    private State _state;
    public State state { get; set; }

    private Transform _firePos;
    private Transform _fxPos;
    private LineRenderer _line;
    private BulletFx _bulletFx;
    private HitFx _hitFx;

    private float _lastShotTime;

    private float _startAmmo;
    public float StartAmmo { get => _startAmmo; }
    private float _maxAmmo;
    private float _currentAmmo;
    public float CurrentAmmo { get => _currentAmmo; }
    private bool _isAiming = false;
    public bool IsAiming { get => _isAiming; set => _isAiming = value; }

    private void Awake()
    {
        _firePos = transform.Find("FirePos").transform;
        _fxPos = transform.Find("FxPos").transform;
        _line = GetComponent<LineRenderer>();
        InitSetting();
    }

    private void Update()
    {
        if(_currentAmmo <= 0)
        {
            _state = State.Empty;
        }
    }

    public void InitSetting()
    {
        _startAmmo = _gunData.startAmmoRemain;
        _maxAmmo = _gunData.maxCapacity;
        _currentAmmo = _maxAmmo;
    }

    public void Fire(Action OnShotAnim, LayerMask targetLayer)
    {
        if(_state == State.Ready && Time.time >= _lastShotTime + _gunData.shotDelay && _currentAmmo != 0)
        {
            _lastShotTime = Time.time;
            Shot(targetLayer);
            OnShotAnim?.Invoke();
        }
    }

    private void Shot(LayerMask targetLayer)
    {
        _currentAmmo--;

        CameraManager.Instance.Shake(2, 0.03f);

        _bulletFx = PoolManager.Instance.Pop("BulletFx") as BulletFx;
        _bulletFx.transform.position = _fxPos.position;
        _bulletFx.transform.rotation = _fxPos.rotation;

        RaycastHit hit;
        Vector3 hitPos = Vector3.zero;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, _gunData.fireDistance, targetLayer))
        {
            hitPos = hit.point;

            Monster monster = hit.transform.GetComponent<Monster>();
            if(monster != null && !monster._dead)
            {
                monster.OnDamage(GunSwapManager.Instance.CurrentGun._gunData.damage, hitPos, hit.normal);
            }
        }
        else
        {
            hitPos = Camera.main.transform.position + Camera.main.transform.forward * _gunData.fireDistance;
        }

        _hitFx = PoolManager.Instance.Pop("HitFx") as HitFx;
        _hitFx.transform.position = hitPos;

        StartCoroutine(ShotEffect(hitPos));
    }

    public void Reload()
    {
        if (_state == State.Empty && _startAmmo > 0)
        {
            _state = State.Reloading;
        }
    }

    public void ReloadComplete() //Animation Event
    {
        if(_startAmmo > 0)
        {
            float decreaseAmmo;

            if (_startAmmo - _maxAmmo >= _maxAmmo) decreaseAmmo = _maxAmmo - _currentAmmo;
            else decreaseAmmo = _startAmmo - _currentAmmo;

            _currentAmmo += decreaseAmmo;
            _startAmmo -= decreaseAmmo;
            _state = State.Ready;
        }
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
