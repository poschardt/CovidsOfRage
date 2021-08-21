using Assets.Scripts;
using Assets.Scripts.Personagem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadLine : MonoBehaviour
{
    // Start is called before the first frame update

    void OnTriggerEnter2D(Collider2D collision)
    {
        var personagem = collision.gameObject.GetComponent<Personagem>();

        if (personagem != null)
            personagem.TookDamage(9999);

        //if (collision.gameObject.layer == LayerMask.NameToLayer("Player") )
        //{
        //    collision.gameObject.GetComponent<Player2D>().TookDamage(99999);
        //}
        //else if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        //{
        //    collision.gameObject.GetComponent<Enemy>().TookDamage(99999);
        //}
    }
}
