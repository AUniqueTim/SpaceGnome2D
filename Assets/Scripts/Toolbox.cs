using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbox : MonoBehaviour
{
    public PlayerManager m_playerManager;
    public BoostBar m_boostBar;
    public BattleSystem m_playerUnit;
    public BattleSystem m_enemyUnit;
    public Boost m_boostClass;

    //START SINGLETON

    public static Toolbox instance;
    public static Toolbox Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Toolbox>();
                if (instance == null)
                {
                    GameObject singleton = new GameObject();
                    singleton.AddComponent<Toolbox>();
                    singleton.name = "(Singleton) Toolbox";
                }
            }
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
       
    }
    private void Update()
    {
       
    }
}
