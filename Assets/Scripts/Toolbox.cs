using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbox : MonoBehaviour
{
    
    public Unit m_playerUnit;
    public Unit m_enemyUnit;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

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
        m_playerUnit = playerPrefab.GetComponent<Unit>();

        m_enemyUnit = enemyPrefab.GetComponent<Unit>();
    }
    private void Update()
    {
       
    }
}
