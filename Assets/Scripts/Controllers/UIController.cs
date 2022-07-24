using Game2D.Define;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game2D.Controller
{
    public class UIController : MonoBehaviour
    {
        public GameObject inGameUiPanel;
        public GameObject gameOverUiPanel;

        public GameObject scoreTextGobj;
        public GameObject secondTextGobj;

        public GameObject scoreGameOverTextGobj;

        private GameController gameController;

        private Text scoreText;
        private Text secondText;
        private Text scoreGameOverText;

        // Start is called before the first frame update
        void Awake()
        {
            this.gameController = FindObjectOfType<GameController>();

            this.scoreText = this.scoreTextGobj.GetComponent<Text>();
            this.secondText = this.secondTextGobj.GetComponent<Text>();
            this.scoreGameOverText = this.scoreGameOverTextGobj.GetComponent<Text>();
        }

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

        public void SetScoreText(int score)
        {
            this.scoreText.text = ""+score;
        }

        public void SetScoreGameOverText(int score)
        {
            this.scoreGameOverText.text = "" + score;
        }


        public void SetSecondText(float seconds)
        {
            this.secondText.text = "" + seconds;
        }

        public void OnRightButtonPressed()
        {
            this.gameController.MoveCraneByButton(BUTTON_STATE.RIGHT_PRESSED);
           
        }

        public void OnRightButtonReleased()
        {
            Debug.Log("rightbutton released");
            this.gameController.MoveCraneByButton(BUTTON_STATE.RIGHT_RELEASED);
        }

        public void OnLeftButtonPressed()
        {
            Debug.Log("leftbutton pressed");
            this.gameController.MoveCraneByButton(BUTTON_STATE.LEFT_PRESSED);

        }

        public void OnLeftButtonReleased()
        {
            Debug.Log("leftbutton released");
            this.gameController.MoveCraneByButton(BUTTON_STATE.LEFT_RELEASED);
        }

        public void OnUpButtonPressed()
        {
            Debug.Log("upbutton pressed");
            this.gameController.MoveCraneByButton(BUTTON_STATE.UP_PRESSED);
        }

        public void OnUpButtonReleased()
        {
            Debug.Log("upbutton released");
            this.gameController.MoveCraneByButton(BUTTON_STATE.UP_RELEASED);
        }

        public void OnDownButtonPressed()
        {
            Debug.Log("downbutton pressed");
            this.gameController.MoveCraneByButton(BUTTON_STATE.DOWN_PRESSED);
        }

        public void OnDownButtonReleased()
        {
            Debug.Log("downbutton released");
            this.gameController.MoveCraneByButton(BUTTON_STATE.DOWN_RELEASED);
        }

        public void OnCatchButtonPressed()
        {
            Debug.Log("catch pressed");
            this.gameController.MoveCraneByButton(BUTTON_STATE.CATCH_PRESSED);
        }

        public void OnCatchButtonReleased()
        {
            Debug.Log("catch released");
            this.gameController.MoveCraneByButton(BUTTON_STATE.CATCH_RELEASED);
        }

        public void OnBackButtonPressed()
        {
            SceneManager.LoadScene("Top", LoadSceneMode.Single);
            
        }

        public void OnResetButtonPressed()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void OnRestartGameOverButtonPressed()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}

