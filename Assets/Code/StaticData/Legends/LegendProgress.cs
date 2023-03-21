﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.StaticData.Legends
{
    [Serializable]
    public class LegendProgress
    {
        [Header("Клубные достижения")]
        public List<string> Club;
        
        [Header("Достижения в сборной")]
        public List<string> NationalTeam;
        
        [Header("Личные достижения")]
        public List<string> PersonalAchievements;
    }
}