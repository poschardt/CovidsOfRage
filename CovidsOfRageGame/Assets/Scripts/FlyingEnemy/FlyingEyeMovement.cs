using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FlyingEyeMovement : MonoBehaviour
{
    public Transform[] moveSpots;
    public GameObject EnemeyEye;
    public float velocidadeInimigo;

    private int position = -1;
    private FlyingEyeController controller;

    private int posDestino;
    private Transform destino;
    bool esperar;

    // Start is called before the first frame update
    void Start()
    {
        posDestino = 0;
        destino = moveSpots[posDestino];

        controller = EnemeyEye.GetComponent<FlyingEyeController>();
    }

    private void FixedUpdate()
    {
        if (!esperar && controller.allowMovement)
        {
            EnemeyEye.transform.position = Vector3.MoveTowards(EnemeyEye.transform.position, destino.position, velocidadeInimigo * Time.deltaTime);

            if (EnemeyEye.transform.position == destino.position)
            {
                esperar = true;
                StartCoroutine("Aguardando");
                destino = moveSpots[TrocaDestino()];
            }
        }
    }

    private int TrocaDestino()
    {
        System.Random rnd = new System.Random();
        return rnd.Next(moveSpots.Length);
    }

    IEnumerator Aguardando()
    {
        yield return new WaitForSeconds(2f);
        esperar = false;
    }
}
