using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private Transform _firePos;
    private LineRenderer _line;

    private float _lastShotTime;

    private void Awake()
    {
        _anim = GameObject.Find("Player/Hands").GetComponent<Animator>();
        _firePos = transform.Find("FirePos").transform;
        _line = GetComponent<LineRenderer>();
    }

    public void Fire()
    {
        if(_state == State.Ready && Time.time >= _lastShotTime + _gunData.shotDelay)
        {
            _lastShotTime = Time.time;
            Shot();
        }
    }

    private void Shot()
    {
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
