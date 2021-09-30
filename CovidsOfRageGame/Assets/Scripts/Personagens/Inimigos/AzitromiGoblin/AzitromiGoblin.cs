using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AzitromiGoblin : Enemy
{

    private Vector3 position;
    public Transform posEsquerda;
    public Transform posDireita;
    private float hForce;
    private bool morto = false;
    // Start is called before the first frame update
    void Start()
    {
        _gm = FindObjectOfType<StageManager>();
        rb = this.GetComponent<Rigidbody2D>();
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
        animator = GetComponent<Animator>();
        position = posEsquerda.position;
        currentHealth = maxHealth;
        isDead = false;
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

            if (canWalk)
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

                rb.velocity = new Vector2(hForce * runSpeed, 0);
                rb.position = new Vector2(rb.position.x, rb.position.y);
            }


            if (Mathf.Abs(_gm.Player.transform.position.x - this.transform.position.x) < 0.65f && Mathf.Abs(_gm.Player.transform.position.y - this.transform.position.y) < 0.65f && Time.time > nextAttack)
            {
                if ((facingRight && this.transform.position.x >= _gm.Player.transform.position.x) || (!facingRight && this.transform.position.x <= _gm.Player.transform.position.x))
                {
                    StartCoroutine("stopWalk");

                    animator.SetTrigger("Attack");
                    rb.velocity = new Vector2(0, 0);
                    nextAttack = Time.time + attackRate;
                }

            }
            animator.SetFloat("Speed", Math.Abs(rb.velocity.x));

        }
        else
        {
            rb.velocity = new Vector2(0, 0);
            animator.SetFloat("Speed", Math.Abs(rb.velocity.x));
            if (!morto)
            {
                animator.SetTrigger("Dead");
            }
            morto = true;
        }
        animator.SetBool("onGround", OnGround);

    }


    IEnumerator stopWalk()
    {
        canWalk = false;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(stopWalkTime);
        canWalk = true;
    }



}
