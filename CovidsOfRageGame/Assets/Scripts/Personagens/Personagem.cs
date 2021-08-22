using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Utils;

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
        public bool knocking = false;

        [Header("Chracter Attribute")]
        public int maxHealth = 10;
        public float walkSpeed;
        public float runSpeed;
        public float jumpForce;
        protected float currentSpeed;
        protected int currentHealth;
        protected bool isDead;
        public GameObject attack;


        public abstract void TookDamage(int damage);
      
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

        protected IEnumerator KnockBack(float knockTime)
        {
            if(rb != null)
            {
                yield return new WaitForSeconds(knockTime);
                knocking = false;

                rb.velocity = Vector2.zero;
            }
        }

        protected IEnumerator Flash(int numberOfFlashes,float flashDuration, SpriteRenderer sprite, Color flashColor)
        {
            int temp = 0;
            int defaultLayer = this.gameObject.layer;
            this.gameObject.layer = LayerMask.NameToLayer("Invulneravel");
            while(temp < numberOfFlashes)
            {
                sprite.color = flashColor;
                yield return new WaitForSeconds(flashDuration);
                sprite.color = Color.white;
                yield return new WaitForSeconds(flashDuration);
                temp++;
            }
            this.gameObject.layer = defaultLayer;
        }
 

        public virtual void Atk(EnumBoolean valor)
        {
           if(valor == EnumBoolean.Verdadeiro)
                this.attack.gameObject.SetActive(true);
           else
                this.attack.gameObject.SetActive(false);

        }

    }
}
