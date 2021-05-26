﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCore
{
    [TSVReader.Data("ID")]
    public class SkillData
    {
        public int ID;
        public string Name;
        public string ClassName;
        public string SpritePath;
        public float CoolTime;
        public float AttackRange;
        public int SkillDamage;
        public string MontageName;
        public string FXPrefabPath;
        public float FXDelay;
    }
}