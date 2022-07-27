using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private float _xRotate, _xRotateMove;
    private float _rotateSpeed = 500f;

    private void Update()
    {
        HandRotate();
    }

    private void HandRotate()
    {
        _xRotateMove = Input.GetAxis("Mouse Y") * Time.deltaTime * _rotateSpeed;
        _xRotate += _xRotateMove;
        _xRotate = Mathf.Clamp(_xRotate, -80f, 80f);

        transform.eulerAngles = new Vector3(-_xRotate, transform.eulerAngles.y, 0);
    }
}
