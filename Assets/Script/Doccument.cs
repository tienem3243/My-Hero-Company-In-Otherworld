using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Doccument : CustomBehaviour, IDraggable
{
    private bool isDragging;
    public bool IsDragging { get => isDragging; }

    protected virtual void Start()
    {

    }

    public void OnDragStart()
    {
        Debug.Log("Start Drag");
        DoccumentManager.Instance.currentDrag = this;
        DoccumentManager.Instance.MoveToTop(this);
     
    }

    public void OnDrag()
    {
        isDragging = true;

        

    }

    public void OnDragEnd()
    {
       
       DoccumentManager.Instance.currentDrag = null;
        var hits =Physics2D.RaycastAll(transform.position, Vector3.forward);
       isDragging = false;
        foreach(var hit in hits)
        {
            if (hit.transform.gameObject.CompareTag("ReturnZone"))
            {
                DoccumentManager.Instance.doccuments.Remove(this);
                TryGetComponent(out SortingGroup sortGroup);
                sortGroup.enabled = true;
                collider2D.enabled = false;
                transform.position = new Vector3(transform.position.x, transform.position.y, -0.1f);
                transform.DOMove(transform.position + Vector3.down * 4, 0.7f)
                    .OnComplete(() => gameObject.SetActive(false));
            }

        }
       
    }

    
}
