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
    }

}
