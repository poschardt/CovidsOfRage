using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotRadiusController : MonoBehaviour
{

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.parent.GetComponent<FlyingEyeController>().CollisionDetected(this);
        }
    }
}
