using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoxaMele_Buffer : MonoBehaviour
{
    // Start is called before the first frame update
    private Enemy enemy;
    void Start()
    {
        enemy = this.transform.GetChild(0).GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {

        if (enemy.Verify_isDead())
         Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            var atk = collision.transform.Find("Attack").GetComponent<Attack>();

            if (atk != null)
            {
                collision.GetComponent<Renderer>().material.color = Color.red;
                atk.currentDamage = atk.damage + 5;
            }

        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            var atk = collision.transform.Find("Attack").GetComponent<Attack>();

            if (atk != null)
            {
                collision.GetComponent<Renderer>().material.color = Color.red;
                atk.currentDamage = atk.damage + 5;
            }

        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {

            var atk = collision.transform.Find("Attack").GetComponent<Attack>();

            if (atk != null)
            {
                collision.GetComponent<Renderer>().material.color = Color.white;
                atk.currentDamage = atk.damage;
            }

        }
    }
}
