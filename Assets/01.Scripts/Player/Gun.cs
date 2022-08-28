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

    private AudioSource _audioSource;
    
    private State _state;
    public State state { get; set; }

    private Transform _firePos;
    private Transform _fxPos;
    private LineRenderer _line;
    private BulletFx _bulletFx;
    private HitFx _hitFx;

    private float _lastShotTime;

    private float _damage;
    public float Damage { get => _damage; set => _damage = value; }

    private float _remainAmmo;
    public float RemainAmmo { get => _remainAmmo; set => _remainAmmo = value; }

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
        _audioSource = GameObject.Find("Hands").GetComponent<AudioSource>();
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
        _damage = Mathf.RoundToInt(_gunData.damage + PlayerManager.Instance.DamageIncrease);
        _remainAmmo = Mathf.RoundToInt(_gunData.startAmmoRemain + PlayerManager.Instance.DamageIncrease);
        _maxAmmo = Mathf.RoundToInt(_gunData.maxCapacity + PlayerManager.Instance.AmmoIncrease);
        _currentAmmo = _maxAmmo;

        AudioManager.Instance.PlaySFX(_audioSource, AudioManager.Instance.Clips["GunCock"]);
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
        AudioManager.Instance.PlaySFX(_audioSource, AudioManager.Instance.Clips["GunShot"]);

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
                monster.OnDamage(_damage, hitPos, hit.normal);
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
        if (_state == State.Empty && _remainAmmo > 0)
        {
            _state = State.Reloading;
        }
    }

    public void ReloadComplete() //Animation Event
    {
        if(_remainAmmo > 0)
        {
            AudioManager.Instance.PlaySFX(_audioSource, AudioManager.Instance.Clips["GunReload"]);

            float decreaseAmmo;

            if (_remainAmmo >= _maxAmmo) decreaseAmmo = _maxAmmo - _currentAmmo;
            else decreaseAmmo = _remainAmmo - _currentAmmo;

            _currentAmmo += decreaseAmmo;
            _remainAmmo -= decreaseAmmo;
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
