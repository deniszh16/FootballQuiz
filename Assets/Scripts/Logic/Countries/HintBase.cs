﻿using Services.PersistentProgress;
using Services.SaveLoad;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Logic.Countries
{
    public abstract class HintBase : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] protected Hints _hints;
        [SerializeField] protected AnswerFromLetters _answerFromLetters;
        [SerializeField] protected ArrangementOfLetters _arrangementOfLetters;
        
        [Header("Стоимость и доступность")]
        [SerializeField] protected int _price;
        [SerializeField] protected bool _availability = true;

        [Header("Кнопка подсказки")]
        [SerializeField] protected Button _button;
        [SerializeField] protected GameObject _buttonText;
        
        public GameObject ButtonGameObject  => _button.gameObject;

        protected IPersistentProgressService _progressService;
        protected ISaveLoadService _saveLoadService;

        [Inject]
        protected void Construct(IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        protected virtual void Start() =>
            _hints.PopupOpened += CheckHint;

        private void CheckHint()
        {
            bool check = _price > _progressService.GetUserProgress.Coins || _availability == false;
            CustomizeHintButton(state: !check);
        }

        private void CustomizeHintButton(bool state)
        {
            _button.interactable = state;
            _buttonText.SetActive(state);
        }

        protected abstract void UseHint();

        public virtual void ResetHintAvailability() =>
            _availability = true;

        protected virtual void OnDestroy() =>
            _hints.PopupOpened -= CheckHint;
    }
}