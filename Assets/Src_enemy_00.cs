using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Src_enemy_00 : MonoBehaviour
{
    Rigidbody2D rigidbody;
    public float velocity;
    float direction = 1;
    public bool CanFlip;
    public float TimeToFlip;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        EnemyFlip();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.velocity = new Vector2(direction * velocity, rigidbody.velocity.y);


    }

    void EnemyFlip() 
    {
        direction *= -1;

        if (CanFlip)
        {
            Invoke("EnemyFlip", TimeToFlip);
        }
    }
}