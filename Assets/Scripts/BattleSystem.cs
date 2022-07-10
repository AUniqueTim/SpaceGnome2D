using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST}

public class BattleSystem : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Unit playerUnit;
    Unit enemyUnit;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public Text enemyNameText;
    public Text playerNameText;
    public BattleState state;

    public Text dialogueText; 

    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetUpBattle());
    }
    IEnumerator SetUpBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit =  playerGO.GetComponent<Unit>();
        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        playerNameText.text = playerUnit.name;
        enemyNameText.text = enemyUnit.name;

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        Debug.Log("Waiting one second(s)...");
        yield return new WaitForSeconds(1f);
        

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }
    IEnumerator PlayerAttack()
    {
        //Damage enemy
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "Attack Hit!";

        yield return new WaitForSeconds(1f);

        //Check enemy health
        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            //Enemy Turn
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
       
    }

    IEnumerator EnemyTurn()
    {
        //Enemy AI goes here

        dialogueText.text = enemyUnit.unitName + " attacks!";

        yield return new WaitForSeconds(0.5f);

        bool isDeadPlayer = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(0.5f);

        if (isDeadPlayer)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }
    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "Battle WON!";
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "Battle LOST!";
        }
    }
    void PlayerTurn() 
    {
        dialogueText.text = "Choose Action: ";
    }

    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(3);

        playerHUD.SetHP(playerUnit.currentHP);
        dialogueText.text = "Player Healed!"; // Add Unit Heal(healAmount);

        yield return new WaitForSeconds(1f);
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }
    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }
    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerHeal());
    }

}
