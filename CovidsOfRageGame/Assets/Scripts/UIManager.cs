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
        public Slider heathUI;
        public Image playerImage;
        public Text playerName;
        public Text livesText;
        private Player2D player;

        public GameObject enemyUI;
        public Slider enemySlider;
        public float enemyUITime = 4f;
        public Text enemyName;
        public Image enemyImage;


        private bool phaseComplete = false;
        private int healtCount = 0;
        private GameManager _gm;
        private float enemyTimer;
        public Text placar;
        void Start()
        {
            player = FindObjectOfType<Player2D>();
            _gm = FindObjectOfType<GameManager>();

            heathUI.maxValue = player.maxHealth;
            heathUI.value = heathUI.maxValue;
            playerName.text = player.playerName;
            playerImage.sprite = player.playerImage;
        }

        void Update()
        {

            if(phaseComplete)
            {
                //SceneManager.LoadScene("");
            }

            enemyTimer += Time.deltaTime;
            if (enemyTimer >= enemyUITime)
            {
                enemyUI.SetActive(false);
                enemyTimer = 0;
            }
            if(_gm != null && placar != null)
            placar.text = String.Format("Vítimas Salvas : {0}/{1}",_gm.vitimasSalvas,_gm.totalVitimas) ;
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
