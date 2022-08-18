using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody _rigid;

    private float _yRotate, _yRotateMove;
    private float _rotateSpeed = 500.0f;

    private float _jumpPower = 5f;
    private int _jumpCount = 0;
    private int _maxJumpCount = 1;
    private bool _isJump = false;

    private void Start()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    private void Update()   
    {
        Move();
        Rotation();
        Jump();
    }

    private void LateUpdate()
    {
        GameObject.Find("MainCam").transform.position = new Vector3(transform.position.x, transform.position.y + 0.484f, transform.position.z);
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(x, 0, z);
        dir = transform.TransformDirection(dir);
        _rigid.position += dir.normalized * _speed * Time.deltaTime;
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && _jumpCount < _maxJumpCount && !_isJump)
        {
            _jumpCount++;
            _isJump = true;

            _rigid.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
        }
    }

    private void Rotation()
    {
        _yRotateMove = Input.GetAxis("Mouse X") * _rotateSpeed * Time.deltaTime;

        _yRotate = transform.eulerAngles.y + _yRotateMove;

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, _yRotate, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Platform"))
        {
            if(collision.contacts[0].normal.y >= 0.7f)
            {
                _jumpCount = 0;
                _isJump = false;
            }
        }
    }
}
