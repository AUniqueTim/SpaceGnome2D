using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{

    public bool victoryActive;
    public AudioClip winSound;
    public GameObject winText;
    public GameObject tnt;
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            //VictoryMethod();
            
            victoryActive = true;
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (victoryActive)
        {
            
            victoryActive = false;
        }
    }
    private void Update()
    {
        if (victoryActive) { winText.SetActive(true); }
        else if (!victoryActive) { winText.SetActive(false); }
    }
    public void VictoryMethod()
    {
        //winAnimation.Play //Add Boat Exploding and Sinking
         //Add explosion SFX.
    }
}
