using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class src_MainCharacter : MonoBehaviour
{

    public Rigidbody2D Rigidbody2D;
    public float Speed;
    public float Salto;
    public bool IsGroundCheck; //La bool que confirma si estamos tocando el suelo o no
    public float GroundCheck_Radius; //Es el randio del circulo para saltar
    public float GroundCheck_Offset; //Esto sube o baja el circulo de salto
    public LayerMask GroundCheck_Layer; //Indicas que capa de suelo
    public int Lives = 3;

    // Start is called before the first frame update
    void Start()
    {
        //le asignamos el rigidbody de la propia capsula en manual
        Rigidbody2D = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        Lives = Mathf.Clamp(Lives, 0, 3);
        
        if (Lives >= 0) 
        {
            float ImputHorizontal = Input.GetAxisRaw("Horizontal"); //Esto son los imputs horizontales de unity
            Rigidbody2D.velocity = new Vector2(ImputHorizontal * Speed, Rigidbody2D.velocity.y); //aqui le damos velocidad con un vector2 el imput izquiero(lo de arriba) lo multiplicamos por la velocidad y lo otro le dejamos la gravedad base

            IsGroundCheck = Physics2D.OverlapCircle(transform.position + Vector3.up * GroundCheck_Offset, GroundCheck_Radius, GroundCheck_Layer); //Aui decimos que si el circulo esta overlapeando el suelo active la bool

            if (Input.GetButtonDown("Jump") && IsGroundCheck) //Jump ya esta definido en unity y la bool tiene que ser positiva para saltar
            {
                Jump();
            }

            
        }

    }

    public void Jump()
    {
        Rigidbody2D.velocity = new Vector3(Rigidbody2D.velocity.x, 0); //Quitas la velocidad en ese momento para que el salto funcione bien
        Rigidbody2D.AddForce(Vector2.up * Salto); //Le damos un impulso en.UP
    }

    public void OnDrawGizmos()  //es la funcion para dibujar el rirculo de salto
    {
        Gizmos.color = Color.magenta; //color
        Gizmos.DrawWireSphere(transform.position + Vector3.up * GroundCheck_Offset, GroundCheck_Radius);//Son la posicion del personaje con el salto incluido y el offset deseado y por ultimo el radio
    }

    private void OnCollisionEnter2D(Collision2D collision) //para detectar una colision
    {
        if (collision.transform.CompareTag("Enemy"))  //si toca el tag Enemy pasa lo de abajo
        {
            foreach(ContactPoint2D Points in collision.contacts) //esto es un bucle, y al tocar cualquier colision pasa lo de abajo
            {
                if (Points.normal.y >= 0.9) //Son unos puntos que tiene los colider.  Se reparten por todo el cuadrado y al poner que solo el rango de 0.1 arriba seria como al darle encima y pasa lo de abajo
                {
                    Destroy(collision.gameObject); //Destruye al enemgio
                }

                else 
                {
                    Lives--; //Te quita una vida
                }

            }
        }
    }


}


  
