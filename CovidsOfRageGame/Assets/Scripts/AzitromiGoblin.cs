using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AzitromiGoblin : Enemy
{
    public float maxSpeed;
    private float currenctSpeed;
    private float walkTimer;
    public float attackRate = 1f;
    public float damageTime = 0.5f;
    private bool facingRight = false;
    private float damageTimer;
    private bool OnGround;
    private bool atacando;


    // Start is called before the first frame update
    void Start()
    {
        _gm = FindObjectOfType<GameManager>() as GameManager;

        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        isDead = currentHealth <= 0;
        anim.SetBool("Dead", isDead);
        if (damaged && !isDead)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageTime)
            {
                damaged = false;
                damageTimer = 0;
            }
        }

        if (Math.Abs(this.transform.position.x - _gm.Player.transform.position.x) < 1f && !atacando)
            Atacar();

    }

    private void Atacar()
    {
        anim.SetTrigger("Attack");
        atacando = true;
        StartCoroutine("DelayAtk");
    }

    IEnumerator DelayAtk()
    {
        yield return new WaitForSeconds(attackRate);

        atacando = false;
    }

    void FixedUpdate()
    {

        if (!isDead)
        {
            
        }
        anim.SetBool("onGround", OnGround);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            OnGround = collision.gameObject.layer == LayerMask.NameToLayer("Ground") ? true : false;
        else
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
    }
}
