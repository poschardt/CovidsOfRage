using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class Enemy : Assets.Scripts.Personagem.Personagem
    {
        public float attackRate;
        protected float nextAttack;
        protected bool damaged;
        protected StageManager _gm;

        public override void TookDamage(int damage)
        {
            if (!isDead)
            {
                currentHealth -= damage;
                 FindObjectOfType<UIManager>().UpdateEnemyUI(maxHealth, currentHealth, characterName, characterImage);
            }
        }
    }

}
