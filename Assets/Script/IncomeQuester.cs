using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;
public class IncomeQuester : CustomBehaviour
{
    Animator anim;
    [SerializeField] SpriteRenderer shade;

    private void Start()
    {
        TryGetComponent<Animator>(out anim);
    }
    public Renderer GetVisual()
    {
        return renderer;
    }
    public void SetVisual(Sprite sprite)
    {
        SpriteRenderer spriteRenderer = (SpriteRenderer)renderer;
        spriteRenderer.sprite = sprite;
        shade.sprite = sprite;
    }
  
}
