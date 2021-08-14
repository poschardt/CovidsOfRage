using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player2D : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    public float jumpForce;


    private bool OnGround;
    public float currentSpeed;
    private Animator animator;
    private Rigidbody2D rb;
    private float xAxis;
    private float zAxis;

    private bool facingRight = true;
    private bool jump;

    private bool healing = false;

    public int maxHealth = 10;
    private int currentHealth;
    private bool isDead = false;

    public string playerName;
    public Sprite playerImage;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentSpeed = walkSpeed;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Direction
        xAxis = Input.GetAxis("Horizontal");
        zAxis = Input.GetAxis("Vertical");
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

        if (isDead)
            SceneManager.LoadScene("GameOver");

        //Corrida - Tecla ainda não decidida (Talvez 2x direção seja uma boa)
    }

    private void FixedUpdate()
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
                OnGround = false;
                rb.AddForce(Vector3.up * jumpForce);
            }

            //Impedir o movimento do eixo z caso o personagem esteja no ar
            if (!OnGround)
                zAxis = 0;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            OnGround = collision.gameObject.layer == LayerMask.NameToLayer("Ground") ? true : false;
        else
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
    }

  
    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;

        transform.localScale = scale;
    }

    public void TookDamage(int damage)
    {
        if (!isDead)
        {
            currentHealth -= damage;
            //    anim.SetTrigger("HitDamage");
            FindObjectOfType<UIManager>().UpdateHealth(currentHealth);
        }
    }

}
