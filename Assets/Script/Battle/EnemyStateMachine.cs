using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public TurnState currentState;
    public Enemy enemy;
    public BattleStateMachine BSM;
    private bool isActionStarted;

    public GameObject HeroToAttack { get; internal set; }

    public bool isReady;

    public float turnCounter;

    public enum TurnState { PROCESSING, CHOOSEACTION, WAITING, ACTION, DEAD, PAUSE }
    private void Update()
    {
        if (currentState == TurnState.PAUSE) return;
        else
            switch (currentState)
            {
                case TurnState.PROCESSING:
                    
                    UpdateTurnCounter();
                    break;
                case TurnState.CHOOSEACTION:

                    ChooseAction();
                    currentState = TurnState.WAITING;
                    break;
                case TurnState.WAITING:
                    break;
                case TurnState.ACTION:
                    StartCoroutine(TimeforAction());
                    break;
                case TurnState.DEAD:
                    break;
            }
    }

    private IEnumerator TimeforAction()
    {
        if (isActionStarted)
        {
            yield break;
        }
        isActionStarted = true;
        //wait a bit
        //do damage
        //animate 
        //remove performer from bsm
        //reset bsm->wait

        yield return new WaitForSeconds(5f);
        BSM.PerformList.RemoveAt(0);
        BSM.battleState = BattleStateMachine.TurnState.PROCESSING;
        isActionStarted = false;

        currentState = TurnState.PROCESSING;
    }

    void ChooseAction()
    {
        Debug.Log(enemy.name + "action");
        HandleTurn myTurn = new();
        myTurn.attackerName = enemy.name;
        myTurn.Type = "Enemy";
        myTurn.attackerObj = this.gameObject;
        myTurn.targetObj = BSM.HeroInGame[Random.Range(0, BSM.HeroInGame.Count - 1)].gameObject;
        BSM.battleState = BattleStateMachine.TurnState.TAKEACTION;
        BSM.CollectAction(myTurn);

    }
    private void Start()
    {
        currentState = TurnState.PROCESSING;
        BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();

    }
    public void UpdateTurnCounter()
    {
        turnCounter += enemy.speed * Time.deltaTime;
        if (turnCounter >= BSM.TURN_LIMIT)
        {
            isReady = true;
            turnCounter = 0;
            BSM.ownerCurrentTurn = enemy.name;
            BSM.battleState = BattleStateMachine.TurnState.ENEMYTURN;
            currentState = EnemyStateMachine.TurnState.CHOOSEACTION;
        }
    }

    

   
}
