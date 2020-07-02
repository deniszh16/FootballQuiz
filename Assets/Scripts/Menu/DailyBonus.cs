﻿using UnityEngine;
using UnityEngine.UI;

namespace Cubra
{
    public class DailyBonus : MonoBehaviour
    {
        [Header("Эффект сияния")]
        [SerializeField] private GameObject _effect;

        [Header("Бонусная кнопка")]
        [SerializeField] private Button _bonusButton;

        private void Start()
        {
            CheckQuantityBonus();
        }

        /// <summary>
        /// Проверка количества ежедневных бонусов
        /// </summary>
        private void CheckQuantityBonus()
        {
            if (PlayerPrefs.GetInt("bonus") > 0)
            {
                _bonusButton.gameObject.SetActive(true);
                _effect.SetActive(true);
            }
        }

        /// <summary>
        /// Использование бонуса
        /// </summary>
        public void UseBonus()
        {
            PlayerPrefs.SetInt("bonus", PlayerPrefs.GetInt("bonus") - 1);
            CheckQuantityBonus();
        }
    }
}