using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion1Controller : Enemy
{
    public float velocidadeMovimento;
    public float tempoCaminhada;

    [Header("Controle de Tiro")]
    public Transform arma;
    public GameObject tiroPrefab;
    public float attackRate;

    private bool atacando;
    private bool triggerDead;

    private bool andar;
    private bool andarTimer;
    private bool invertido;

    void Start()
    {
        currentHealth = maxHealth;
        _gm = FindObjectOfType<GameManager>() as GameManager;

        rb = this.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        isDead = currentHealth <= 0;

        if(isDead && !triggerDead)
        {
            anim.SetTrigger("Dead");
            triggerDead = true;
        }

        //caso não esteja morto, vamos tomar ações
        if (!isDead)
        {
            if (andar)
            {
                Andar();
            }
            else
            {
                DefineComportamento();
            }

            anim.SetBool("isMoving", Math.Abs(rb.velocity.x) > 0);


            if (Math.Abs(this.transform.position.x - _gm.Player.transform.position.x) > 1f && !atacando)
                AnimAtk();
        }
    }

    private void DefineComportamento()
    {
        //criação de uma valor randômico
        System.Random rnd = new System.Random();

        #region Controle Caminhada
        int num = rnd.Next(100);
        if (num < 50)
        {
            andar = true;

            #region Controle Rotação
            num = rnd.Next(100);
            if (num < 50)
                Rotacionar();
            #endregion
        }

        #endregion
    }

    private void Rotacionar()
    {
        Debug.Log("Rotacionar");
        if (invertido)
        {
            this.transform.localRotation = new Quaternion(0f, 180f, 0f, 0f);
            arma.transform.localRotation = new Quaternion(0f, 180f, 0f, 0f);
        }
        else
        {
            this.transform.localRotation = new Quaternion(0f, 0, 0f, 0f);
            arma.transform.localRotation = new Quaternion(0f, 0, 0f, 0f);
        }

        invertido = !invertido;
    }

    private void Andar()
    {
        int direcao = invertido ? 1 : -1;
        rb.velocity = new Vector3(velocidadeMovimento * direcao, rb.velocity.y, 0f);
         
        if(!andarTimer) //devemos iniciar a corrotina só uma vez
            StartCoroutine("DelayAndar");
    }

    IEnumerator DelayAndar()
    {
        andarTimer = true;
        yield return new WaitForSeconds(tempoCaminhada);

        andar = false;
        andarTimer = false;
    }

    private void AnimAtk()
    {
        atacando = true;
        anim.SetTrigger("Atk");
    }

    private void Atacar()
    {
        GameObject bulletInstance = Instantiate(tiroPrefab, arma.transform.position, arma.transform.localRotation) as GameObject;
        Rigidbody2D rb2d = bulletInstance.GetComponent<Rigidbody2D>();

        rb2d.velocity = this.transform.right * 3f;

        StartCoroutine("DelayAtk");
    }

    IEnumerator DelayAtk()
    {
        yield return new WaitForSeconds(attackRate);

        atacando = false;
    }
}
