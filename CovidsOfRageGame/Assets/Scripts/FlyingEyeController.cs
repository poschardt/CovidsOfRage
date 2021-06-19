using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeController : MonoBehaviour
{
    private Rigidbody2D rbEye;
    private SpriteRenderer spRenderer;
    public GameObject player;

    

    void Start()
    {
        rbEye = GetComponent<Rigidbody2D>();
        spRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 difference = player.transform.position - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        if(player.transform.position.x < transform.position.x)
        {
            if (!spRenderer.flipY)
                spRenderer.flipY = true;
        }
        else
        {
            if (spRenderer.flipY)
                spRenderer.flipY = false;
        }
    }
}
