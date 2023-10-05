using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickAbleObect : MonoBehaviour
{
    [SerializeField] UnityEvent Event;
    private void OnMouseDown()
    {
        Event.Invoke();
    }
}
