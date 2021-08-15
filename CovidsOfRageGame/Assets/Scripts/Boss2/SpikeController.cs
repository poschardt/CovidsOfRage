using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
   
    private void Update()
    {
        if (this.transform.position.y < -1.54)
            Destroy(this.gameObject);
    }
}
