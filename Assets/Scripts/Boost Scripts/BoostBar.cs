using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BoostBar : MonoBehaviour
{
    public Image boostBar;
    public PlayerManager playerManager;
    public Boost boost;
    public  GameObject boostBarGO;
    
    
    private void Awake()
    {
        boostBar.GetComponent<Image>();

        //boostBar.fillAmount = .5f;

        boost = new Boost();
        boostBarGO = FindObjectOfType<BoostBar>().gameObject;
        
      //  DontDestroyOnLoad(boostBarGO);
    }
    public void Start()
    {
        
    }

    
    public void Update()
    {

        
        boost.Update();

        boostBar.fillAmount = boost.GetBoostNormalized();
    }

   

    public class Boost
    {
        public const float boostTotal = 10000f;

        [SerializeField] float boostAmount;
        [SerializeField] float boostRegen;

        public Boost()
        {
            boostAmount = 0f;
            boostRegen = 2000f;
        }

        public void Update()
        {
            boostAmount += boostRegen * Time.deltaTime;
            boostAmount = Toolbox.Instance.m_playerManager.boost;
        }

        public void SpendBoost(float amount)
        {
            if (boostAmount >= amount)
            {
                boostAmount -= amount;
            }
        }
        public float GetBoostNormalized()
        {
            return Toolbox.Instance.m_playerManager.boost / boostTotal;
        }
    }
}
