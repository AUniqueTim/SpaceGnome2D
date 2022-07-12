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
       

        Debug.Log("Waiting two second(s)...");
        yield return new WaitForSeconds(2f);
        

        state = BattleState.PLAYERTURN;
        Debug.Log("Player turn started.");
        StartCoroutine(PlayerTurn());
    }

    IEnumerator PlayerTurn()
    {

        //Current Workaround for button mashing bug is to disable buttons during enemy turn, but does not work well.
        // attackButtonGO.SetActive(true);
        // healButtonGO.SetActive(true);

        dialogueText.text = "Choose Action: ";

        yield return new WaitForSeconds(1f);

    }

    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(Random.Range(1, 4));
        playerHUD.SetHUD(playerUnit);


        dialogueText.text = "Player Healed!"; // Add Unit Heal(healAmount);


        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());

        yield return new WaitForSeconds(1f);
    }
    IEnumerator PlayerAttack()
    {
        //enemyUnit.TakeDamage(playerUnit.damage);
        //enemyHUD.SetHP(enemyUnit.currentHP - playerUnit.damage);
        //Damage enemy
        bool isDead = enemyUnit.TakeDamage(enemyUnit.currentHP - playerUnit.damage);


       
        //playerHUD.SetHUD(playerUnit);
        
        enemyHUD.SetHUD(enemyUnit);
      

        
        //Toolbox.Instance.m_playerManager.boost -= 1000f;


        //Check enemy health
        if (isDead)
        {
            state = BattleState.WON;
          //  yield return new WaitForSeconds(1f);
            StartCoroutine(EndBattle());
       
            
        }
        else if(!isDead)
        {
            //Enemy Turn
            state = BattleState.ENEMYTURN;

         // yield return new WaitForSeconds(1f);

            StartCoroutine(EnemyTurn());
        }
        dialogueText.text = "Attack Hit!";
        yield return new WaitForSeconds(1f);
    }
    IEnumerator BuyDrink()
    {
        //Toolbox.Instance.m_playerManager.boost += 1000f;

        //Heal Enemy
        enemyUnit.ReceiveDrink(2);
        
        //Give Enemy Boost
        enemyHUD.SetEnemyBoost(100);

        enemyHUD.SetHUD(enemyUnit);
       
       


        if (enemyUnit.currentHP >= enemyUnit.maxHP)
        {
            enemyUnit.currentHP = enemyUnit.maxHP;
            hpFull = true;


        }
        else {hpFull = false; }
        if (hpFull)
        {
            state = BattleState.WON;
            
            StartCoroutine(EndBattle());
            yield return new WaitForSeconds(0.5f);

        }
        else
        {
            //Enemy Turn
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
            yield return new WaitForSeconds(0.5f);
        }
        //yield return new WaitForSeconds(0.5f);
        dialogueText.text = "Bought a Drink!";
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator EnemyTurn()
    {
        Debug.Log("Enemy turn started.");
        //enemyUnit.currentBoost -= 50;
        //enemyHUD.SetHUD(enemyUnit);
        int critChance;
        bool critHit = false;
        // attackButtonGO.SetActive(false);
        // healButtonGO.SetActive(false);
        //Enemy AI goes here

      

        //Enemy Crits
        critChance = Random.Range(0, 10);
        if (critChance == 3 || critChance == 7)
        {
            critHit = true;
        }
        else { critHit = false; }
        if (critHit)
        {
            enemyUnit.damage += 2;
            playerHUD.SetHP(playerUnit.currentHP - enemyUnit.damage);
            Debug.Log("Enemy rolled a " + critChance + ", scoring a critical hit.");
            
         
            enemyUnit.damage -= 2;

           
        }
        if (!critHit)
        {
            dialogueText.text = "A " + enemyUnit.name + " attacks!";
            playerHUD.SetHP(playerUnit.currentHP -= enemyUnit.damage);
            // playerHUD.SetHUD(playerUnit);
            yield return new WaitForSeconds(0.5f);
        }
        


        bool isDeadPlayer = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHUD(playerUnit);


        if (isDeadPlayer)
        {
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
           // yield return new WaitForSeconds(0.5f);
        }
        else
        {
            state = BattleState.PLAYERTURN;
            StartCoroutine(PlayerTurn());
           // yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(1.5f);
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

        enemyUnit.currentBoost = 0;
        playerUnit.currentBoost = 100;
        //EditorSceneManager.LoadScene("WorldMap");
        Debug.Log("Finished battle.");
        yield return new WaitForSeconds(1.5f);

    }
    
    IEnumerator Charm()
    {

        int charmChance;
        int threshhold = 10;
        
        charmChance = Random.Range(1, 21);

        Debug.Log("Rolled a " + charmChance + ", threshhold is currently " + threshhold);

        if (charmChance == 20 || charmChance >= threshhold)
        {
            
            enemyHUD.SetEnemyBoost(300);
            dialogueText.text = "Player successfully charmed enemy!";
            yield return new WaitForSeconds(0.5f);

        }
        else if (charmChance != 20 || charmChance < threshhold)
            {
            dialogueText.text = "Player failed to charm enemy!";
            yield return new WaitForSeconds(0.5f);
        }
        
        enemyHUD.SetHUD(enemyUnit);
        

        

        if (enemyUnit.currentBoost >= enemyUnit.maxBoost)
        {
            state = BattleState.WON;
            StartCoroutine(EndBattle());
           // yield return new WaitForSeconds(0.5f);
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
           // yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(1.5f);
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
