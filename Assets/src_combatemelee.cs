using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class src_combatemelee : MonoBehaviour
{
    [SerializeField] private Transform controladorGolpe;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float dañoGolpe;
    Animator animator;
    public AudioSource hit;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1")) 
        {
            hit.Play();
            Golpe();
            
        }
    }

    void Golpe() 
    {
        animator.SetInteger("ataques", 1);
        
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);

        foreach (Collider2D colisionador in objetos)
        {

            if (colisionador.CompareTag("Enemy")) 
            {
                colisionador.transform.GetComponent<Src_enemy_00>().TomarDaño(dañoGolpe);
            }

        }

        StartCoroutine(coldownanimation());
    }

    private IEnumerator coldownanimation()
    {
        yield return new WaitForSeconds(0.4f);
        animator.SetInteger("ataques", 0);
    }

    private void OnDrawGizmos()
    {
          Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
    }

}
