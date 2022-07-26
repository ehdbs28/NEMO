using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    private float _xRotate, _yRotate, _xRotateMove, _yRotateMove;
    public float _rotateSpeed = 500.0f;

    private void Update()
    {
        CameraRotation();
    }

    private void CameraRotation()
    {
        _xRotateMove = -Input.GetAxis("Mouse Y") * Time.deltaTime * _rotateSpeed;
        _yRotateMove = Input.GetAxis("Mouse X") * Time.deltaTime * _rotateSpeed;

        _yRotate = transform.eulerAngles.y + _yRotateMove;
        _xRotate += _xRotateMove;

        _xRotate = Mathf.Clamp(_xRotate, -80f, 80f);

        transform.eulerAngles = new Vector3(_xRotate, _yRotate, 0);
    }
}
