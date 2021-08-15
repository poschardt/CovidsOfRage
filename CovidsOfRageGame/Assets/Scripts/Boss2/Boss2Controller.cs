using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Controller : Enemy
{

    [Header("Controle Ataque")]
    public GameObject atkPrefab;

    [Header("Movimentação")]
    public float velocidadeMovimento;
    public GameObject SpotA;
    public GameObject SpotB;

    private GameManager _gm;
    private bool isAttacking;
    private int destino = 0;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        _gm = FindObjectOfType<GameManager>() as GameManager;

        rb = this.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            //controle de movimentação
            #region Movimentação

            ControlemMovimentacao();
            #endregion

            //controle de ataque
            #region Atk
            if (!isAttacking)
            {
                Atacar();
            }
            #endregion
        }
        else
        {
            anim.SetTrigger("Die");
        }
    }

    private void ControlemMovimentacao()
    {
        if (destino == 0)
        {
            Vector3 posFinalBoss = new Vector3(SpotA.transform.position.x, this.transform.position.y, this.transform.position.z);
            this.transform.position = Vector3.MoveTowards(this.transform.position, posFinalBoss, velocidadeMovimento * Time.deltaTime);
        }
        else if (destino == 1)
        {
            Vector3 posFinalBoss = new Vector3(SpotB.transform.position.x, this.transform.position.y, this.transform.position.z);

            this.transform.position = Vector3.MoveTowards(this.transform.position, posFinalBoss, velocidadeMovimento * Time.deltaTime);
        }


        if (destino == 0 && (Math.Abs(this.transform.position.x - SpotA.transform.position.x) < 0.1f))
            destino = 1;

        if (destino == 1 && (Math.Abs(this.transform.position.x - SpotB.transform.position.x) < 0.1f))
            destino = 0;
        
    }

    private void Atacar()
    {
        isAttacking = true;
        anim.SetTrigger("Summon");

        float playerXposition = _gm.Player.transform.position.x;

        GameObject bulletInstance = Instantiate(atkPrefab, new Vector3(playerXposition, 0.7f, 0f), this.transform.localRotation) as GameObject;
        StartCoroutine("DelayAtk");

    }

    IEnumerator DelayAtk()
    {
        yield return new WaitForSeconds(attackRate);

        isAttacking = false;
    }
}
