using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoController : MonoBehaviour
{
    private bool isPressed;
    private Animator anim;

    public float delayBotao;

    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isPressed)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerHit"))
            {
                isPressed = true;
                anim.SetBool("Pressed", true);
                DerrubaSpikes();

                StartCoroutine("DelayBotao");
            }
        }        
    }

    IEnumerator DelayBotao()
    {
        yield return new WaitForSeconds(delayBotao);
        isPressed = false;
        anim.SetBool("Pressed", false);
    }

    private void DerrubaSpikes()
    {
        GameObject currentSpike = GameObject.FindGameObjectWithTag("spike");

        GameObject spikeClone = currentSpike;

        if (currentSpike != null)
        {
            currentSpike.GetComponent<Rigidbody2D>().gravityScale = 1f;
            CriaNovoSpike(spikeClone);
        }
        else
        {
            Debug.Log("Não achou spike");
        }
    }

    private void CriaNovoSpike(GameObject currentSpike)
    {
        System.Random rnd = new System.Random();
        int posX = rnd.Next(5);

        if (posX == 5) posX = -1;

        Vector3 posNova = new Vector3(posX, currentSpike.transform.position.y, currentSpike.transform.position.z);
        GameObject spikeClone = Instantiate(currentSpike, posNova, currentSpike.transform.localRotation) as GameObject;
        spikeClone.GetComponent<Rigidbody2D>().gravityScale = 0f;
    }
}
