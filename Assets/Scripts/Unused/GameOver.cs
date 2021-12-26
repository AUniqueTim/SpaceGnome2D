using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{

    public ObjectKiller objectKillerScript;
    public GameObject player;
    public Movement movementScript;

   
    public void GameOverMan()
    {
        Instantiate(objectKillerScript.playerPrefab, objectKillerScript.spawnTransform, true);
        print("Player Spawned");    

        movementScript.playerDestroyed = false;
        //public static Object Instantiate(Object original, Vector3 position, Quaternion rotation, Transform parent

        //movementScript.enabled = true;

    }
}
