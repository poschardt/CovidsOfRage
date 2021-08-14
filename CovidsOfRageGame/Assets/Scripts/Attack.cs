using Assets.Scripts;
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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        Player2D player = other.GetComponent<Player2D>();
        if (enemy != null)
        {

            if(this.transform.position.x - other.transform.position.x > 0)
            {
                other.transform.position = new Vector3(other.transform.position.x - 0.01f, other.transform.position.y, other.transform.position.z);
            }
            else
            {
                other.transform.position = new Vector3(other.transform.position.x + 0.01f, other.transform.position.y, other.transform.position.z);

            }


            enemy.TookDamage(currentDamage);
        }
     
        if (player != null)
        {
            player.TookDamage(currentDamage);
        }
    }

  

}
