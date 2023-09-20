using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkingTable : CustomBehaviour
{
    [SerializeField] LayerMask layermask;
    List<GameObject> OnTableObject= new();
    public bool expandDocument;
    private BoxCollider2D col;
    private void Start()
    {
        col = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (UbhUtil.Contains(layermask,collision.gameObject.layer))
        {
            collision.transform.TryGetComponent<DocumentVisualControl>(out var visual);
            OnTableObject.Add(collision.gameObject);
            visual?.ExpandFullVersion(expandDocument ? DocumentVisualControl.DoccumentState.FULLVERSION : DocumentVisualControl.DoccumentState.TABLEVERSION);
        }

    }
   
}
