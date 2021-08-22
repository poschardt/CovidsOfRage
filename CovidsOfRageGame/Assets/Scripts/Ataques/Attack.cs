using Assets.Scripts;
using Assets.Scripts.Personagem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;
    public int currentDamage;
    // Start is called before the first frame update
    void Start()
    {
        currentDamage = damage;
    }

   

    private void OnTriggerEnter2D(Collider2D other)
    {
        Personagem characterHitted = other.GetComponent<Personagem>();
        if (characterHitted != null)
        {

            characterHitted.TookDamage(currentDamage);

        }
    }

  

}
