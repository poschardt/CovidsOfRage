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

        private float enemyTimer;
        void Start()
        {
            player = FindObjectOfType<Player2D>();
     
            heathUI.maxValue = player.maxHealth;
            heathUI.value = heathUI.maxValue;
            playerName.text = player.playerName;
            playerImage.sprite = player.playerImage;
        }

        void Update()
        {

            if(phaseComplete)
            {
                //print("CABO A FASE");
                SceneManager.LoadScene("GameOver");
            }

            enemyTimer += Time.deltaTime;
            if (enemyTimer >= enemyUITime)
            {
                enemyUI.SetActive(false);
                enemyTimer = 0;
            }
        }


        public void UpdateHealth(int amount)
        {
            heathUI.value = amount;
        }

        public void UpdateEnemyUI(int maxHealth, int currentHealth, string name, Sprite image)
        {
            print("CHEGUEI");
            if (image == null)
                print("IMAGEM NULA");
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

            if(healtCount > 5)
            {
                phaseComplete = true;
            }
        }
    }

}
