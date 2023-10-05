using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class BattleStateMachine : MonoBehaviourSingleton<BattleStateMachine> 
{
    public enum TurnState { PROCESSING, ENEMYTURN, PLAYERTURN, TAKEACTION, PERFORMACTION }
    public enum HeroGUI { ACTIVATE, WAITING, INPUT1, INPUT2, DONE }
    public enum TURNOWNER { ENEMY, PLAYER }

    public GameObject AttackPanel;
    public HeroGUI HeroInput;
    public TurnState battleState;
    public string ownerCurrentTurn;
    public List<HandleTurn> PerformList = new();
    public List<HeroStateMachine> HeroInGame = new();
    public List<EnemyStateMachine> EnemyInGame = new();
   
    public List<GameObject> HeroToManage = new();
    public HandleTurn HeroChoice;
    public Slider[] speedBar;
    public GameObject EnemyAttackPanel;
   
    public GameObject ButtonEnemy;

    public int TURN_LIMIT = 200;
  
    private void Start()
    {
       
        AttackPanel.SetActive(false);
        EnemyAttackPanel.SetActive(false);
        battleState = TurnState.PROCESSING;
        InitEnemyButton();
    }
    private void Update()
    {
       
        speedBar[0].value = (float)(EnemyInGame[0].turnCounter / TURN_LIMIT);
        speedBar[1].value = (float)(EnemyInGame[1].turnCounter / TURN_LIMIT);
        speedBar[2].value = (float)(HeroInGame[0].turnCounter / TURN_LIMIT);
        speedBar[3].value = (float)(HeroInGame[1].turnCounter / TURN_LIMIT);
        switch (battleState)
        {
            case TurnState.PROCESSING:
                PerformList.Clear();

                EnemyInGame.ForEach(x => x.currentState = EnemyStateMachine.TurnState.PROCESSING);
                HeroInGame.ForEach(x => x.currentState = HeroStateMachine.TurnState.PROCESSING);
                // Check if any enemy is ready to take a turn
                bool anyEnemyReady = EnemyInGame.Any(e => e.isReady);
                if (anyEnemyReady)
                {
                    battleState = TurnState.ENEMYTURN;
                    break;
                }

                // Check if any hero is ready to take a turn
                bool anyHeroReady = HeroInGame.Any(h => h.isReady);
                if (anyHeroReady)
                {
                    battleState = TurnState.PLAYERTURN;
                }
                break;



            case TurnState.ENEMYTURN:
                EnemyAttackPanel.SetActive(false);
                AttackPanel.SetActive(false);
                EnemyInGame.ForEach(x =>
                {
                    if(x.name!=ownerCurrentTurn)
                    x.currentState =  EnemyStateMachine.TurnState.PAUSE;
                    else
                    {
                        x.isReady = false;
                      
                    }
                 
                });
                HeroInGame.ForEach(x => x.currentState = HeroStateMachine.TurnState.PAUSE);
                break;
            case TurnState.PLAYERTURN:
               
                HeroInGame.ForEach(x =>
                {
                    if (x.name != ownerCurrentTurn)
                        x.currentState = HeroStateMachine.TurnState.PAUSE;
                    else
                    {
                        x.isReady = false;
                      
                    }
                });
                EnemyInGame.ForEach(x => x.currentState = EnemyStateMachine.TurnState.PAUSE);
               
              
                break;

           
            case TurnState.TAKEACTION:


                GameObject performer = GameObject.Find(PerformList[0].attackerName);
                if (PerformList[0].Type == "Enemy")
                {
             
                    EnemyStateMachine ESM = performer.GetComponent<EnemyStateMachine>();
                    ESM.HeroToAttack = PerformList[0].targetObj;
                    ESM.currentState = EnemyStateMachine.TurnState.ACTION;

                }
                if (PerformList[0].Type == "Hero")
                {
                    HeroStateMachine HSM = performer.GetComponent<HeroStateMachine>();
                    HSM.HeroToAttack = PerformList[0].targetObj;
                    HSM.currentState = HeroStateMachine.TurnState.ACTION;
                }
                battleState = TurnState.PERFORMACTION;
                break;
            case TurnState.PERFORMACTION:
                //idle wait for action done
                break;
        }
       
        switch (HeroInput)
        {
            case HeroGUI.ACTIVATE:
                if (HeroToManage.Count > 0)
                {
                    EnemyAttackPanel.SetActive(true);
                    AttackPanel.SetActive(true);
                    HeroChoice = new();
                  
                    HeroInput = HeroGUI.WAITING;
                }

                break;
            case HeroGUI.WAITING:
                //idle state
                break;
            case HeroGUI.INPUT1:
                break;
            case HeroGUI.INPUT2:
                break;
            case HeroGUI.DONE:
                
                HeroInputDone();
                break;
        }
    }


    public void CollectAction(HandleTurn turn)
    {
        PerformList.Add(turn);
        battleState = TurnState.TAKEACTION;

    }
    public void Input1()
    {

        HeroChoice.attackerName = HeroToManage[0].name;
        HeroChoice.attackerObj = HeroToManage[0];
        HeroChoice.Type = "Hero";
        AttackPanel.SetActive(false);
        EnemyAttackPanel.SetActive(true);
    }
    public void Input2(GameObject chosenEnermy)
    {
        if (HeroToManage.Count <= 0) return;
        HeroChoice.targetObj = chosenEnermy;
        HeroInput = HeroGUI.DONE;
    }
    public void HeroInputDone()
    {
        PerformList.Add(HeroChoice);
        EnemyAttackPanel.SetActive(false);
        HeroToManage.RemoveAt(0);
        HeroInput = HeroGUI.ACTIVATE;
        battleState = TurnState.TAKEACTION;
    }
    public void InitEnemyButton()
    {
        foreach (var item in EnemyInGame)
        {
            var button = Instantiate(ButtonEnemy, EnemyAttackPanel.transform);
            button.GetComponent<EnemyTargetButton>().enemy = item.gameObject;
        }
    }
  

}
