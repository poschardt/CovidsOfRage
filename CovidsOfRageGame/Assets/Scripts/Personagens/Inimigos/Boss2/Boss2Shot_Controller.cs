using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Shot_Controller : MonoBehaviour
{
    public GameObject Attack;

    public void AtivaAttack()
    {
        Attack.SetActive(true);
    }

    public void DesativaAttack()
    {
        Attack.SetActive(false);
    }
}
