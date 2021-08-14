using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeController : Enemy
{
    public GameObject enemy;
    public GameObject ShotRange;
    public GameObject DiveRange;
    public GameObject HitRange;

    private SpriteRenderer spRenderer;
    //private Rigidbody2D rb;
 //   private Animator anim;

    private CapsuleCollider2D hitCollider;
    private CircleCollider2D shotCollider;
    private CircleCollider2D diveCollider;

    private GameManager _gm;

    private bool insideShotRadius;
    private bool insideDashRadius;

    private bool shoot;
    private bool isShooting;
 //   private bool isDead;

    private bool meleeAtk;

    public bool allowMovement;

    [Header("Controle de Tiro")]
    public GameObject tiroPrefab;
    public float delayTiro;

    [Header("Velocidade Dash")]
    public float velocidadeDash;

    void Start()
    {
        currentHealth = maxHealth;
        _gm = FindObjectOfType<GameManager>() as GameManager;

        rb = this.GetComponent<Rigidbody2D>();
        anim = enemy.GetComponent<Animator>();
        spRenderer = enemy.GetComponent<SpriteRenderer>();

        hitCollider = HitRange.GetComponent<CapsuleCollider2D>();
        shotCollider = ShotRange.GetComponent<CircleCollider2D>();
        diveCollider = DiveRange.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
       //isDead = true;
        if (!isDead)
        {
            if (!meleeAtk)
            {
                //controle de estado do inimigo  
                if (insideDashRadius || insideShotRadius)
                {

                    allowMovement = false;

                    if (insideShotRadius && !insideDashRadius)
                    {
                        ApontarPlayer();
                        shoot = true;
                    }

                    if (insideDashRadius)
                    {
                        shoot = false;
                        meleeAtk = true;
                    }
                }
                else
                {
                    ResetaRotacao();
                    allowMovement = true;
                    shoot = false;
                }

                if (shoot && !isShooting)
                    StartCoroutine("Shoot");
            }

            if (meleeAtk)
            {
                rb.velocity = this.transform.right * velocidadeDash;
            }
        }
        else
        {
            rb.velocity = this.transform.right * 0f;
        }
        
    }

    IEnumerator Shoot()
    {
        isShooting = true;
        anim.SetTrigger("Shoot");
        //dispara tiro
        GameObject bulletInstance = Instantiate(tiroPrefab, this.transform.position, this.transform.localRotation) as GameObject;
        Rigidbody2D rb2d = bulletInstance.GetComponent<Rigidbody2D>();

        rb2d.velocity = this.transform.right * 3f;

        yield return new WaitForSeconds(delayTiro);
        isShooting = false;

        //verificando se devemos atirar novamente
        //if(shoot)
        //    StartCoroutine("Shoot");
    }

    private void ResetaRotacao()
    {
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0f);

        spRenderer.flipY = false;
    }

    private void ApontarPlayer()
    {
        Vector3 difference = _gm.Player.transform.position - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        if (_gm.Player.transform.position.x < transform.position.x)
        {
            if (!spRenderer.flipY)
                spRenderer.flipY = true;
        }
        else
        {
            if (spRenderer.flipY)
                spRenderer.flipY = false;
        }
    }

    #region Colisores dos raios
    public void CollisionDetected(DashRadiusController dashColliderScript)
    {
        insideDashRadius = true;
    }
    public void CollisionLeave(DashRadiusController dashColliderScript)
    {
        insideDashRadius = false;
    }

    public void CollisionDetected(ShotRadiusController shotColliderScript)
    {
        insideShotRadius = true;
    }
    public void CollisionLeave(ShotRadiusController shotColliderScript)
    {
        insideShotRadius = false;
    }

    public void CollisionDetected(HitRadiusController hitColliderScript)
    {
        anim.SetTrigger("Grounded");
        rb.transform.localRotation = new Quaternion(0f, 180f, 0f, 0f);
        rb.velocity = this.transform.right * 0f;
        isDead = true;
    }
    #endregion


}
