using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadLine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.name);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") )
        {
            collision.gameObject.GetComponent<Player2D>().TookDamage(99999);
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TookDamage(99999);
        }
    }
}
