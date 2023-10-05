using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    protected int diceCost;
    protected string abilityName;
    protected string detail;
    public virtual void Use()
    {

    }
    public virtual void Use(Character source,Character target)
    {

    }
}
