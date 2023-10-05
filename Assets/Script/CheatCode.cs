using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SerializableDictionary.Scripts;
using TMPro;
public class CheatCode : MonoBehaviour
{
    [SerializeField]SerializableDictionary<string,UnityEvent> cheatSheet;
    [SerializeField] TMP_InputField console;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.BackQuote))
        {
            console.text = "";
            console.gameObject.SetActive(!console.IsActive());
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter) &&console.text!=null)
        {
          
            var cheat=  cheatSheet.Get(console.text);
            if (cheat != null) cheat.Invoke();
            console.text = "";
        }
    }
}
