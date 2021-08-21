using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Stages
{
    /// <summary>
    /// Para fases comuns, a condição de vitória é salvar um número mínimo de pessoas
    /// </summary>
    public class Stage_Common : Stage
    {
        public int pessoasSalvas_Minimo;
        public int pessoasSalvas_Atual;
        private bool finishLine;

        public Stage_Common(string currentStage, string nextStage, GameObject deadLine, int pessoasSalvas_Minimo)
        {
            this.currentStage = currentStage;
            this.nextStage = nextStage;
            this.deadLine = deadLine;
            this.pessoasSalvas_Minimo = pessoasSalvas_Minimo;

            pessoasSalvas_Atual = 0;
            finishLine = false;
        }

        public override bool isStageComplete()
        {
            if(finishLine && (pessoasSalvas_Atual == pessoasSalvas_Minimo))
                return true;

            return false;
        }

        public override void IncrementaVitimaSalva()
        {
            this.pessoasSalvas_Atual++;
        }

        public override void setFinishLine(bool value)
        {
            this.finishLine = value;
        }

    }
}
