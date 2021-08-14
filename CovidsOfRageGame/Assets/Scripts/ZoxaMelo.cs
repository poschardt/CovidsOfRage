using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoxaMelo : Enemy
{
    private float currenctSpeed;
    private float walkTimer;
    private bool facingRight;
    private bool OnGround;
    private bool morto = false;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = FindObjectOfType<Player2D>().transform;

        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        isDead = currentHealth <= 0;


        facingRight = (target.position.x > this.transform.position.x) ? false : true;

        if (facingRight)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    private void FixedUpdate()
    {
        if(!isDead)
        {

        }
        else
        {
            anim.SetTrigger("Dead");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnGround = collision.gameObject.layer == LayerMask.NameToLayer("Ground") ? true : false;
    }
}
