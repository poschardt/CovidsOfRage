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

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Curavel")
    //    {
    //        collision.gameObject.GetComponent<Animator>().SetTrigger("Healed");

    //        print("BATEU");


    //        //Destroy(collision.gameObject);
    //        FindObjectOfType<UIManager>().UpdateHealthCount();
    //    }
    //}

    //void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        Debug.Log("Player Exited");
    //        isPlayerColliding = false;
    //    }
    //}

    //private IEnumerator KillOnAnimationEnd()
    //{
    //    yield return new WaitForSeconds(0.167f);
    //    Destroy(gameObject);
    //}

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
