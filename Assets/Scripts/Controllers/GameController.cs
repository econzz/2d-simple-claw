using Game2D.Define;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game2D.Controller
{
    public class GameController : MonoBehaviour
    {
        public GameObject clawGameObject;
        public GameObject[] prizeGameObjects;
        public int numberSpawn = 50;
        public float gameSeconds = 30.0f;

        public GameObject spawnObjectCollectionGobj;

        private CraneScript clawObject;

        

        private int score;

        private UIController uiController;

        private GAME_STATE _currentGameState ;
        public GAME_STATE currentGameState
        {
            get
            {
                return _currentGameState;
            }
            set
            {
                _currentGameState = value;    //５未満なら代入する
            }
        }

        // Start is called before the first frame update
        void Awake()
        {
            this.clawObject = clawGameObject.GetComponent<CraneScript>();
            this.uiController = FindObjectOfType<UIController>();

            this.SpawnPrize();

            this.currentGameState = GAME_STATE.BEFORE_IN_GAME;
            this.uiController.ToggleUIBasedOnState(this.currentGameState);
        }

        private void Start()
        {
            this.SetScore(0);
        }

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

        private void BeginGame()
        {
            if (this.currentGameState == GAME_STATE.BEFORE_IN_GAME)
            {
                this.currentGameState = GAME_STATE.IN_GAME;
                FindObjectOfType<AudioController>().PlayBgm(GameConstant.BGM);
            }
            
        }

        private void FinishGame()
        {
            this.currentGameState = GAME_STATE.GAME_OVER;
            this.uiController.SetScoreGameOverText(this.score);
            this.uiController.ToggleUIBasedOnState(this.currentGameState);

            FindObjectOfType<AudioController>().StopBgm(GameConstant.BGM);
        }

        public void SetScore(int score)
        {
            this.score = score;
            this.uiController.SetScoreText(this.score);
        }

        public void OnPrizeGet()
        {
            FindObjectOfType<AudioController>().Play(GameConstant.SE_GREAT);
            this.AddScore();

            Debug.Log(this.spawnObjectCollectionGobj.transform.childCount);
        }

        public void AddScore()
        {
            this.score++;
            this.uiController.SetScoreText(this.score);
        }

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

            if(this.currentGameState == GAME_STATE.IN_GAME)
            {
                this.gameSeconds -= Time.deltaTime;
                uiController.SetSecondText(gameSeconds);
                if (this.gameSeconds <= 0)
                {
                    this.gameSeconds = 0;
                    uiController.SetSecondText(gameSeconds);

                    this.FinishGame();

                }
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                this.MoveCraneByButton(BUTTON_STATE.DOWN_PRESSED);
            }

            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                this.MoveCraneByButton(BUTTON_STATE.DOWN_RELEASED);
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                this.MoveCraneByButton(BUTTON_STATE.UP_PRESSED);
            }

            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                this.MoveCraneByButton(BUTTON_STATE.UP_RELEASED);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                this.MoveCraneByButton(BUTTON_STATE.LEFT_PRESSED);
            }

            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                this.MoveCraneByButton(BUTTON_STATE.LEFT_RELEASED);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                this.MoveCraneByButton(BUTTON_STATE.RIGHT_PRESSED);
            }

            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                this.MoveCraneByButton(BUTTON_STATE.RIGHT_RELEASED);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.MoveCraneByButton(BUTTON_STATE.CATCH_PRESSED);
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
