using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject italian;
    public GameObject playerPrefab;
    [SerializeField] private ObjectKiller objectKillerScript;
    [SerializeField] private float enemyMoveSpeed;

    [SerializeField] private bool hitWayPoint;

    [SerializeField] private Enemy enemyScript;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") { playerPrefab.transform.position = objectKillerScript.startingPosition; }

        if (collision.gameObject.tag == "Waypoint" && hitWayPoint == false)
        {
            hitWayPoint = true;
            print("Hit Way Point");
        }
        else if (collision.gameObject.tag == "Waypoint" && hitWayPoint == true)
        {
            hitWayPoint = false;
            print("Hit Way Point");
        }

    }
    private void Start()
    {
        //italian.transform.Translate(Vector3.left * Time.deltaTime);
        hitWayPoint = false;
    }

    private void Update()
    {
        if (!hitWayPoint) { italian.transform.Translate(Vector3.left * enemyMoveSpeed * Time.deltaTime); hitWayPoint = false; }
        else if (hitWayPoint) { italian.transform.Translate(Vector3.right * enemyMoveSpeed * Time.deltaTime); hitWayPoint = true; }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
     
    }
}
