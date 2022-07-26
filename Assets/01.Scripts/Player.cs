using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody _rigid;

    private float _yRotate, _yRotateMove;
    private float _rotateSpeed = 500.0f;

    private void Start()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    private void Update()   
    {
        Move();
        Rotation();
    }

    private void LateUpdate()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y + 0.484f, transform.position.z);
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(x, 0, z);
        dir = transform.TransformDirection(dir);
        _rigid.MovePosition(transform.position + dir.normalized * _speed * Time.deltaTime);
    }

    private void Rotation()
    {
        _yRotateMove = Input.GetAxis("Mouse X") * _rotateSpeed * Time.deltaTime;

        _yRotate = transform.eulerAngles.y + _yRotateMove;

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, _yRotate, 0);
    }
}
