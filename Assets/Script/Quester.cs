using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quester
{
    [SerializeField] Sprite sprite;
    [SerializeField] string name;
    [SerializeField] List<IndentificationCard> card;

    public Quester(Sprite sprite, string name, List<IndentificationCard> card)
    {
        this.sprite = sprite;
        this.name = name;
        this.card = card;
    }
}