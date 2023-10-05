
using DG.Tweening;
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
    bool isComing;
    int currentQuester;
    [SerializeField] Transform cardPlacement;

    private void Start()
    {
        CreatQuester();
        Invoke(nameof(QuesterCome), currentQuester == 0 ? 0f : (float)timelineController.Timeline[0].duration);
    }

 
    public void NextQuester()
    {
        if (isComing || currentQuester > quester.Count-1) return;
        if (currentQuester != 0)
            timelineController.PlayIndex(0);
        isComing = true;
        Invoke(nameof(QuesterCome), currentQuester == 0 ? 0f : (float)timelineController.Timeline[0].duration);
        currentQuester++;
    }

    private void QuesterCome()
    {
        isComing = false;
        LoadQuester(currentQuester);
        timelineController.PlayIndex(1);
    }

    void LoadQuester(int i)
    {

        questerVisual.SetVisual(quester[i].Portrail);
    }

    public void GetQuesterDoccument()
    {
        for (int i = 0; i < quester[currentQuester].card.Count; i++)
        {
            IndentificationCard indentificationCard = quester[currentQuester].card[i];
            indentificationCard.transform.position = questerVisual.transform.position;
            indentificationCard.gameObject.SetActive(true);
            indentificationCard.transform.DOMove(cardPlacement.position, 0.5f);
        }
    }
    private void CreatQuester()
    {
        Object[] sprite = Resources.LoadAll("Portrail", typeof(Sprite));
        //generate card
        IndentificationCard inde = Instantiate(cardIn);
        inde.gameObject.SetActive(false);
        Sprite portrail = (Sprite)sprite[Random.Range(0, sprite.Length)];
        inde.portrail.sprite = portrail;
        var newQuester = new Quester(portrail, "beta", new List<IndentificationCard> { inde });
        quester.Add(newQuester);

    }
}
