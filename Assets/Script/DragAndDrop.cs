using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] InputAction mouseClick;
    private Camera mainCamera;
    [SerializeField] private LayerMask layerMask;
    private string oldTalble="";
    private string currentTable="";

    private void Awake()
    {
        mainCamera = Camera.main;
    }
    private void OnEnable()
    {
        mouseClick.Enable();
        mouseClick.performed += MousePressed;

    }
    private void MousePressed(InputAction.CallbackContext obj)
    {
        Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(mouseWorld, Vector3.forward);
       
        if (hit.collider != null&& hit.collider.gameObject.CompareTag("Doccument"))
        {
            Vector2 offset = mouseWorld - (Vector2)hit.collider.gameObject.transform.position;
            StartCoroutine(DragUpdate(hit.collider.gameObject,offset));
        }
    }

    private IEnumerator DragUpdate(GameObject clickedObject, Vector3 offset)
    {
        IDraggable iDragComponent= default(IDraggable);
        clickedObject.transform.parent?.TryGetComponent<IDraggable>(out iDragComponent);
    
        iDragComponent?.OnDragStart();
        while (mouseClick.ReadValue<float>() != 0)
        {
            iDragComponent?.OnDrag();
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            RaycastHit2D[] hit = Physics2D.RaycastAll(mouseWorld, Vector3.forward);
            foreach (var item in hit)
            {
                if (item.collider.gameObject.CompareTag("WorkingTable"))
                {
                    oldTalble = currentTable;
                    string name1 = item.collider.gameObject.name;
                    currentTable = name1;       
                    if(currentTable!=oldTalble)
                    offset = Vector3.zero;
                }
            }
            Vector3 clickPos = mouseWorld - offset;
            Vector3 pos = clickedObject.transform.parent.position;
            clickedObject.transform.parent.position = new Vector3(clickPos.x, clickPos.y,pos.z);
            yield return new WaitForFixedUpdate();
        }
        iDragComponent?.OnDragEnd();
    }
}
