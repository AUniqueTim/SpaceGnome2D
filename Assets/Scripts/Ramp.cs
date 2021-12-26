using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramp : MonoBehaviour
{
    public GameObject ramp;
    public GameObject poke;
    public bool isExitingCollision;
    public bool isEnteringCollision;


    public float rampJumpHeight;
    [SerializeField] int defaultJumpSpeed;

    public float playerYHeight;

    public Movement movementScript;
    public Ramp rampScript;
    
    public void Start()
    {
        playerYHeight = poke.transform.position.y;

        movementScript = poke.GetComponent<Movement>();    
    }
    public void OnTriggerEnter2D(Collider2D rampCollider)
    {
        if (poke != null)
        {

            if (movementScript.runState == "isSliding")
            {
                Movement.jumpSpeed = rampJumpHeight;
                
                Debug.Log("Player hit ramp.");

                rampScript.isEnteringCollision = true;

            }
        }
        
        
    }
    public void OnTriggerExit2D(Collider2D rampCollider)
    {
        if (poke != null)
        {
            if (poke.transform.position.y >= (playerYHeight * 2))
            {
                
                rampScript.isExitingCollision = true;
            }
        }
    }

}
