using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saida : MonoBehaviour
{

    public GameManager _gm;
    // Start is called before the first frame update
    void Start()
    {
        _gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if(_gm.totalVitimas == _gm.vitimasSalvas)
            {
                print("FIM DA FASE");
            }
        }
    }
}
