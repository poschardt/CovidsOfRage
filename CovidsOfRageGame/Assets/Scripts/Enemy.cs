using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour
    {
        protected int currentHealth;
        public int maxHealth;
        protected bool isDead = false;
        protected bool damaged = false;

        protected Animator anim;
        protected Rigidbody2D rb;

        public string enemyName;
        public Sprite enemyImage;



        public void TookDamage(int damage)
        {
            if (!isDead)
            {
                damaged = true;
                currentHealth -= damage;
                anim.SetTrigger("Hitted");
                FindObjectOfType<UIManager>().UpdateEnemyUI(maxHealth, currentHealth, enemyName, enemyImage);
                if (currentHealth <= 0)
                {
                    isDead = true;
                    rb.AddRelativeForce(new Vector3(3, 5, 0), ForceMode2D.Impulse);
                }
            }
        }
    }

}
