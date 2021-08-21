using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Stages
{
    /// <summary>
    /// Para fase com Boss, a condição de vitória é derrota-lo
    /// </summary>
    public class Stage_Boss : Stage
    {

        public GameObject Boss { get; set; }

        public Stage_Boss(string currentStage, string nextStage, GameObject deadLine, GameObject Boss)
        {
            this.currentStage = currentStage;
            this.nextStage = nextStage;
            this.deadLine = deadLine;
            this.Boss = Boss;
        }

        public override bool isStageComplete()
        {
            if (Boss.GetComponent<Enemy>().Verify_isDead())
                return true;

            return false;
        }

    
    }
}
