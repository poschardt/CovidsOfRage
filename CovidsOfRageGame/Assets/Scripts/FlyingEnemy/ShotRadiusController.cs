using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotRadiusController : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.parent.GetComponent<FlyingEyeController>().CollisionDetected(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.parent.GetComponent<FlyingEyeController>().CollisionLeave(this);
        }
    }
}
