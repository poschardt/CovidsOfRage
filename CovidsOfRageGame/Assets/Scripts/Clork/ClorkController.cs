using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClorkController : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;

     
    public float velocidade;

    [Header("Parametros de Atk")]
    public float delayAtk;
    public float distanciaAtk;
    
    public GameObject player;

    private bool atacando;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame//
    void Update()
    {
        rb.velocity = new Vector2(velocidade, 0f);

        if(rb.velocity.x != 0)
        {
            anim.SetBool("Andando", true);
        }
        else
        {
            anim.SetBool("Andando", false);
        }


        if(this.transform.position.x > (player.transform.position.x)*1.15)
        {
            Vector3 theScale = transform.localScale;
            theScale.x = -1;
            transform.localScale = theScale;
            velocidade = -0.2F;
        }


        float distBossPlayer = Math.Abs(player.transform.position.x - this.transform.position.x);
        
        //Debug.Log(distBossPlayer);

        if(distBossPlayer < distanciaAtk && !atacando)
        {
            anim.SetTrigger("Atk");
            atacando = true;
            StartCoroutine("DelayAtk");
        }
    }

    IEnumerator DelayAtk()
    {
        yield return new WaitForSeconds(delayAtk);

        atacando = false;
    }
}
