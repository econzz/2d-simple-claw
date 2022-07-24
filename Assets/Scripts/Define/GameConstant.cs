using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game2D.Define
{
    public enum BUTTON_STATE
    {
        RIGHT_PRESSED,
        RIGHT_RELEASED,
        UP_PRESSED,
        UP_RELEASED,
        DOWN_PRESSED,
        DOWN_RELEASED,
        LEFT_PRESSED,
        LEFT_RELEASED,
        CATCH_PRESSED,
        CATCH_RELEASED
    }

    public enum CRANE_STATE
    {
        STANDBY,
        MOVING_LEFT,
        MOVING_RIGHT,
        MOVING_DOWN,
        MOVING_UP
    }

    public enum CRANE_CLAW_STATE
    {
        OPEN_CLAW,
        CLOSE_CLAW,
    }

    public enum GAME_STATE
    {
        BEFORE_IN_GAME,
        IN_GAME,
        GAME_OVER,
    }

    public class GameConstant : MonoBehaviour
    {
        public static string TOP_SCENE = "Top";
        public static float CLAW_OPENCLOSE_SPEED = 100.0f;
        public static string SE_HIT = "hit";
        public static string SE_GREAT = "great";
        public static string SE_NICE = "nice";
        public static string SE_CLAWMOVE = "crane_move";
        public static string SE_CLAWCATCHING = "crane_catching";

        public static string BGM = "bgm";
    }

}
