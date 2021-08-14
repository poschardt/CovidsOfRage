using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Saida : MonoBehaviour
{
    public enum Fases
    {
        Fase1 = 0,
        Fase1_Boss,
        Fase2,
        Fase2_Boss
    }


    private Fases[] fases = new Fases[] { 
        Fases.Fase1 
        ,Fases.Fase1_Boss
        ,Fases.Fase2
        ,Fases.Fase2_Boss
    };
    public GameManager _gm;
    public Fases fase;
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
                SceneManager.LoadScene(Enum.GetName(typeof(Fases),this.fases[((int)this.fase)+ 1])) ;
            }
        }
    }
}
