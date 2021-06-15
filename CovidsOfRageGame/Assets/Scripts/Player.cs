using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    public float jumpForce;


    private bool OnGround;
    private string currentAnimation;
    private float currentSpeed;
    private Animator animator;
    private Rigidbody rb;
    private float xAxis;
    private float zAxis;
    private bool jump;
    private bool run;
    private bool facingRight = true;


    //Animations states
    const string PLAYER_IDLE = "Idle";
    const string PLAYER_WALK = "Walk";
    const string PLAYER_RUN = "Run";
    const string PLAYER_JUMP = "Jump";
    const string PLAYER_FALL = "Fall";

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = walkSpeed;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        run = false;
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

        //Corrida - Tecla ainda não decidida (Talvez 2x direção seja uma boa)
        run = false;
    }

    private void FixedUpdate()
    {
        Vector2 vel = new Vector2(0, rb.velocity.y);

        //Determinando velocidade (Corrida não implementada)
        currentSpeed = walkSpeed;
        print(zAxis);

        if (xAxis > 0 && !facingRight)//Direita
        {
            Flip();

        }
        else if (xAxis < 0 && facingRight)//Esquerda
        {
            Flip();

        }

        //Impedir o movimento do eixo z caso o personagem esteja no ar
        if (!OnGround)
            zAxis = 0;

        //Movimentação do personagem 
        rb.velocity = new Vector3(xAxis * currentSpeed, rb.velocity.y, zAxis * currentSpeed);

        if (jump)
        {
            jump = false;
            OnGround = false;

            rb.AddForce(Vector3.up * jumpForce);
        }


        if (OnGround)
        {
            if (rb.velocity.x != 0f)
                ChangeAnimationState(PLAYER_WALK);
            else if (rb.velocity.x == 0)
                ChangeAnimationState(PLAYER_IDLE);

        }
        else
        {
            print("entrei aqui");
            if (rb.velocity.y > 0f)
                ChangeAnimationState(PLAYER_JUMP);
            else if (rb.velocity.y < 0f)
                ChangeAnimationState(PLAYER_FALL);

        }



    }

    private void OnCollisionEnter(Collision collision)
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

    private void ChangeAnimationState(string state)
    {
        animator.Play(state);
        currentAnimation = state;
    }
}
