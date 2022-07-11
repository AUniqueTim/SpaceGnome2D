using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class Interact : MonoBehaviour
{
   
    public bool isInteracting;


    public void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Colliding.");
            if (isInteracting)
            {
                EditorSceneManager.LoadScene("Bar2");
            }
        }
        else { return; }
    }
  
    private void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            isInteracting = true;
        }
        else { isInteracting = false; }
    }
}
