using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("OPA "+other.name);   
        Enemy enemy = other.GetComponent<Enemy>();
        Player2D player = other.GetComponent<Player2D>();

        if (enemy != null)
        {
            print("INIMIGO TOMANDO DANO: "+ damage.ToString());

            if(this.transform.position.x - other.transform.position.x > 0)
            {
                other.transform.position = new Vector3(other.transform.position.x - 0.01f, other.transform.position.y, other.transform.position.z);
            }
            else
            {
                other.transform.position = new Vector3(other.transform.position.x + 0.01f, other.transform.position.y, other.transform.position.z);

            }


            enemy.TookDamage(damage);
        }
     
        if (player != null)
        {
            player.TookDamage(damage);
        }
    }
}
