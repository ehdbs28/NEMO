using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private Gun _gun;
    private Animator _anim;

    private float _xRotate, _xRotateMove;
    private float _rotateSpeed = 500f;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        HandRotate();
        Attack();
        Reload();
    }

    private void Attack()
    {
        if (Input.GetMouseButton(0))
        {
            _gun = GunSwapManager.Instance.CurrentGun;

            _gun.Fire(() => { _anim.SetTrigger("IsAttack"); });
        }
    }

    private void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _gun = GunSwapManager.Instance.CurrentGun;
            if (_gun.state == Gun.State.Reloading && _gun.StartAmmo <= 0) return;

            _anim.SetBool("IsReloading", true);
            _gun.Reload();
        }
    }

    public void ReloadComplete() //Animation Event
    {
        _gun = GunSwapManager.Instance.CurrentGun;
        _gun.ReloadComplete();
        _anim.SetBool("IsReloading", false);
    }

    private void HandRotate()
    {
        _xRotateMove = Input.GetAxis("Mouse Y") * Time.deltaTime * _rotateSpeed;
        _xRotate += _xRotateMove;
        _xRotate = Mathf.Clamp(_xRotate, -80f, 80f);

        transform.eulerAngles = new Vector3(-_xRotate, transform.eulerAngles.y, 0);
    }
}
