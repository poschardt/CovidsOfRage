using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    public class UIManager : MonoBehaviour
    {
        public Slider heathUI;

        private Player2D player;
        public GameObject enemyUI;

        public Slider enemySlider;
        public float enemyUITime = 4f;

        private float enemyTimer;
        void Start()
        {
            player = FindObjectOfType<Player2D>();
            heathUI.maxValue = player.maxHealth;
            heathUI.value = heathUI.maxValue;
        }

        void Update()
        {
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
            enemySlider.maxValue = maxHealth;
            enemySlider.value = currentHealth;
          //  enemyName.text = name;
         //   enemyImage.sprite = image;
            enemyTimer = 0;
            enemyUI.SetActive(true);
        }
    }

}
