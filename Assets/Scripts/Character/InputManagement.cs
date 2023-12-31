﻿using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Character
{
    public class InputManagement : MonoBehaviour
    {
        private GameplayInputs _gameplayInputs;

        public GameplayInputs GameplayInputs { get { return _gameplayInputs; } private set { _gameplayInputs = value; } }
        [SerializeField] float DeadzoneJoystick = 0.3f;
        [SerializeField] float DeadzoneJoystickTrigger = 0.3f;
        [field:SerializeField] public InputsEnum Inputs { get; private set; }

        private void Awake()
        {
            _gameplayInputs = new GameplayInputs();
            _gameplayInputs.Enable();
        }

        private void Update()
        {
            GatherInputs();
        }


        private void GatherInputs()
        {
            InputsEnum inputsEnum = Inputs;
            
            // inputsEnum.RotateLeft = _gameplayInputs.Boat.StaticRotateLeft.ReadValue<float>();
            // inputsEnum.RotateRight = _gameplayInputs.Boat.StaticRotateRight.ReadValue<float>();

            inputsEnum.Deadzone = DeadzoneJoystick;

            inputsEnum.AnyButton = _gameplayInputs.Boat.AnyButton.ReadValue<float>() > 0.3f;
            inputsEnum.Start = _gameplayInputs.Boat.ShowLeaveMenu.ReadValue<float>() > 0.3f;

            Inputs = inputsEnum;
        }

        // public void PaddleLeft(InputAction.CallbackContext context)
        // {
        //     InputsEnum inputsEnum = Inputs;
        //     // inputsEnum.PaddleLeft = _gameplayInputs.Boat.PaddleLeft.ReadValue<float>() > DeadzoneJoystickTrigger;
        //     // inputsEnum.PaddleLeft = context.ReadValue<bool>();
        //     // inputsEnum.PaddleLeft = context.action.triggered;
        //     Debug.Log("aloed");
        //     Inputs = inputsEnum;
        // }
        // public void PaddleRight(InputAction.CallbackContext context)
        // {
        //     InputsEnum inputsEnum = Inputs;
        //     // inputsEnum.PaddleRight = _gameplayInputs.Boat.PaddleRight.ReadValue<float>() > DeadzoneJoystickTrigger;
        //     inputsEnum.PaddleRight = context.ReadValue<bool>();
        //     inputsEnum.PaddleRight = context.action.triggered;
        //     Inputs = inputsEnum;
        // }
    }

    [Serializable]
    public struct InputsEnum
    {
        // public bool PaddleLeft;
        // public bool PaddleRight;
        
        // public float RotateLeft;
        // public float RotateRight;
        
        public float Deadzone;
        
        public bool AnyButton;
        public bool Start;
    }
}