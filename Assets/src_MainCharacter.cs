using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class src_MainCharacter : MonoBehaviour
{

    public Rigidbody2D Rigidbody2D;
    public float Speed;
    public float Salto;
    public bool IsGroundCheck;
    public float GroundCheck_Radius;
    public float GroundCheck_Offset;
    public LayerMask GroundCheck_Layer;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float ImputHorizontal = Input.GetAxisRaw("Horizontal");
        Rigidbody2D.velocity = new Vector2(ImputHorizontal * Speed, Rigidbody2D.velocity.y);

        IsGroundCheck = Physics2D.OverlapCircle(transform.position + Vector3.up * GroundCheck_Offset, GroundCheck_Radius, GroundCheck_Layer);

        if (Input.GetButtonDown("Jump") && IsGroundCheck)
        {
            Rigidbody2D.velocity = new Vector3(Rigidbody2D.velocity.x , 0);
            Rigidbody2D.AddForce(Vector2.up * Salto);



        }

    }
  public void OnDrawGizmos() 
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position + Vector3.up * GroundCheck_Offset, GroundCheck_Radius);
    }

}


  
