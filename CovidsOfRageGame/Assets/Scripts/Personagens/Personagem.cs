using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Personagem
{
    public abstract class Personagem : MonoBehaviour, IPersonagem
    {
        protected bool OnGround;
        protected Animator animator;
        protected Rigidbody2D rb;

        protected bool facingRight;
        protected bool jump;

        [Header("Character Info")]
        public string characterName;
        public Sprite characterImage;


        [Header("Chracter Attribute")]
        public int maxHealth = 10;
        public float walkSpeed;
        public float runSpeed;
        public float jumpForce;
        protected float currentSpeed;
        protected int currentHealth;
        protected bool isDead;
        protected Attack attack;


        public virtual void TookDamage(int damage)
        {
            if (!isDead)
            {
                currentHealth -= damage;
                FindObjectOfType<UIManager>().UpdateHealth(currentHealth);
            }
        }
        public virtual void OnCollisionEnter2D(Collision2D collision)
        {
            switch(LayerMask.LayerToName(collision.gameObject.layer))
            {
                case "Ground":
                    OnGround = true;
                    break;
            }
        }
        public virtual void OnCollisionStay2D(Collision2D collision)
        {
            switch (LayerMask.LayerToName(collision.gameObject.layer))
            {
                case "Ground":
                    OnGround = true;
                    break;
            }
        }
        public virtual void OnCollisionExit2D(Collision2D collision)
        {
            switch (LayerMask.LayerToName(collision.gameObject.layer))
            {
                case "Ground":
                    OnGround = false;
                    break;
            }
        }
        public virtual void Flip()
        {
            facingRight = !facingRight;

            Vector3 scale = transform.localScale;
            scale.x *= -1;

            transform.localScale = scale;
        }
        public virtual bool Verify_isDead()
        {
            return isDead;
        }
    }
}
