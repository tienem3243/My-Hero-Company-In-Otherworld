using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroStateMachine : MonoBehaviour
{
    public TurnState currentState;

    public Hero character;
    public BattleStateMachine BSM;
    private bool isActionStarted;
    public bool isReady;
    public float turnCounter;

    public GameObject HeroToAttack { get; internal set; }

    public enum TurnState { PROCESSING,ADDTOLIST, WAITING,SELECTING, ACTION, DEAD,PAUSE }

    private void Update()
    {
        if (currentState == TurnState.PAUSE) return; 
        switch (currentState)
        {
            case TurnState.PROCESSING:
                UpdateTurnCounter();
                break;
            case TurnState.ADDTOLIST:
                BSM.HeroToManage.Add(this.gameObject);
                currentState = TurnState.WAITING;
                break;
            case TurnState.WAITING:
                //idlestate
                break;
            case TurnState.SELECTING:
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
        Debug.Log(gameObject+"attack" + HeroToAttack);
        yield return new WaitForSeconds(5f);
        BSM.PerformList.RemoveAt(0);
        BSM.battleState = BattleStateMachine.TurnState.PROCESSING;
        isActionStarted = false;
       
        currentState = TurnState.PROCESSING;
    }

    
    private void Start()
    {
        currentState = TurnState.PROCESSING;
        BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();

    }
    public void UpdateTurnCounter()
    {
        turnCounter += character.speed*Time.deltaTime;
        if (turnCounter >=BSM.TURN_LIMIT)
        {
            isReady = true;
            turnCounter = 0;
            BSM.ownerCurrentTurn = character.name;
            BSM.battleState = BattleStateMachine.TurnState.PLAYERTURN;
            currentState = TurnState.ADDTOLIST;

        }
    }

  
}
