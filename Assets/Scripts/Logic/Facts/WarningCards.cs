﻿using DZGames.Football.Helpers;
using UnityEngine;

namespace DZGames.Football.Facts
{
    public class WarningCards : MonoBehaviour
    {
        public int ReceivedCards => _receivedCards;
        
        [Header("Ссылки на компоненты")]
        [SerializeField] private Tasks _tasks;
        
        [Header("Футбольные карточки")]
        [SerializeField] private GameObject _yellowCard;
        [SerializeField] private GameObject _redCard;
        
        private int _receivedCards;

        public void GetCurrentCards()
        {
            _receivedCards = _tasks.ProgressService.GetUserProgress.FactsData
                .ReceivedCards[_tasks.CurrentCategory - ForArrays.MinusOne];
            ShowCard();
        }

        public void AddCard()
        {
            _receivedCards++;
            ShowCard();
        }

        private void ShowCard()
        {
            if (_receivedCards == 0)
            {
                _yellowCard.SetActive(false);
                _redCard.SetActive(false);
            }
            else if (_receivedCards == 1)
            {
                _yellowCard.SetActive(true);
                _redCard.SetActive(false);
            }
            else if (_receivedCards == 2)
            {
                _yellowCard.SetActive(true);
                _redCard.SetActive(true);
            }
        }
    }
}