using Game2D.Define;
using Game2D.ObjectScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Game2D.Controller
{
    public class GameController : MonoBehaviour
    {
        //他のスクリプトやゲームオブジェクトがイベントにsubscribe できるように管理する。
        public static Action OnGameStart;
        public static Action<int> OnGameFinished;

        public static Action<float> OnSecondUpdated;
        public static Action<int> OnScoreUpdated;
        public static Action<GAME_STATE> OnGameStateChanged;


        [SerializeField] private GameObject[] prizeGameObjects;
        [SerializeField] private int numberSpawn = 50;
        [SerializeField] private float gameSeconds = 30.0f;
        [SerializeField] private GameObject spawnObjectCollectionGobj;
        [SerializeField] private InputController inputController;

        [SerializeField] private CraneScript clawObject;

        

        private int score;
        private GAME_STATE _currentGameState ;
        public GAME_STATE currentGameState
        {
            get
            {
                return _currentGameState;
            }
            set
            {
                _currentGameState = value;
                OnGameStateChanged?.Invoke(_currentGameState);
            }
        }

        private void Start()
        {
            this.currentGameState = GAME_STATE.BEFORE_IN_GAME;
            RegisterCallback();
            GameStart();
        }

        /// <summary>
        /// ゲームを初期スタート
        /// </summary>
        private void GameStart()
        {
            SetScore(0);
            SpawnPrize();
            OnGameStart?.Invoke();
        }

        /// <summary>
        /// イベントコールバック登録
        /// </summary>
        private void RegisterCallback()
        {
            UIController.OnButtonControlPressed += OnButtonPressed;
            UIController.OnButtonControlReleased += OnButtonReleased;
            InputController.OnControlButtonKeyboardPressed += OnButtonPressed;
            InputController.OnControlButtonKeyboardReleased += OnButtonReleased;

            UIController.OnResetPressed += ResetScene;

            GoalScript.OnPrizeGet += OnPrizeGet;
        }

        /// <summary>
        /// コールバックunregister
        /// </summary>
        private void OnDestroy()
        {
            UIController.OnButtonControlPressed -= OnButtonPressed;
            UIController.OnButtonControlReleased -= OnButtonReleased;
            InputController.OnControlButtonKeyboardPressed -= OnButtonPressed;
            InputController.OnControlButtonKeyboardReleased -= OnButtonReleased;

            UIController.OnResetPressed -= ResetScene;

            GoalScript.OnPrizeGet -= OnPrizeGet;
        }

        /// <summary>
        /// ゲームをリセット、シーン再読み込み
        /// </summary>
        private void ResetScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }

        /// <summary>
        /// ボタンを押したとき、両方keyboard or UI によって押されたとき
        /// </summary>
        /// <param name="buttonState"></param>
        private void OnButtonPressed(BUTTON_STATE buttonState)
        {
            MoveCraneByButton(buttonState);
        }

        /// <summary>
        /// ボタンを離したとき、両方keyboard or UI によって離されたとき
        /// </summary>
        /// <param name="buttonState"></param>
        private void OnButtonReleased(BUTTON_STATE buttonState)
        {
            MoveCraneByButton(buttonState);
        }

        /// <summary>
        /// 景品出現させる
        /// </summary>
        private void SpawnPrize()
        {
            for(int i = 0; i < numberSpawn; i++)
            {
                GameObject toSpawn = prizeGameObjects[Random.Range(0,prizeGameObjects.Length)];
                
                toSpawn.transform.position = new Vector3(Random.Range(-5.0f, 9.69f), 0,0);

                GameObject temp = Instantiate(toSpawn);
                temp.transform.parent = this.spawnObjectCollectionGobj.transform;
            }
        }

        /// <summary>
        /// ボタン押されたときに実際ゲームのタイマーなどを始動する
        /// </summary>
        private void BeginGame()
        {
            if (this.currentGameState == GAME_STATE.BEFORE_IN_GAME)
            {
                this.currentGameState = GAME_STATE.IN_GAME;
                FindObjectOfType<AudioController>().PlayBgm(GameConstant.BGM);
            }
            
        }

        /// <summary>
        /// ゲーム終了しました
        /// </summary>
        private void FinishGame()
        {
            this.currentGameState = GAME_STATE.GAME_OVER;

            OnGameFinished?.Invoke(this.score);

            FindObjectOfType<AudioController>().StopBgm(GameConstant.BGM);
        }

        /// <summary>
        /// スコアを更新
        /// </summary>
        /// <param name="score"></param>
        public void SetScore(int score)
        {
            this.score = score;
            OnScoreUpdated?.Invoke(this.score);
        }

        /// <summary>
        /// 秒数を更新
        /// </summary>
        /// <param name="time"></param>
        public void SetSecond(float time)
        {
            this.gameSeconds = time;
            OnSecondUpdated?.Invoke(this.gameSeconds);
        }

        /// <summary>
        /// 景品を穴に落とした
        /// </summary>
        /// <param name="prize">ゲットしたprize</param>
        public void OnPrizeGet(PrizeScript prize)
        {
            FindObjectOfType<AudioController>().Play(GameConstant.SE_GREAT);
            AddScore();

            Destroy(prize.gameObject);

        }

        /// <summary>
        /// スコア+1加算
        /// </summary>
        public void AddScore()
        {
            SetScore(this.score+1);
        }

        /// <summary>
        /// ボタンによってクレーン移動
        /// </summary>
        /// <param name="buttonState"></param>
        public void MoveCraneByButton(BUTTON_STATE buttonState)
        {

            BeginGame();

            if (this.currentGameState != GAME_STATE.IN_GAME)
                return;

            switch (buttonState)
            {
                case BUTTON_STATE.UP_PRESSED:
                    this.clawObject.currentState = CRANE_STATE.MOVING_UP;
                    FindObjectOfType<AudioController>().Play(GameConstant.SE_CLAWMOVE);
                    break;
                case BUTTON_STATE.LEFT_PRESSED:
                    this.clawObject.currentState = CRANE_STATE.MOVING_LEFT;
                    FindObjectOfType<AudioController>().Play(GameConstant.SE_CLAWMOVE);
                    break;
                case BUTTON_STATE.RIGHT_PRESSED:
                    this.clawObject.currentState = CRANE_STATE.MOVING_RIGHT;
                    FindObjectOfType<AudioController>().Play(GameConstant.SE_CLAWMOVE);
                    break;
                case BUTTON_STATE.DOWN_PRESSED:
                    this.clawObject.currentState = CRANE_STATE.MOVING_DOWN;
                    FindObjectOfType<AudioController>().Play(GameConstant.SE_CLAWMOVE);
                    break;
                case BUTTON_STATE.CATCH_PRESSED:
                    if (this.clawObject.currentClawState == CRANE_CLAW_STATE.OPEN_CLAW)
                    {
                        this.clawObject.currentClawState = CRANE_CLAW_STATE.CLOSE_CLAW;
                    }
                    else
                    {
                        this.clawObject.currentClawState = CRANE_CLAW_STATE.OPEN_CLAW;
                    }
                    
                    break;

                case BUTTON_STATE.UP_RELEASED:
                case BUTTON_STATE.DOWN_RELEASED:
                case BUTTON_STATE.LEFT_RELEASED:
                case BUTTON_STATE.RIGHT_RELEASED:
                    FindObjectOfType<AudioController>().Stop(GameConstant.SE_CLAWMOVE);
                    this.clawObject.currentState = CRANE_STATE.STANDBY;
                    break;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (this.currentGameState == GAME_STATE.GAME_OVER)
                return;
            
            clawObject.OnUpdate();
            inputController.OnUpdate();

            if(this.currentGameState == GAME_STATE.IN_GAME)
            {
                SetSecond(this.gameSeconds - Time.deltaTime);
                if (this.gameSeconds <= 0)
                {
                    SetSecond(0);
                    this.FinishGame();

                }
            }

           

            if (this.clawObject.currentState == CRANE_STATE.MOVING_UP)
            {
                if (this.clawObject.transform.localPosition.y < 8.9f)
                    this.clawObject.transform.Translate(0, 0.03f * Time.deltaTime * 100f, 0);
            }

            if (this.clawObject.currentState == CRANE_STATE.MOVING_DOWN)
            {
               
                if(this.clawObject.transform.localPosition.y > 1.15f)
                    this.clawObject.transform.Translate(0,-0.03f * Time.deltaTime * 100f, 0);
            }

            if (this.clawObject.currentState == CRANE_STATE.MOVING_LEFT)
            {
                if (this.clawObject.transform.localPosition.x > -6.1f)
                    this.clawObject.transform.Translate(-0.05f * Time.deltaTime * 100f, 0, 0);
            }

            if (this.clawObject.currentState == CRANE_STATE.MOVING_RIGHT)
            {
                
                if (this.clawObject.transform.localPosition.x < 8.2f)
                    this.clawObject.transform.Translate(0.05f * Time.deltaTime * 100f, 0, 0);
            }

            
        }
    }

}
