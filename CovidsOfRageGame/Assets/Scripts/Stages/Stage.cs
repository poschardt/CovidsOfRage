using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Stages
{
    //Informações básicas da tela (tanto tela normal quanto tela de boss)
    public abstract class Stage 
    {
        public List<GameObject> inimigos { get; set; }
        public string currentStage { get; set; }
        public string nextStage { get; set; }
        public GameObject deadLine { get; set; }

        public abstract bool isStageComplete();

        public virtual void IncrementaVitimaSalva()
        {

        }

        public virtual void setFinishLine(bool value)
        {

        }
      
    }

    public enum StageType
    {
        Common,
        Boss
    }
}
