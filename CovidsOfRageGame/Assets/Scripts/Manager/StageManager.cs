using Assets.Scripts.Stages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    public GameObject Player;
    public GameObject Boss;

    [SerializeField]
    public StageType stageType;
    public List<GameObject> inimigos;
    public string currentStage_name;
    public string nextStage_name;
    public int pessoasSalvas_Minimo;
    public GameObject deadLine;
    public Stage currentStage;
 
    // Start is called before the first frame update
    void Start()
    {
        if (stageType == StageType.Common)
            currentStage = new Stage_Common(currentStage_name, nextStage_name, deadLine, pessoasSalvas_Minimo);
        else
            currentStage = new Stage_Boss(currentStage_name, nextStage_name, deadLine,Boss);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentStage.isStageComplete())
            SceneManager.LoadScene(currentStage.nextStage);

    }

   
}
