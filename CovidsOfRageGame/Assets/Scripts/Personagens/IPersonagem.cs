using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Personagem
{
    interface IPersonagem
    {
        public void TookDamage(int damage);
        public void Flip();
        public bool Verify_isDead();
    }
}
