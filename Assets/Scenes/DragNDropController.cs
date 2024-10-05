using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragNDropController : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private float scaleCoef;
    [SerializeField] private float returnSpeed;
    [SerializeField] private int actionType;
    private Vector3 startPosition;
    private bool isDragging = false;
    private RaycastHit _hit;
    private Ray _ray;

    // for checking GO
    private Vector3 vectorDown = Vector3.down;
    private float distance = 10;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void OnMouseDown()
    {
        isDragging = true;
        transform.localScale *= scaleCoef;
        mainCamera = Camera.main;
    }

    private void OnMouseUp()
    {
        isDragging = false;
        transform.localScale /= scaleCoef;
        CheckGameZone();
    }

    private void CheckGameZone()
    {
        RaycastHit[] hitsInDown = Physics.RaycastAll(transform.position, vectorDown, distance);
        foreach (RaycastHit item in hitsInDown)
        {
            // have to check dependency
            if (item.transform.GetComponent<CharacterMovement>())                                   
            {
                item.transform.GetComponent<CharacterMovement>().MovingByAlgorithm(actionType);
                Destroy(gameObject);
            }
        }
    }


    private void Update()
    {
        if (isDragging)
        {
            _ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _hit))
            {
                Vector3 pos = new Vector3(_hit.point.x, transform.position.y, _hit.point.z);
                transform.position = Vector3.MoveTowards(transform.position, pos, returnSpeed * 2 * Time.deltaTime);
            }
            else
            {
                isDragging = false;
            }
        }

        else if (isDragging == false && transform.position != startPosition && transform.gameObject != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, returnSpeed * Time.deltaTime);
        }
    }
}
