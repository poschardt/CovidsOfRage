using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotController : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator anim;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    } 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ground" || collision.tag == "Player")
        {
            anim.SetTrigger("Collided");
            rig.velocity = new Vector2(0f, 0f);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this);   
    }
}
