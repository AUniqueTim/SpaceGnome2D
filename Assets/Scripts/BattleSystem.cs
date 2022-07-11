using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.SceneManagement;

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

    public GameObject attackButtonGO;
    public GameObject healButtonGO;

    public bool hpFull;

    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetUpBattle());
    }
    IEnumerator SetUpBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation, true) ;
        playerUnit =  playerGO.GetComponent<Unit>();
        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation, true);
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
        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        dialogueText.text = "Attack Hit!";
        Toolbox.Instance.m_playerManager.boost -= 1000f;
        yield return new WaitForSeconds(1f);

        //Check enemy health
        if (isDead)
        {
            state = BattleState.WON;
            StartCoroutine(EndBattle());
        }
        else
        {
            //Enemy Turn
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
       
    }
    IEnumerator BuyDrink()
    {
        Toolbox.Instance.m_playerManager.boost += 1000f;

        //Heal Enemy
        hpFull = enemyUnit.ReceiveDrink(enemyUnit.maxHP);

        enemyHUD.SetHP(enemyUnit.currentHP);

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        dialogueText.text = "Bought a Drink!";

        yield return new WaitForSeconds(0.5f);

        if (enemyUnit.currentHP >= enemyUnit.maxHP)
        {
            enemyUnit.currentHP = enemyUnit.maxHP;
            hpFull = true;


        }
        else hpFull = false;
        if (hpFull)
        {
            state = BattleState.WON;
            StartCoroutine(EndBattle());
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
        int critChance;
        bool critHit = false;
        attackButtonGO.SetActive(false);
        healButtonGO.SetActive(false);
        //Enemy AI goes here
        critChance = Random.Range(0, 10);
        if (critChance == 3 || critChance == 7)
        {
            critHit = true;
        }
        if (critHit)
        {
            enemyUnit.damage += 2;
            Debug.Log("Enemy rolled a " + critChance + ", scoring a critical hit.");
        }
        else { yield return new WaitForSeconds(0.5f); }
        dialogueText.text = enemyUnit.unitName + " attacks!";

        yield return new WaitForSeconds(0.5f);

        bool isDeadPlayer = playerUnit.TakeDamage(enemyUnit.damage);
        enemyUnit.damage -= 2;
        playerHUD.SetHP(playerUnit.currentHP);
        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);
        yield return new WaitForSeconds(0.5f);

        if (isDeadPlayer)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else if (hpFull) 
        {
            state = BattleState.WON;
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }
    IEnumerator EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "Battle WON!";
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "Battle LOST!";
        }
        yield return new WaitForSeconds(1.5f);

        EditorSceneManager.LoadScene("WorldMap");
        
       
    }
    void PlayerTurn() 
    {

        //Current Workaround for button mashing bug is to disable buttons during enemy turn, but does not work well.
        attackButtonGO.SetActive(true);
        healButtonGO.SetActive(true);

        dialogueText.text = "Choose Action: ";
    }

    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(3);

        playerHUD.SetHP(playerUnit.currentHP);

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);
        dialogueText.text = "Player Healed!"; // Add Unit Heal(healAmount);

        yield return new WaitForSeconds(1f);
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }
    IEnumerator Charm()
    {
        playerUnit.TakeDamage(1);
        enemyUnit.currentHP += 3;
        playerHUD.SetHP(playerUnit.currentHP);
        dialogueText.text = "Player charmed enemy";

        yield return new WaitForSeconds(0.5f);
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }
    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }
    public void OnBuyDrinkButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(BuyDrink());
    }
    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerHeal());
    }
    public void OnCharmButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(Charm());
    }

}
