using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

public class Vitima : MonoBehaviour
{
    private Animator anim;
    public List<RuntimeAnimatorController> animacoes;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();

        anim.runtimeAnimatorController = animacoes.PickRandom() as RuntimeAnimatorController;
    }

    // Update is called once per frame
    void Update()
    {

    }

 
}
