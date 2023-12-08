﻿using Logic.Helpers;
using Services.PersistentProgress;
using Services.SaveLoad;
using Services.SceneLoader;
using Services.StaticData;
using StaticData.Questions.Players;
using TMPro;
using UnityEngine;
using Zenject;

namespace Logic.Players
{
    public class Tasks : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Answer _answer;
        [SerializeField] private ArrangementOfVariants _arrangementOfVariants;
        [SerializeField] private UpdateTask _updateTask;
        
        [Header("Поле вопроса")]
        [SerializeField] private TextMeshProUGUI _textQuestion;

        public int CurrentQuestion => _currentQuestion;
        public int CurrentCategory => _currentCategory;

        private int _currentCategory;
        private int _currentQuestion;

        public PlayersStaticData PlayersStaticData { get; private set; }
        
        public IPersistentProgressService ProgressService { get; private set; }
        private IStaticDataService _staticDataService;
        private ISceneLoaderService _sceneLoaderService;
        private ISaveLoadService _saveLoadService;
        
        [Inject]
        private void Construct(IPersistentProgressService progressService, IStaticDataService staticDataService,
            ISceneLoaderService sceneLoader, ISaveLoadService saveLoadService)
        {
            ProgressService = progressService;
            _staticDataService = staticDataService;
            _sceneLoaderService = sceneLoader;
            _saveLoadService = saveLoadService;
        }

        private void Awake()
        {
            GetCurrentTask();
            GetCurrentStaticData();
        }

        private void Start() =>
            CheckTaskExists();

        public void GetCurrentTask()
        {
            _currentCategory = ActivePartition.CategoryNumber;
            _currentQuestion = ProgressService.GetUserProgress.PlayersData.Sets[_currentCategory - ForArrays.MinusOne];
        }

        private void GetCurrentStaticData() =>
            PlayersStaticData = _staticDataService.GetPlayersCategory(_currentCategory);

        public void CheckTaskExists()
        {
            if (_currentQuestion < PlayersStaticData.Questions.Count)
            {
                GetQuestion();
                PrepareTaskWithVariants();
            }
            else
            {
                ProgressService.GetUserProgress.AddCoins(350);
                ProgressService.GetUserProgress.AddScore(100);
                _saveLoadService.SaveProgress();
                _sceneLoaderService.Load(Scenes.Results.ToString());
            }
        }

        private void GetQuestion() =>
            _textQuestion.text = PlayersStaticData.Questions[_currentQuestion].Task;

        private void PrepareTaskWithVariants()
        {
            _answer.GetAnswer();
            _arrangementOfVariants.ArrangeVariants();
            _updateTask.TaskUpdateButton.SetActive(false);
        }
    }
}