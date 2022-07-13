using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    public PlayerManager playerManager;
    public GameObject boostBarGO;


    public GameObject player;
    private void Awake()
    {


        boostBarGO = FindObjectOfType<BoostBar>().gameObject;
        //DontDestroyOnLoad(boostBarGO);
        //DontDestroyOnLoad(gameObject);
        //DontDestroyOnLoad(FindObjectOfType<Boost>().gameObject);
    }

}
