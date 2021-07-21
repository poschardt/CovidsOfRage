using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    public float jumpForce;


    private bool OnGround;
    private float currentSpeed;
    private Animator animator;
    private Rigidbody2D rb;
    private float xAxis;
    private float zAxis;

    private bool facingRight = true;
    private bool jump;

    // Start is called before the first frame update
    void Start()
    {
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

        if (Input.GetKeyDown(KeyCode.X))
        {
            animator.SetTrigger("Attack");
        }

        //Corrida - Tecla ainda não decidida (Talvez 2x direção seja uma boa)
    }

    private void FixedUpdate()
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
        rb.velocity = new Vector3(xAxis * currentSpeed, rb.velocity.y, zAxis * currentSpeed);

        //Caso estiver no  chao, ativar animação de velocidade (Não entendi o motivo de passar a velocidade para animação)
        if (OnGround)
        {
            animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        }
        animator.SetBool("onGround", OnGround);
        animator.SetFloat("ySpeed", rb.velocity.y);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnGround = collision.gameObject.layer == LayerMask.NameToLayer("Ground") ? true : false;
    }

    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;

        transform.localScale = scale;
    }

}
