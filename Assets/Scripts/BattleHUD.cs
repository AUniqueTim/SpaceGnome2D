using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text nameText;
    public Text levelText;
    public Slider hpSlider;
    public Text hpText;
    public Text enemyHPText;
    public Text boostText;
    public Text enemyBoostText;

    public Unit enemyUnit;
    public Unit playerUnit;

    private void Start()
    {
        SetHUD(enemyUnit);
        SetHUD(playerUnit);
    }

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        levelText.text = "LVL " + unit.unitLevel;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
        hpText.text = unit.currentHP.ToString();
        enemyHPText.text = unit.currentHP.ToString();
        boostText.text = unit.currentBoost.ToString();
        enemyBoostText.text = unit.currentBoost.ToString();
    }

    public void SetHP(int hp)
        {
            hpSlider.value = hp;
      //  hpText.text = hp.ToString();
        //enemyHPText.text = hp.ToString();
        
        }

    public void SetBoost(int boost)
    {
        //enemyUnit.currentBoost += boost;
       playerUnit.currentBoost += boost;
     //  boostText.text += boost.ToString();
       
        if (playerUnit.currentBoost >= playerUnit.maxBoost)
        {
            playerUnit.currentBoost = playerUnit.maxBoost;
        }

       
    }
    public void SetEnemyBoost(int boost)
    {
        enemyUnit.currentBoost += boost;
       // enemyBoostText.text += boost.ToString();

        if (enemyUnit.currentBoost >= enemyUnit.maxBoost)
        {
            enemyUnit.currentBoost = enemyUnit.maxBoost;
        }
    }
    

    
}
