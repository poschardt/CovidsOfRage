using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AzitromiGoblin : Enemy
{

    private float currenctSpeed;
    private float walkTimer;
    private bool facingRight;
    private float damageTimer;
    private bool OnGround;
    private Vector3 position;
    private Transform posEsquerda;
    private Transform posDireita;
    private float nextAttack;
    private float hForce;
    private bool morto = false;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
        target = FindObjectOfType<Player2D>().transform;

        posEsquerda = this.transform.parent.transform.GetChild(1);
        posDireita = this.transform.parent.transform.GetChild(2);


        position = posEsquerda.position;
        facingRight = true;
        hForce = 0;
        nextAttack = 0.6f;
    }

    // Update is called once per frame
    void Update()
    {
        isDead = currentHealth <= 0;


        facingRight = (position.x > this.transform.position.x) ? false : true;

        if (facingRight)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

   
    }
    void FixedUpdate()
    {

        if (!isDead)
        {
            if (this.transform.position.x <= position.x)
            {
                position = posDireita.position;
                hForce = 1;
            }
            else
            {
                position = posEsquerda.position;
                hForce = -1;
            }

            //  float hForce = position.x / Mathf.Abs(position.x);
            rb.velocity = new Vector2(hForce * maxSpeed, 0);
            rb.position = new Vector2(rb.position.x, rb.position.y);
            anim.SetFloat("Speed", Math.Abs(rb.velocity.x));


            if (Mathf.Abs(target.position.x - this.transform.position.x) < 0.7f && Mathf.Abs(target.position.y - this.transform.position.y) < 0.7f && Time.time > nextAttack)
            {
                if ((facingRight && this.transform.position.x >= target.position.x) || (!facingRight && this.transform.position.x <= target.position.x))
                {
                    anim.SetTrigger("Attack");
                    rb.velocity = new Vector2(0, 0);
                    nextAttack = Time.time + attackRate;
                }

            }

        }
        else
        {
            rb.velocity = new Vector2(0, 0);
            anim.SetFloat("Speed", Math.Abs(rb.velocity.x));
            if(!morto)
            {
                anim.SetTrigger("Dead");
            }
            morto = true;
        }
        anim.SetBool("onGround", OnGround);

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnGround = collision.gameObject.layer == LayerMask.NameToLayer("Ground") ? true : false;
    }


}
