using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    public GameObject player;

    public float velocidade;

    [Header("Parâmetro de Ataque")]
    public float distanciaAtk;
    public float delayAtk;

    private bool atacando;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
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


        float distBossPlayer = Math.Abs(player.transform.position.x - this.transform.position.x);

        //Debug.Log(distBossPlayer);

        if (distBossPlayer < distanciaAtk && !atacando)
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
