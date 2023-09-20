
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class QuesterGeneration : MonoBehaviour
{
    [SerializeField] IndentificationCard cardIn;
    public List<Quester> quester;
   
    private void Start()
    {
        CreatQuester();
    }

    private void CreatQuester()
    {
        Object[] sprite = Resources.LoadAll("Portrail",typeof(Sprite));
        Sprite portrail = (Sprite)sprite[Random.Range(0, sprite.Length)];
        List<Doccument> arrayList = new();
        for (int i = 0; i < 4; i++)
        {
            IndentificationCard inde= Instantiate(cardIn);

            var newQuester= new Quester(portrail, "beta", new List<IndentificationCard> { inde});
            quester.Add(newQuester);
        }
    }
}
