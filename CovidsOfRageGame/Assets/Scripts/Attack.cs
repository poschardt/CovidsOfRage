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
        print("DANO");
        Enemy enemy = other.GetComponent<Enemy>();
        Player2D player = other.GetComponent<Player2D>();
        if (enemy != null)
        {
            enemy.TookDamage(damage);
        }

        if (player != null)
        {
            player.TookDamage(damage);
        }
    }
}
