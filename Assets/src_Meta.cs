using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class src_Meta : MonoBehaviour
{
    public int SceneToGO;
    private void OnTriggerEnter2D(Collider2D collision)
    
    {
         if (collision.transform.CompareTag("Player"))
         {
            SceneManager.LoadScene(SceneToGO);
         }
    }
}   
