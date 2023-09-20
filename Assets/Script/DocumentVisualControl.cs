using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentVisualControl : MonoBehaviour
{
    public enum DoccumentState { FULLVERSION,TABLEVERSION}
    [SerializeField] GameObject fullVersion;
    [SerializeField] GameObject tableVersion;
    public void ExpandFullVersion(DoccumentState state)
    {
        switch (state)
        {
            case DoccumentState.FULLVERSION:
                fullVersion.SetActive(true);
                tableVersion.SetActive(false);
                break;
            case DoccumentState.TABLEVERSION:
                fullVersion.SetActive(false);
                tableVersion.SetActive(true);
                break;
        }
      
    }
   
}
