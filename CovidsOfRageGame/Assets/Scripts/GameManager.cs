using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public int totalVitimas;
    public int vitimasSalvas;
    // Start is called before the first frame update
    void Start()
    {
        vitimasSalvas = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncrementeVitimaSalva()
    {
        vitimasSalvas++;
    }
}
