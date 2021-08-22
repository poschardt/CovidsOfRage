using Assets.Scripts.Stages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets
{
    public class UIManager : MonoBehaviour
    {
        [Header("Character Info")]
        public Slider heathUI;
        public Image playerImage;
        public Text playerName;
        public Text livesText;
        private Player2D player;

        [Header("Enemy Info")]
        public GameObject enemyUI;
        public Slider enemySlider;
        public float enemyUITime = 4f;
        public Text enemyName;
        public Image enemyImage;


        public StageManager _gm;
        private bool phaseComplete = false;
        private int healtCount = 0;
        private float enemyTimer;
        [Header("Score Info")]
        public Text placar;
        void Start()
        {
            player = _gm.Player.GetComponent<Player2D>();

            heathUI.maxValue = player.maxHealth;
            heathUI.value = heathUI.maxValue;
            playerName.text = player.characterName;
            playerImage.sprite = player.characterImage;
        }

        void Update()
        {

            if(phaseComplete)
            {
                SceneManager.LoadScene(_gm.currentStage.nextStage);
            }

            //Controle de tempo da UI do Inimigo
            enemyTimer += Time.deltaTime;
            if (enemyTimer >= enemyUITime)
            {
                enemyUI.SetActive(false);
                enemyTimer = 0;
            }

            //Placar (caso tenha)
            var stage = _gm.currentStage as Stage_Common;
            if(stage != null)
            {
                placar.text = String.Format("Vítimas Salvas : {0}/{1}",stage.pessoasSalvas_Atual, stage.pessoasSalvas_Minimo);
            }
        }


        public void UpdateHealth(int amount)
        {
            heathUI.value = amount;
        }

        public void UpdateEnemyUI(int maxHealth, int currentHealth, string name, Sprite image)
        {

            enemySlider.maxValue = maxHealth;
            enemySlider.value = currentHealth;
            enemyName.text = name;
            enemyImage.sprite = image;
            enemyTimer = 0;
            enemyUI.SetActive(true);
        }

        public void UpdateHealthCount()
        {
            healtCount++;

            if(healtCount > 0)
            {
                phaseComplete = true;
            }
        }
    }

}
