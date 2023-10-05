using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetButton : MonoBehaviour
{
    public GameObject    enemy;
    public void SelectEnemy()
    {
        BattleStateMachine.Instance.Input2(enemy);
        Debug.Log("Cast");
    }
}
