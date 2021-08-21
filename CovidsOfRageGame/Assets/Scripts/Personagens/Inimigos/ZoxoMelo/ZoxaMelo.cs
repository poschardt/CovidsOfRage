using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoxaMelo : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isDead = false;
        _gm = FindObjectOfType<StageManager>();

        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        isDead = currentHealth <= 0;
        facingRight = (_gm.Player.transform.position.x > this.transform.position.x) ? false : true;

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
            animator.SetTrigger("Dead");
        }
    }

}
