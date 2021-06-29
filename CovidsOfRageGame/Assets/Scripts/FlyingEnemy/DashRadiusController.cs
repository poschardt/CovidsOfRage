using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashRadiusController : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.parent.GetComponent<FlyingEyeController>().CollisionDetected(this);
        }
    }
}
