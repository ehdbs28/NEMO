using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Entity
{
    [SerializeField] private Slider _hpSlider;
    [SerializeField] private GameObject _redLight;

    private void Start()
    {
        _redLight.SetActive(false);
    }

    private void Update()
    {
        _hpSlider.value = _health;
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if(_health >= 0 && !_dead)
        {
            StopAllCoroutines();
            StartCoroutine(DamageEffect());

            base.OnDamage(damage, hitPoint, hitNormal);
        }
    }

    public override void Die()
    {
        _hpSlider.gameObject.SetActive(false);
        UIManager.Instance.GameOver();
        base.Die();
    }

    IEnumerator DamageEffect()
    {
        _redLight.SetActive(false);

        _redLight.SetActive(true);
        yield return new WaitForSeconds(0.03f);
        _redLight.SetActive(false);
    }
}
