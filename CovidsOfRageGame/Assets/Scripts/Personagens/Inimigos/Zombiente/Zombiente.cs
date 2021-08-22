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
    private bool transformando;
    public float timeToHeal;
    private float currentTimeToHeal;
    private bool transformado;
    private float hForce = 0;
    // Start is called before the first frame update
    void Start()
    {
        _gm = FindObjectOfType<StageManager>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        attack = gameObject.transform.Find("Attack").gameObject;
        vitima = this.transform.parent.transform.GetChild(1);
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
        animator.SetFloat("Transforming", this.currentTimeToHeal);
        animator.SetBool("Transformado", this.transformado);

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

            rb.velocity = new Vector2(hForce * runSpeed, 0);
            rb.position = new Vector2(rb.position.x, rb.position.y);

            animator.SetFloat("Speed", Math.Abs(rb.velocity.x));


            if (Mathf.Abs(_gm.Player.transform.position.x - this.transform.position.x) < 0.7f && Mathf.Abs(_gm.Player.transform.position.y - this.transform.position.y) < 0.7f && Time.time > nextAttack)
            {
                if ((facingRight && this.transform.position.x >= _gm.Player.transform.position.x) || (!facingRight && this.transform.position.x <= _gm.Player.transform.position.x))
                {

                    animator.SetTrigger("Attack");
                    rb.velocity = new Vector2(0, 0);

                    nextAttack = Time.time + attackRate;
                }

            }
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
            animator.SetFloat("Speed", Math.Abs(rb.velocity.x));

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
                animator.SetTrigger("Healed");
                _gm.currentStage.IncrementaVitimaSalva();
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
