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
        public float attackRate;
        public float damageTime;
        public float maxSpeed;
        protected Transform target;
        public void TookDamage(int damage)
        {
            if (!isDead)
            {
                rb.velocity = new Vector2(0, 0);
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

        public bool get_isDead()
        {
            return isDead;
        }
    }

}
