using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour

{
    [SerializeField] private float _speed;

    private Rigidbody _rigid;

    private AudioSource _audioSource;

    private float _yRotate, _yRotateMove;
    private float _rotateSpeed = 500.0f;

    private float _jumpPower = 5f;
    private int _jumpCount = 0;
    private int _maxJumpCount = 1;
    private bool _isJump = false;

    private void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
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

        if ((x != 0 || z != 0) && !_audioSource.isPlaying && !_isJump)
        {
            _audioSource.volume = 1f;
            AudioManager.Instance.PlaySFX(_audioSource, AudioManager.Instance.Clips["FootStep"]);
        }

        Vector3 dir = new Vector3(x, 0, z);
        dir = transform.TransformDirection(dir);
        _rigid.position += dir.normalized * (_speed + PlayerManager.Instance.SpeedIncrease) * Time.deltaTime;
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && _jumpCount < _maxJumpCount && !_isJump)
        {
            _audioSource.volume = 0.5f;
            AudioManager.Instance.PlaySFX(_audioSource, AudioManager.Instance.Clips["Jump"]);

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Item"))
        {
            Item item = other.transform.GetComponent<Item>();
            if (item != null)
            {
                AudioManager.Instance.UseItemSound();
                item.UseItem();
            }
        }
        else if (other.transform.CompareTag("InCollider"))
        {
            if (!GameManager.Instance.IsShop && other.transform.parent.name == "Award")
            {
                AudioManager.Instance.ChestAudio();

                int gunNum = Random.Range(0, GunManager.Instance.GunList.Count);
                Item gunItem = PoolManager.Instance.Pop($"{GunManager.Instance.GunList[gunNum].name.Replace("-", "")}Item") as Item;
                gunItem.transform.position = new Vector3(-6.49f, 1.494f, -15.522f);
            }

            GameManager.Instance.IsShop = true;
        }
        else if (other.transform.CompareTag("OutCollider"))
        {
            if (GameManager.Instance.IsShop && other.transform.parent.name == "Award") AudioManager.Instance.ChestAudio();

            GameManager.Instance.IsShop = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("SellThing"))
        {
            SellAble sellAble = other.transform.GetComponent<SellAble>();

            GameObject text = GameObject.Find("Canvas").transform.Find("SellInfoTxt").gameObject;
            text.SetActive(true);
            sellAble._infoTxt = text.transform.GetComponent<TextMeshProUGUI>();

            if (sellAble != null)
            {
                sellAble.IsSellAble = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("SellThing"))
        {
            SellAble sellAble = other.transform.GetComponent<SellAble>();
            GameObject text = GameObject.Find("Canvas").transform.Find("SellInfoTxt").gameObject;
            text.SetActive(false);
            sellAble._infoTxt = null;
            if (sellAble != null)
            {
                sellAble.IsSellAble = false;
            }
        }
    }
}
