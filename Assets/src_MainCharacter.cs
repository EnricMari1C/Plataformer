using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class src_MainCharacter : MonoBehaviour
{
    public Rigidbody2D Rigidbody2D;
    public float Speed;
    public float Salto;
    public bool IsGroundCheck; // La bool que confirma si estamos tocando el suelo o no
    public float GroundCheck_Radius; // Es el radio del círculo para saltar
    public float GroundCheck_Offset; // Esto sube o baja el círculo de salto
    public float GroundCheck_XOffset; // Esto desplaza el círculo en X
    public LayerMask GroundCheck_Layer; // Indicas qué capa de suelo
    public int Lives = 3;
    Animator animations;
    float ImputHorizontal;
    public CapsuleCollider2D Capsula;
    float JumpAcceleration = 20f;
    public float FallMultiplier = 2.5f;
    public float LowJumpMultiplier = 2f;
    private float jumpTimeCounter;
    public float JumpTime;
    public float invulnerabilityDuration = 1f; // Duración de la invulnerabilidad en segundos
    private bool isInvulnerable = false; // Estado de invulnerabilidad
    private SpriteRenderer spriteRenderer; // Para parpadeos visuales
    public LayerMask originalLayer; // Capa original del personaje
    public LayerMask invulnerableLayer; // Capa para ignorar enemigos
    public AudioSource jump;
    public AudioSource dolor;


    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        animations = GetComponent<Animator>();
        Capsula = GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalLayer = gameObject.layer;
        invulnerableLayer = LayerMask.NameToLayer("IgnoreEnemy");
    }

    // Update is called once per frame
    void Update()
    {
        Lives = Mathf.Clamp(Lives, 0, 3);

        if (Lives >= 0)
        {
            ImputHorizontal = Input.GetAxisRaw("Horizontal");
            Rigidbody2D.velocity = new Vector2(ImputHorizontal * Speed, Rigidbody2D.velocity.y);

            // Calcula el offset total en X e Y
            Vector3 groundCheckOffset = Vector3.up * GroundCheck_Offset + Vector3.right * GroundCheck_XOffset * Mathf.Sign(transform.localScale.x);

            // Actualiza la verificación de suelo con el nuevo offset
            IsGroundCheck = Physics2D.OverlapCircle(Capsula.transform.position + groundCheckOffset, GroundCheck_Radius, GroundCheck_Layer);

            if (Input.GetButtonDown("Jump") && IsGroundCheck)
            {
                animations.SetTrigger("jump");
                //IsJumping = true;
                jump.Play();
                jumpTimeCounter = JumpTime;
                Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, Salto);
            }

            if (Input.GetButton("Jump"))
            {
                if (jumpTimeCounter > 0)
                {
                    Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, Rigidbody2D.velocity.y + JumpAcceleration * Time.deltaTime);
                    jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    //IsJumping = false;
                }
            }

            FlipCharacter();
            if (ImputHorizontal != 0)
            {
                animations.SetInteger("velocity", 1);
            }
            else
            {
                animations.SetInteger("velocity", 0);
            }

            if (IsGroundCheck) animations.SetBool("GroundCheark", true);
            else if (!IsGroundCheck) animations.SetBool("GroundCheark", false);

        }
    }

    public void Jump()
    {
        Rigidbody2D.velocity = new Vector3(Rigidbody2D.velocity.x, 0);
        Rigidbody2D.AddForce(Vector2.up * Salto);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Vector3 groundCheckOffset = Vector3.up * GroundCheck_Offset + Vector3.right * GroundCheck_XOffset * Mathf.Sign(transform.localScale.x);
        Gizmos.DrawWireSphere(Capsula.transform.position + groundCheckOffset, GroundCheck_Radius);
    }
    public void TakeDamage(int damage)
    {
        if (isInvulnerable) return; // No recibe daño si está invulnerable

        Lives -= damage; // Aplica el daño
        StartCoroutine(ActivateInvulnerability());
    }

    private IEnumerator ActivateInvulnerability()
    {
        isInvulnerable = true;
        gameObject.layer = invulnerableLayer; // Cambia a capa invulnerable

        // Efecto de parpadeo
        for (float i = 0; i < invulnerabilityDuration; i += 0.2f)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled; // Alterna la visibilidad
            yield return new WaitForSeconds(0.2f);
        }

        spriteRenderer.enabled = true; // Asegura que quede visible
        gameObject.layer = originalLayer; // Restaura la capa original
        isInvulnerable = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            foreach (ContactPoint2D Points in collision.contacts)
            {

                dolor.Play();
                TakeDamage(1);

            }
        }
    }

    private void FlipCharacter()
    {
        if (ImputHorizontal < 0.0f)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (ImputHorizontal > 0.0f)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        }
    }
}
