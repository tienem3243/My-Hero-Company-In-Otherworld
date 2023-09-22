using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoccumentManager : MonoBehaviourSingleton<DoccumentManager>
{
    private const float originalDepth = 1f;
    public List<Doccument> doccuments = new();
    public Doccument currentDrag;
   
    public void MoveToTop(Doccument doccument)
    {
        doccuments.Remove(doccument);
        doccuments.Add(doccument);
        
        RecalculatePos();
    }

    private void RecalculatePos()
    {
        float i = originalDepth;
        doccuments.ForEach(x =>
        {
            var trans = x.transform.position;
            trans.z = i;
            x.transform.position = trans;
            i -= 0.2f;
        });
    }
    public void Reset()
    {
        doccuments.Clear();
        currentDrag = null;
    }
}
