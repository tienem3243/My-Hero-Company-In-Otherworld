
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class QuesterGeneration : MonoBehaviour
{
    [SerializeField] TimelineController timelineController;
    [SerializeField] IndentificationCard cardIn;
    public List<Quester> quester;
    [SerializeField] IncomeQuester questerVisual;
    int currentQuester;
    private void Start()
    {
        CreatQuester();
      
    }
 
    [ContextMenu("next")]
    private void NextQuester()
    {
        if (currentQuester != 0)
            timelineController.PlayIndex(0);

      
 
        Invoke(nameof(QuesterCome), currentQuester==0?0f:(float)timelineController.Timeline[0].duration);
        currentQuester++;
    }

    private void QuesterCome()
    {
        LoadQuester(currentQuester);
        timelineController.PlayIndex(1);
    }

    void LoadQuester(int i)
    {

        questerVisual.SetVisual(quester[i].Portrail);
    }
    private void CreatQuester()
    {
        Object[] sprite = Resources.LoadAll("Portrail", typeof(Sprite));
       
        for (int i = 0; i < 4; i++)
        {
            IndentificationCard inde = Instantiate(cardIn);
            Sprite portrail = (Sprite)sprite[Random.Range(0, sprite.Length)];
            var newQuester = new Quester(portrail, "beta", new List<IndentificationCard> { inde });
            quester.Add(newQuester);
        }
    }
}
