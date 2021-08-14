using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public Zombie zombie;

    private Transform zombiente;
    private Transform vitima;

    // Start is called before the first frame update
    void Start()
    {
        zombiente = zombie.transform.GetChild(0);
        vitima = zombie.transform.GetChild(1);

    }

    // Update is called once per frame
    void Update()
    {
        //vitima.position = this.transform.position;
    }
}
