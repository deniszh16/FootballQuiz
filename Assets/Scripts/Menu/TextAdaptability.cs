﻿using UnityEngine;
using TMPro;

namespace Cubra
{
    public class TextAdaptability : MonoBehaviour
    {
        [Header("Короткий текст")]
        [SerializeField] private string _shortText;

        private TextMeshProUGUI _textComponent;

        private void Awake()
        {
            _textComponent = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            if (AspectRatio.Ratio <= 0.5f)
            {
                _textComponent.text = _shortText;
            }
        }
    }
}