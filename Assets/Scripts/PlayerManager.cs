using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


public class PlayerManager : MonoBehaviour
{
    public PlayerManager playerManager;

    //public Image boostBar;
    
    public float boost;

    private void Start()
    {

        boost = 10000f;
      
    }
}
