using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Zombiente : Enemy
{

    private Transform vitima;
    public Transform posEsquerda;
    public Transform posDireita;
    private Vector3 position;
    private bool facingRight;
    private bool transformando;
    public float nextAttack;
    public float timeToHeal;
    private float currentTimeToHeal;
    private bool transformado;
    private float hForce = 0;
    // Start is called before the first frame update
    void Start()
    {
        vitima = this.transform.parent.transform.GetChild(1);
        posEsquerda = this.transform.parent.transform.GetChild(2);
        posDireita = this.transform.parent.transform.GetChild(3);

        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        target = FindObjectOfType<Player2D>().transform;
        currentHealth = maxHealth;
        position = posEsquerda.position;
        facingRight = false;
        transformando = false;
        isDead = false;
        transformado = false;
        currentTimeToHeal = 0;
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
        anim.SetFloat("Transforming", this.currentTimeToHeal);
        anim.SetBool("Transformado", this.transformado);

        if (!isDead && !transformando && !transformado)
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

           // hForce = position.x / Mathf.Abs(position.x);

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

        }

    }
    IEnumerator Aguardando()
    {
        yield return new WaitForSeconds(1f);
        // esperar = false;
    }
    public void Transforma()
    {

        vitima.position = this.transform.position;
        this.gameObject.SetActive(false);

        vitima.gameObject.SetActive(true);

    }

    public void startTransform()
    {
        transformando = true;

        rb.velocity = new Vector2(0, 0);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("LanternHit"))
        {
            this.transformando = true;

        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("LanternHit"))
        {
            currentTimeToHeal += Time.deltaTime;

            if(currentTimeToHeal >= timeToHeal && !transformado)
            {
                transformado = true;
                anim.SetTrigger("Healed");
            }

        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("LanternHit"))
        {
            this.transformando = false;
            currentTimeToHeal = 0;

        }
    }

}
