using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quester
{
    [SerializeField] Sprite portrail;
    [SerializeField] string name;
    [SerializeField] List<IndentificationCard> card;

    public Quester(Sprite sprite, string name, List<IndentificationCard> card)
    {
        this.portrail = sprite;
        this.name = name;
        this.card = card;
    }

    public Sprite Portrail { get => portrail; }
    public string Name { get => name; }
    public List<IndentificationCard> Card { get => card; }
}