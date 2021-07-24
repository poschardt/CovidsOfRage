using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        print("CHEGOU " + collision.gameObject.tag);
        if (collision.gameObject.tag == "Curavel")
        {
            Destroy(collision.gameObject);
            FindObjectOfType<UIManager>().UpdateHealthCount();
        }
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    print("CHEGOU " +other.tag );
    //    if(other.tag == "Curavel" )
    //    {
    //        print("Destruido");
    //        Destroy(other.gameObject);
    //    }
    //}
}
