using Assets.Scripts;
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
    // Start is called before the first frame update
    void Start()
    {
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
