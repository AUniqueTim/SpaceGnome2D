using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class Interactables : MonoBehaviour
{
    public GameObject smallChild;
    public GameObject dialogue1;
    public bool smallChildDialogueActive;


    private void OnCollisionEnter2D(Collision2D collision)
    {
       
            if (collision.gameObject.tag == "Player")
            {
                smallChildDialogueActive = true;
                //enable smallchild dialogue 1

                
            }
      
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (smallChildDialogueActive)
        {
            smallChildDialogueActive = false;
        }
    }
    private void Update()
    {
        if (smallChildDialogueActive) { dialogue1.SetActive(true); }
        else if (!smallChildDialogueActive) { dialogue1.SetActive(false); }
    }
}
