using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectKiller : MonoBehaviour
{
    public GameObject playerPrefab;
    public Movement movementScript;
    public Vector3 startingPosition;
    public Transform spawnTransform;
    public GameOver gameOverScript;

    public void Start() {
        startingPosition = playerPrefab.transform.position;
        print(startingPosition);
        spawnTransform.position = startingPosition; 
    }
    //private void Update()
    //{
    //    if (movementScript.playerDestroyed == true) { SpawnPlayer(); }
    //}
    public void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.gameObject.tag == "Player")
        {
            //gameOverScript.GameOverMan();


            playerPrefab.transform.position = startingPosition;
            //Destroy(playerPrefab);
           
            movementScript.playerDestroyed = true;
            //movementScript.GameOver();
            //SpawnPlayer();

            Debug.Log("Player Restarted");
        }
       
    }
    public void SpawnPlayer()
    {
        Instantiate(playerPrefab,spawnTransform); //movementScript.playerDestroyed = false;
    }
        
        

}
