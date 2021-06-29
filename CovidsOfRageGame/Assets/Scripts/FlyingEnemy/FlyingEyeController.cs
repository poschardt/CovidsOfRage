using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeController : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public GameObject ShotRange;
    public GameObject DiveRange;

    private SpriteRenderer spRenderer;
    private Rigidbody2D rbEye;
    private CircleCollider2D shotCollider;
    private CircleCollider2D diveCollider;

    void Start()
    {
        rbEye = enemy.GetComponent<Rigidbody2D>();
        spRenderer = enemy.GetComponent<SpriteRenderer>();
        shotCollider = ShotRange.GetComponent<CircleCollider2D>();
        diveCollider = DiveRange.GetComponent<CircleCollider2D>();
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

    public void CollisionDetected(DashRadiusController childScript)
    {
        Debug.Log("collided dash");
    }

    public void CollisionDetected(ShotRadiusController childScript)
    {
        Debug.Log("shot collided");
    }
}
