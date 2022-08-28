using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private float _distance = 10f;

    private Vector3 _initPos;

    private Animator _anim;

    private void Start()
    {
        _anim = GetComponentInParent<Animator>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _anim.enabled = false;
        _initPos = transform.position;
        _distance = Vector3.Distance(transform.position, Camera.main.transform.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 touchPosition = eventData.position;

        Vector3 touchPositionInWorld = Camera.main.ScreenToWorldPoint(
            new Vector3(
                touchPosition.x,
                touchPosition.y,
                _distance - 2)
        );

        transform.position = touchPositionInWorld;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _anim.enabled = true;
        transform.position = _initPos;
    }
}
