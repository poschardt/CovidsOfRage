using Assets;
using Assets.Scripts.Personagem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class Player2D : Personagem
{

    public GameObject attack2;
    //Propriedade que verifica se Alita está curando ou não
    private bool healing = false;
    private float xAxis;

    [Header("KnockBack stuff")]
    public float thrust;
    public float minimumYthrust;
    public float knockbackTime;

    [Header("Flash stuff")]
    public int numberOfFlashses;
    public float flashDuration;
    public SpriteRenderer sprite;
    public Color flashColor;


    [Header("Dash")]
    public bool dashing;
    public float dashDuration;
    private float doubleTapTime;
    public float doubleTap;
    public float dashForce;
    KeyCode lastKeyCode;
    KeyCode currentKeyCode;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        currentSpeed = walkSpeed;
        facingRight = true;
        dashing = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Direction
        xAxis = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (doubleTapTime > Time.time && lastKeyCode == KeyCode.LeftArrow)
            {
                if (!knocking)
                    if(!dashing)
                    StartCoroutine(Dash(-1f));
            }
            else
            {
                doubleTapTime = Time.time + doubleTap;
            }
            lastKeyCode = KeyCode.LeftArrow;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (doubleTapTime > Time.time && lastKeyCode == KeyCode.RightArrow)
            {
                if (!knocking)
                    if (!dashing)
                        StartCoroutine(Dash(1f));

            }
            else
            {
                doubleTapTime = Time.time + doubleTap;
            }
            lastKeyCode = KeyCode.RightArrow;
        }

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && OnGround && !dashing)
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
            if (!healing && !knocking)
            {
                if (!dashing)
                    rb.velocity = new Vector3(xAxis * currentSpeed, rb.velocity.y, 0);

            }
            else if (!knocking)//Essa verificação é necessaria pois pode interferir no knockback
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (LayerMask.LayerToName(other.gameObject.layer))
        {
            case "EnemyHit":
                StartCoroutine(this.Flash(numberOfFlashses, flashDuration, sprite, flashColor));
                knocking = true;

                StartCoroutine(this.KnockBack(knockbackTime));

                //Calculando KnockBack
                Vector2 difference = other.transform.position - this.transform.position;
                difference = difference.normalized * thrust;
                difference.x = -difference.x;
                if (difference.y - this.transform.position.y <= 0f)
                    difference.y = minimumYthrust;
                this.rb.velocity = Vector2.zero;
                this.rb.AddForce(difference, ForceMode2D.Impulse);
                break;
        }
    }


    public override void TookDamage(int damage)
    {
        if (!isDead)
        {
            currentHealth -= damage;

            FindObjectOfType<UIManager>().UpdateHealth(currentHealth);

        }
    }

    public virtual void Atk2(EnumBoolean valor)
    {
        if (valor == EnumBoolean.Verdadeiro)
            this.attack2.gameObject.SetActive(true);
        else
            this.attack2.gameObject.SetActive(false);

    }

    protected IEnumerator Dash(float direction)
    {
        dashing = true;
        animator.SetTrigger("Dashing");

        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(new Vector2(4 * direction, 0f), ForceMode2D.Impulse);
        int defaultLayer = this.gameObject.layer;
        this.gameObject.layer = LayerMask.NameToLayer("Invulneravel");
        float gravity = rb.gravityScale;
        rb.gravityScale = 0.1f;
        yield return new WaitForSeconds(dashDuration);
        this.gameObject.layer = defaultLayer;
        dashing = false;
        rb.gravityScale = gravity;
        //  dashing = false;
        // animator.SetBool("Dashing", dashing);

    }


}
