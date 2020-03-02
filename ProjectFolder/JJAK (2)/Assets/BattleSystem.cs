using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState {
    START,
    PLAYERTURN,
    ENEMYTURN,
    WON,
    LOST
}

public class BattleSystem : MonoBehaviour
{
    public BattleState state;
    public GameObject enemyPrefab;
    public GameObject playerPrefab;
    public Transform playerBattleStation;
    public Transform enemyBattleStation;
    public Hud enemyHud;
    public Hud playerHud;

    Unit playerUnit;
    Unit enemyUnit;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        SetupBattle();
    }

    void SetupBattle()
    {
        GameObject playerGo = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGo.GetComponent<Unit>();
        playerHud.SetHud(playerUnit);

        GameObject enemyGo = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGo.GetComponent<Unit>();
        enemyHud.SetHud(enemyUnit);

        //Consol enemyUnit.unitName;
    }

}
