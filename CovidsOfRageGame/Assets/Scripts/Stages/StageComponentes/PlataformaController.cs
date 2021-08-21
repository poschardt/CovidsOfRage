using Assets.Scripts.Personagem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaController : MonoBehaviour
{
    public Transform[] moveSpots;
    public float velocidadePlataforma;
    public float tempoEspera;

    private Transform destino;
    private bool esperar;
    private int indexDestino = 0;
    private bool ordemCrescente;

    // Start is called before the first frame update
    void Start()
    {
        destino = moveSpots[indexDestino];
    }

    // Update is called once per frame
    void Update()
    {
        if (!esperar)
            this.transform.position = Vector3.MoveTowards(this.transform.position, destino.position, velocidadePlataforma * Time.deltaTime);

        if (this.transform.position == destino.position)
        {
            esperar = true;
            StartCoroutine("Aguardando");
            TrocaDestino();
            destino = moveSpots[indexDestino];
        }
    }

    private void TrocaDestino()
    {
        if (indexDestino == moveSpots.Length - 1 && ordemCrescente)
            ordemCrescente = false;

        if (indexDestino == 0 && !ordemCrescente)
            ordemCrescente = true;

        indexDestino += ordemCrescente ? 1 : -1; 
    }

    IEnumerator Aguardando()
    {
        yield return new WaitForSeconds(tempoEspera);
        esperar = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.transform.name);
        var personagem = collision.gameObject.GetComponent<Personagem>();

        if(personagem != null)
        {
            print(collision.transform.name);

            personagem.transform.SetParent(this.transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        print(collision.transform.name);

        var personagem = collision.gameObject.GetComponent<Personagem>();

        if (personagem != null)
        {
            print(collision.transform.name);

            personagem.transform.SetParent(null);
        }
    }
}
