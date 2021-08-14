using Assets;
using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EstadoBoss
{
    Aguardando,
    Atacando,
    Teleportando,
    Invocando,
    Alerta
}


public class Boss1Controller : Enemy
{
    public EstadoBoss estadoAtual;
    public float delayEstado;
    public float delayTeleporte;
    public float delayAtk;



    public GameObject minionPrefab;
    public Transform[] TeleportSpots;
    public Transform[] SpawnSpots;

    private bool invocando;
    private bool teleportando;
    private bool atacando;
    private bool morrendo;
    private int idxSpotAtual;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        _gm = FindObjectOfType<GameManager>() as GameManager;

        rb = this.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        idxSpotAtual = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            //corrigindo orientação
            #region Orientação do boss
            if (this.transform.position.x > _gm.Player.transform.position.x)
            {
                this.transform.localRotation = new Quaternion(0, 180, 0, 0);
            }
            else
            {
                this.transform.localRotation = new Quaternion(0, 0, 0, 0);
            }
            #endregion

            //alterando layer de acordo com estado
            if (teleportando)
            {
                this.gameObject.layer = LayerMask.NameToLayer("Invulneravel");
            }
            else
            {
                this.gameObject.layer = LayerMask.NameToLayer("Enemy");
            }

            //caso esteja aguardando, vamos mudar para o próximo estado de acordo com algum parâmetro
            if (estadoAtual == EstadoBoss.Aguardando)
            {
                //
                AlteraEstado(EstadoBoss.Invocando);
            }


            if (estadoAtual == EstadoBoss.Invocando && !invocando)
            {
                InvocarMinions();
            }

            if (estadoAtual == EstadoBoss.Teleportando && !teleportando)
            {
                Teleportar();
            }

            if (estadoAtual == EstadoBoss.Alerta)
            {
                if (Math.Abs(this.transform.position.x - _gm.Player.transform.position.x) < 0.8f &&
                   Math.Abs(this.transform.position.y - _gm.Player.transform.position.y) < 0.5f)
                {
                    if (!atacando)
                    {
                        Atacar();
                    }
                }
            }
        }
        else
        {
            if (!morrendo)
            {
                Morrer();
            }
        }
    }

    private void Morrer()
    {
        morrendo = true;
        anim.SetTrigger("Death");
    }

    private void Atacar()
    {
        atacando = true;
        anim.SetTrigger("Atk");
        StartCoroutine("DelayAtk");
    }

    IEnumerator DelayAtk()
    {
        yield return new WaitForSeconds(delayAtk);
        atacando = false;

        System.Random rnd = new System.Random();
        int num = rnd.Next(60);

        if (num < 20)
        {
            AlteraEstado(EstadoBoss.Invocando);
        }
        else if (num >= 20 && num < 40)
        {
            AlteraEstado(EstadoBoss.Teleportando);
        }
        else if (num >= 40 && num < 60)
        {
            AlteraEstado(EstadoBoss.Alerta);
        }
    }

    private void Teleportar()
    {
        teleportando = true;
        anim.SetTrigger("InicioTeleporte");

        StartCoroutine("DelayTeleporte");
        
    }

    IEnumerator DelayTeleporte()
    {
        yield return new WaitForSeconds(delayTeleporte);

        teleportando = false;
        AlteraEstado(EstadoBoss.Alerta);
    }

    public void MoverPosicao()
    {
        System.Random rnd = new System.Random();
        int num = rnd.Next(5);

        while(num == idxSpotAtual)
            num = rnd.Next(5);

        this.transform.position = TeleportSpots[num].transform.position;

        idxSpotAtual = num;
    }

    private void InvocarMinions()
    {
        invocando = true;
        anim.SetBool("Invocando", true);

        foreach(Transform spawnPoint in SpawnSpots)
        {
            GameObject minionInstance = Instantiate(minionPrefab, spawnPoint.position, spawnPoint.localRotation) as GameObject;
        }

        StartCoroutine("DelayInvocacao");
    }

    IEnumerator DelayInvocacao()
    {
        yield return new WaitForSeconds(delayEstado);
        
        invocando = false;
        anim.SetBool("Invocando", false);

        AlteraEstado(EstadoBoss.Teleportando);

    }


    private void AlteraEstado(EstadoBoss novoEstado)
    {
        this.estadoAtual = novoEstado;
    }

    public override void TookDamage(int damage)
    {
        Debug.Log("Custom hihi");
        if (!isDead)
        {
            damaged = true;
            currentHealth -= damage;
            anim.SetTrigger("Hitted");
            FindObjectOfType<UIManager>().UpdateEnemyUI(maxHealth, currentHealth, enemyName, enemyImage);

            this.gameObject.layer = LayerMask.NameToLayer("Invulneravel");
            StartCoroutine("DelayHit");


            if (currentHealth <= 0)
            {
                isDead = true;
                //rb.AddRelativeForce(new Vector3(3, 5, 0), ForceMode2D.Impulse);
            }
        }
    }

    IEnumerator DelayHit()
    {
        yield return new WaitForSeconds(1f);
        AlteraEstado(EstadoBoss.Teleportando);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    AlteraEstado(EstadoBoss.Teleportando);
    //    Debug.LogError("Chegou aqui");
    //}
}


