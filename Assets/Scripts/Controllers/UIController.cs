using Game2D.Define;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game2D.Controller
{
    public class UIController : MonoBehaviour
    {
        public static Action<BUTTON_STATE> OnButtonControlPressed;
        public static Action<BUTTON_STATE> OnButtonControlReleased;

        public static Action OnResetPressed;

        [SerializeField] private GameObject inGameUiPanel;
        [SerializeField] private GameObject gameOverUiPanel;
        [SerializeField] private Text scoreText;
        [SerializeField] private Text secondText;

        [SerializeField] private Text scoreGameOverText;


        // Start is called before the first frame update
        void Awake()
        {
            RegisterCallback();
        }


        /// <summary>
        /// イベントコールバック登録
        /// </summary>
        private void RegisterCallback()
        {
            GameController.OnScoreUpdated += OnScoreUpdated;
            GameController.OnSecondUpdated += OnSecondUpdated;
            GameController.OnGameStateChanged += ToggleUIBasedOnState;

            GameController.OnGameFinished += OnGameFinished;
        }

        /// <summary>
        /// コールバックunregister
        /// </summary>
        private void OnDestroy()
        {
            GameController.OnScoreUpdated -= OnScoreUpdated;
            GameController.OnSecondUpdated -= OnSecondUpdated;
            GameController.OnGameStateChanged -= ToggleUIBasedOnState;

            GameController.OnGameFinished -= OnGameFinished;
        }

        /// <summary>
        /// ゲーム終了したときに呼び出す
        /// </summary>
        /// <param name="finalScore"></param>
        private void OnGameFinished(int finalScore)
        {
            SetScoreGameOverText(finalScore);
        }

        /// <summary>
        /// スコアを変更されたときに呼び出される
        /// </summary>
        /// <param name="score"></param>
        private void OnScoreUpdated(int score)
        {
            SetScoreText(score);
        }

        /// <summary>
        /// 秒数が変更されたときに呼び出される
        /// </summary>
        /// <param name="time"></param>
        private void OnSecondUpdated(float time)
        {
            SetSecondText(time);
        }

        /// <summary>
        /// ゲームのstate によって UI 変わる
        /// </summary>
        /// <param name="state"></param>
        public void ToggleUIBasedOnState(GAME_STATE state)
        {

            //this.inGameUiPanel.SetActive(false);
            this.gameOverUiPanel.SetActive(false);
            
            if (state == GAME_STATE.IN_GAME)
            {
              //  this.inGameUiPanel.SetActive(true);
            }
            else if (state == GAME_STATE.GAME_OVER)
            {
                this.gameOverUiPanel.SetActive(true);
            }
            

        }

        /// <summary>
        /// スコアテキストを更新
        /// </summary>
        /// <param name="score"></param>
        public void SetScoreText(int score)
        {
            this.scoreText.text = ""+score;
        }

        /// <summary>
        /// ゲームオーバーにあるスコアテキストを更新
        /// </summary>
        /// <param name="score"></param>
        public void SetScoreGameOverText(int score)
        {
            this.scoreGameOverText.text = "" + score;
        }

        /// <summary>
        /// 秒数テキスト更新
        /// </summary>
        /// <param name="seconds"></param>
        public void SetSecondText(float seconds)
        {
            this.secondText.text = "" + seconds;
        }

        /// <summary>
        /// →ボタン押す
        /// </summary>
        public void OnRightButtonPressed()
        {
            OnButtonControlPressed?.Invoke(BUTTON_STATE.RIGHT_PRESSED);
        }

        /// <summary>
        /// →ボタン離す
        /// </summary>
        public void OnRightButtonReleased()
        {
            OnButtonControlReleased?.Invoke(BUTTON_STATE.RIGHT_RELEASED);
            
        }

        /// <summary>
        /// ←ボタン押す
        /// </summary>
        public void OnLeftButtonPressed()
        {
            OnButtonControlPressed?.Invoke(BUTTON_STATE.LEFT_PRESSED);

        }

        /// <summary>
        /// ←ボタン離す
        /// </summary>
        public void OnLeftButtonReleased()
        {
            OnButtonControlReleased?.Invoke(BUTTON_STATE.LEFT_RELEASED);
        }

        /// <summary>
        /// ↑ボタン押す
        /// </summary>
        public void OnUpButtonPressed()
        {
            OnButtonControlPressed?.Invoke(BUTTON_STATE.UP_PRESSED);
        }

        /// <summary>
        /// ↑ボタン離す
        /// </summary>
        public void OnUpButtonReleased()
        {
            OnButtonControlReleased?.Invoke(BUTTON_STATE.UP_RELEASED);
        }

        /// <summary>
        /// ↓ボタン押す
        /// </summary>
        public void OnDownButtonPressed()
        {
            OnButtonControlPressed?.Invoke(BUTTON_STATE.DOWN_PRESSED);
        }

        /// <summary>
        /// ↓ボタン離す
        /// </summary>
        public void OnDownButtonReleased()
        {
            OnButtonControlReleased?.Invoke(BUTTON_STATE.DOWN_RELEASED);
        }

        /// <summary>
        /// キャッチボタン押す
        /// </summary>
        public void OnCatchButtonPressed()
        {

            OnButtonControlPressed?.Invoke(BUTTON_STATE.CATCH_PRESSED);
        }

        /// <summary>
        /// キャッチボタン離す
        /// </summary>
        public void OnCatchButtonReleased()
        {
            OnButtonControlReleased?.Invoke(BUTTON_STATE.CATCH_RELEASED);
        }

        /// <summary>
        /// リスタートボタン押す
        /// </summary>
        public void OnResetButtonPressed()
        {
            OnResetPressed?.Invoke();
        }

        /// <summary>
        /// リスタートボタン押す
        /// </summary>
        public void OnRestartGameOverButtonPressed()
        {
            OnResetPressed?.Invoke();
        }

    }

}

