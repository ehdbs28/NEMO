using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance = null;

    private CinemachineVirtualCamera _mainVCam;

    private CinemachineBasicMultiChannelPerlin _mainPerlin;

    private const int _backPriority = 10;
    private const int _frontPriority = 15;

    private CinemachineVirtualCamera _activeCam = null;
    private CinemachineBasicMultiChannelPerlin _activePerlin = null;

    private void Start()
    {
        _mainVCam = GameObject.Find("MainCam").GetComponent<CinemachineVirtualCamera>();

        _mainPerlin = _mainVCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        SetToMain();
    }

    public void SetToMain()
    {
        _mainVCam.Priority = _frontPriority;

        _activeCam = _mainVCam;
        _activePerlin = _mainPerlin;
    }

    public void Shake(float intensity, float endTime)
    {
        if (_activeCam == null || _activePerlin == null) return;
        StopAllCoroutines();
        StartCoroutine(ShakeCoroutine(intensity, endTime));
    }

    IEnumerator ShakeCoroutine(float intensity, float endTime)
    {
        _activePerlin.m_AmplitudeGain = intensity;
        float currentTime = 0f;
        while(currentTime < endTime)
        {
            yield return new WaitForEndOfFrame();
            if (_activePerlin == null) break;

            _activePerlin.m_AmplitudeGain = Mathf.Lerp(intensity, 0, currentTime / endTime);
            currentTime += Time.deltaTime;
        }
        if(_activePerlin != null)
        {
            _activePerlin.m_AmplitudeGain = 0;
        }
    }
}
