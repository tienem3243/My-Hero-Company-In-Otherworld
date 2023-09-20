using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cachet :Doccument
{
    [SerializeField]private GameObject mark;
    [SerializeField] private Transform markPoint;
    [SerializeField] private LayerMask layer;
    Transform currentMarkTarget;
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)&&currentMarkTarget!=null)
        {
            var martPrintPos = new Vector3(markPoint.position.x, markPoint.position.y, currentMarkTarget.position.z);
           var obj= Instantiate(mark,martPrintPos , Quaternion.identity, currentMarkTarget.transform);
        
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        if (collision != null&&UbhUtil.Contains(layer,collision.gameObject.layer))
            currentMarkTarget = collision.transform;
    
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        currentMarkTarget = null;
    }

  
}
