﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReModCE.Loader;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace ReModCE.UI
{
    internal class ReQuickToggle : UIElement
    {
        private string _name;

        private readonly GameObject _toggleStateOn;
        private readonly GameObject _toggleStateOff;

        private readonly Button _buttonComponent;

        public bool Interactable
        {
            get => _buttonComponent.interactable;
            set => _buttonComponent.interactable = value;
        }

        public ReQuickToggle(Vector2 pos, string text, string tooltip, Action<bool> onToggle, bool defaultValue = false, Transform parent = null) : base(GameObject.Find("UserInterface/QuickMenu/UserInteractMenu/BlockButton"), parent, pos, $"{text}Toggle")
        {
            _name = $"{text}Toggle";
            var textComponent = gameObject.GetComponentInChildren<Text>();
            textComponent.text = text;

            var tooltipComponent = gameObject.GetComponent<UiTooltip>();
            tooltipComponent.field_Public_String_0 = tooltip;
            tooltipComponent.field_Public_String_1 = tooltip;

            _toggleStateOn = rectTransform.Find("Toggle_States_Visible/ON").gameObject;
            _toggleStateOff = rectTransform.Find("Toggle_States_Visible/OFF").gameObject;

            _toggleStateOn.GetComponentsInChildren<Text>()[0].text = $"{text}\nON";
            _toggleStateOn.GetComponentsInChildren<Text>()[0].resizeTextForBestFit = true;
            _toggleStateOff.GetComponentsInChildren<Text>()[0].text = $"{text}\nON";
            _toggleStateOff.GetComponentsInChildren<Text>()[0].resizeTextForBestFit = true;

            _toggleStateOn.GetComponentsInChildren<Text>()[1].text = $"{text}\nOFF";
            _toggleStateOn.GetComponentsInChildren<Text>()[1].resizeTextForBestFit = true;
            _toggleStateOff.GetComponentsInChildren<Text>()[1].text = $"{text}\nOFF";
            _toggleStateOff.GetComponentsInChildren<Text>()[1].resizeTextForBestFit = true;

            _buttonComponent = gameObject.GetComponent<Button>();
            _buttonComponent.onClick = new Button.ButtonClickedEvent();
            _buttonComponent.onClick.AddListener(new Action(() =>
            {
                var toggled = !_toggleStateOn.activeSelf;
                Toggle(toggled);
                onToggle(toggled);
            }));

            Toggle(defaultValue);
        }

        public void Toggle(bool value)
        {
            _toggleStateOn.SetActive(value);
            _toggleStateOff.SetActive(!value);
        }
    }
}