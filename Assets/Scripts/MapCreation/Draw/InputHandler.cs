using System;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

namespace MapCreation
{
    [Serializable]
    public class InputHandler
    {
        [SerializeField] private bool IsEnabled = true;
        [field: SerializeField] public readonly InputButtonEvent Type;

        private Func<bool> _isCorrectInput;

        public bool IsPressed { get; private set; }

        public InputHandler(Func<bool> isCorrectInput, InputButtonEvent buttonType)
        {
            _isCorrectInput = isCorrectInput;
            Type = buttonType;
        }

        public void Update()
        {
            if (IsEnabled == false)
                return;

            bool isEventUsed = false;

            Event currentEvent = Event.current;

            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));

            if (_isCorrectInput())
            {
                isEventUsed = true;
                currentEvent.Use();
            }

            IsPressed = isEventUsed;
        }
    }
}

