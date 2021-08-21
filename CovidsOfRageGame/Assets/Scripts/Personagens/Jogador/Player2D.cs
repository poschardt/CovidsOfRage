using Assets;
using Assets.Scripts.Personagem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player2D : Personagem
{


    //Propriedade que verifica se Alita está curando ou não
    private bool healing = false;
    private float xAxis;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        attack = gameObject.transform.GetComponentInChildren<Attack>();
        currentHealth = maxHealth;
        currentSpeed = walkSpeed;
        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Direction
        xAxis = Input.GetAxis("Horizontal");
        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && OnGround)
        {
            jump = true;
        }

        if (Input.GetKey(KeyCode.C) && OnGround)
        {
            healing = true;
            animator.SetBool("Healing", healing);
        }
        else
        {
            healing = false;
            animator.SetBool("Healing", healing);
        }



        if (Input.GetKeyDown(KeyCode.X) && !healing)
        {
            animator.SetTrigger("Attack");
        }

        isDead = currentHealth <= 0 ? true : false;

        //Isso tem que ser decidido no UIManager
        if (isDead)
            SceneManager.LoadScene("GameOver");

    }

    void FixedUpdate()
    {
        if (!isDead)
        {

            if (xAxis > 0 && !facingRight)//Direita
            {
                Flip();
            }
            else if (xAxis < 0 && facingRight)//Esquerda
            {
                Flip();
            }

            //Pulo
            if (jump)
            {
                jump = false;
                rb.AddForce(Vector3.up * jumpForce);
            }


            //Movimentação do personagem 
            if (!healing)
                rb.velocity = new Vector3(xAxis * currentSpeed, rb.velocity.y, 0);
            else
                rb.velocity = new Vector3(0, rb.velocity.y, 0);

            //Caso estiver no  chao, ativar animação de velocidade 
            if (OnGround)
            {
                animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
            }

            animator.SetBool("onGround", OnGround);
            animator.SetFloat("ySpeed", rb.velocity.y);
        }

    }

    //public void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
    //        OnGround = collision.gameObject.layer == LayerMask.NameToLayer("Ground") ? true : false;
    //    else
    //        rb.velocity = new Vector3(0, rb.velocity.y, 0);
    //}
  
    //public void Flip()
    //{
    //    facingRight = !facingRight;

    //    Vector3 scale = transform.localScale;
    //    scale.x *= -1;

    //    transform.localScale = scale;
    //}

    //public void TookDamage(int damage)
    //{
    //    if (!isDead)
    //    {
    //        currentHealth -= damage;
    //        //    anim.SetTrigger("HitDamage");
    //        FindObjectOfType<UIManager>().UpdateHealth(currentHealth);
    //    }
    //}

}
