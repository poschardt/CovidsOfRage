using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeController : MonoBehaviour
{
    public GameObject enemy;
    public GameObject ShotRange;
    public GameObject DiveRange;

    private SpriteRenderer spRenderer;
    private Rigidbody2D rbEye;
    private CircleCollider2D shotCollider;
    private CircleCollider2D diveCollider;

    private GameManager _gm;

    private bool insideShotRadius;
    private bool insideDashRadius;

    private bool shoot;

    public bool allowMovement;

    [Header("Controle de Tiro")]
    public GameObject tiroPrefab;
    public float delayTiro;

    void Start()
    {
        _gm = FindObjectOfType<GameManager>() as GameManager;

        rbEye = enemy.GetComponent<Rigidbody2D>();
        spRenderer = enemy.GetComponent<SpriteRenderer>();
        shotCollider = ShotRange.GetComponent<CircleCollider2D>();
        diveCollider = DiveRange.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //controle de estado do inimigo  
        if (insideDashRadius || insideShotRadius)
        {
            ApontarPlayer();
            allowMovement = false;

            if(insideShotRadius && !insideDashRadius)
            {
                shoot = true;
            }
        }
        else
        {
            ResetaRotacao();
            allowMovement = true;
            shoot = false;
        }

        if (shoot)
            StartCoroutine("Shoot");
    }

    IEnumerator Shoot()
    {
        //dispara tiro
        GameObject bulletInstance = Instantiate(tiroPrefab) as GameObject;
        Rigidbody2D rb2d = bulletInstance.GetComponent<Rigidbody2D>();

        rb2d.velocity = new Vector2(1f, 1);

        yield return new WaitForSeconds(delayTiro);

        //verificando se devemos atirar novamente
        if(shoot)
            StartCoroutine("Shoot");
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
    #endregion


}
