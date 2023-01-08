using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapCreation
{

    [Serializable]
    public class Inputs
    {
        [field: SerializeField] public static InputHandler LeftMouseButton { get; private set; } = new InputHandler(() => (Event.current.type == EventType.MouseDrag || Event.current.type == EventType.MouseDown) && Event.current.button == 0, InputButtonEvent.LMBHolded);
        [field: SerializeField] public static InputHandler RightMouseButton { get; private set; } = new InputHandler(() => (Event.current.type == EventType.MouseDrag || Event.current.type == EventType.MouseDown) && Event.current.button == 1, InputButtonEvent.RMBHolded);
        [field: SerializeField] public static InputHandler Control { get; private set; } = new InputHandler(() => Event.current.control, InputButtonEvent.CTRLHolded);

        public static InputButtonEvent Current { get; private set; }

        public void Update()
        {

            if (Event.current.button == 1)
                return;

            LeftMouseButton.Update();
            RightMouseButton.Update();
            //Control.Update();

            if (Control.IsPressed)
                Current = Control.Type;
            else if (RightMouseButton.IsPressed)
                Current = RightMouseButton.Type;
            else if (LeftMouseButton.IsPressed)
                Current = LeftMouseButton.Type;
            else
                Current = InputButtonEvent.None;
        }
    }

    public enum InputButtonEvent
    {
        None,
        LMBHolded,
        RMBHolded,
        CTRLHolded
    }
}

