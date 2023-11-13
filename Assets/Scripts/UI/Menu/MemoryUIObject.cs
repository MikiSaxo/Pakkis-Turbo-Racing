using System;
using System.Collections.Generic;
using Character;
using Collectible.Data;
using Dialog;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Menu
{
    public class MemoryUIObject : MenuUIObject
    {
        public TMP_Text MemoryTitleText;
        public string MemoryText { get; private set; }

        private string _categoryTitle;

        public override void Set(bool isActive)
        {
            base.Set(isActive);

            MemoryTitleText.text = _categoryTitle;
            MemoryTitleText.fontSize = isActive ? 40 : 32;
        }
    }
}