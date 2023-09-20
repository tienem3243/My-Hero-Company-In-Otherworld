using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;
public class IncomeQuester : CustomBehaviour
{
    Animator anim;
   
    private void Start()
    {
        TryGetComponent<Animator>(out anim);
    }
    public void Come()
    {
        anim.SetTrigger("Come");
    }
    public void Out()
    {
        anim.SetTrigger("Out");
    }

  
}
