using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitRadiusController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            transform.parent.GetComponent<FlyingEyeController>().CollisionDetected(this);
    }
}
