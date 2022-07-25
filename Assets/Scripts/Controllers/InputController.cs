using Game2D.Define;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game2D.Controller
{
    public class InputController : MonoBehaviour
    {
        public static Action<BUTTON_STATE> OnControlButtonKeyboardPressed;
        public static Action<BUTTON_STATE> OnControlButtonKeyboardReleased;

        // Start is called before the first frame update
        void Start()
        {

        }

        /// <summary>
        /// GameController のUpdateにて1フレームごと呼び出される
        /// </summary>
        public void OnUpdate()
        {
            CheckKeyboard();
        }

        /// <summary>
        /// keyboard 処理
        /// </summary>
        private void CheckKeyboard()
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                OnControlButtonKeyboardPressed?.Invoke(BUTTON_STATE.DOWN_PRESSED);
            }

            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                OnControlButtonKeyboardReleased?.Invoke(BUTTON_STATE.DOWN_RELEASED);
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                OnControlButtonKeyboardPressed?.Invoke(BUTTON_STATE.UP_PRESSED);
            }

            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                OnControlButtonKeyboardReleased?.Invoke(BUTTON_STATE.UP_RELEASED);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                OnControlButtonKeyboardPressed?.Invoke(BUTTON_STATE.LEFT_PRESSED);
            }

            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                OnControlButtonKeyboardReleased?.Invoke(BUTTON_STATE.LEFT_RELEASED);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                OnControlButtonKeyboardPressed?.Invoke(BUTTON_STATE.RIGHT_PRESSED);
            }

            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                OnControlButtonKeyboardReleased?.Invoke(BUTTON_STATE.RIGHT_RELEASED);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnControlButtonKeyboardPressed?.Invoke(BUTTON_STATE.CATCH_PRESSED);
            }
        }

        
    }
}

