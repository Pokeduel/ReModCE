﻿using System;
using MelonLoader;
using ReModCE.Core;
using ReModCE.Managers;
using ReModCE.UI;
using UnityEngine;

namespace ReModCE.Components
{
    internal class IngameLog : ModComponent
    {
        private ReScrollView _log;
        private string _preUiLog;

        public IngameLog()
        {
            MelonLogger.MsgCallbackHandler += (color, consoleColor, nameSection, msg) =>
            {
                if (nameSection != "ReModCE")
                    return;

                if (_log == null)
                {
                    _preUiLog += $"<color={ConsoleColorToHexCode(color)}>[{nameSection}]</color> <color={ConsoleColorToHexCode(consoleColor)}>{msg}</color>\n";
                    return;
                }

                _log.AddText($"<color={ConsoleColorToHexCode(color)}>[{nameSection}]</color> <color={ConsoleColorToHexCode(consoleColor)}>{msg}</color>\n");
            };
        }

        public override void OnUiManagerInit(UiManager uiManager)
        {
            var cameraButton = GameObject.Find("UserInterface/QuickMenu/ShortcutMenu/UserIconCameraButton");
            var cameraButtonPos = cameraButton.transform.localPosition;

            _log = new ReScrollView("ReModCELog", cameraButtonPos + new Vector3(-3165f, 0f), QuickMenu.prop_QuickMenu_0.transform.Find("ShortcutMenu"));
            _log.AddText(_preUiLog);

        }
        private static string ConsoleColorToHexCode(ConsoleColor c)
        {
            string[] cColors = {
                "#000000", //Black = 0
                "#000080", //DarkBlue = 1
                "#008000", //DarkGreen = 2
                "#008080", //DarkCyan = 3
                "#800000", //DarkRed = 4
                "#800080", //DarkMagenta = 5
                "#808000", //DarkYellow = 6
                "#C0C0C0", //Gray = 7
                "#808080", //DarkGray = 8
                "#0000FF", //Blue = 9
                "#00FF00", //Green = 10
                "#00FFFF", //Cyan = 11
                "#FF0000", //Red = 12
                "#FF00FF", //Magenta = 13
                "#FFFF00", //Yellow = 14
                "#FFFFFF"  //White = 15
            };
            return cColors[(int)c];
        }

    }
}