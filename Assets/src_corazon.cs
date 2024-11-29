using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class src_corazon : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player")) 
        {
            var Character = collision.GetComponent<src_MainCharacter>();

            if (Character.Lives != 3)
            {
                Character.Lives += 1;
                Destroy(gameObject);
            }
        }
    }
}
