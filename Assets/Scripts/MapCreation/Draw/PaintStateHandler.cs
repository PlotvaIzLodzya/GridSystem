using System;
using UnityEngine;

namespace MapCreation
{
    [Serializable]
    public class PaintStateHandler
    {
        [field: SerializeField] public PaintState CurrentState { get; private set; }

        private void SetState(PaintState paintState)
        {
            CurrentState = paintState;
        }
    }
}
