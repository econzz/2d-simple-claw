using Game2D.Define;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneScript : MonoBehaviour
{
    private float mostOpenLeft = 319f;
    private float mostOpenRight = 43.0f;
    private float mostCloseLeft = 0.0f;
    private GameObject leftClaw;
    private GameObject rightClaw;

    private CRANE_STATE _currentState;
    public CRANE_STATE currentState
    {
        get
        {
            return _currentState;
        }
        set
        {
            _currentState = value;    //５未満なら代入する
        }
    }
    public CRANE_CLAW_STATE currentClawState = CRANE_CLAW_STATE.CLOSE_CLAW;
    /*private CRANE_CLAW_STATE _currentClawState;
    public CRANE_CLAW_STATE currentClawState
    {
        get
        {
            return _currentClawState;
        }
        set
        {
            _currentClawState = value;    //５未満なら代入する
        }
    }*/

    // Start is called before the first frame update
    void Awake()
    {
        this.currentState = CRANE_STATE.STANDBY;
        this.currentClawState = CRANE_CLAW_STATE.OPEN_CLAW;

        this.leftClaw = this.transform.Find("LeftClaw").gameObject;
        this.rightClaw = this.transform.Find("RightClaw").gameObject;
    }

 
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("leftclaw2 " + leftClaw.transform.eulerAngles.z);
        //Debug.Log("rightclaw2 " + rightClaw.transform.eulerAngles.z);
        //Debug.Log(this.currentClawState);
        if (this.currentClawState == CRANE_CLAW_STATE.OPEN_CLAW)
        {

            if (leftClaw.transform.eulerAngles.z >= 320)
            {
                leftClaw.transform.Rotate(0, 0, -50f * Time.deltaTime);
            }
            else
            {
                leftClaw.transform.eulerAngles = new Vector3(0,0,mostOpenLeft);
            }
            if(rightClaw.transform.eulerAngles.z <= 40.0f)
            {
                rightClaw.transform.Rotate(0, 0, 50f * Time.deltaTime);
            }
            else
            {
                rightClaw.transform.eulerAngles = new Vector3(0, 0, mostOpenRight);
            }
            /*if (leftClaw.transform.eulerAngles.z <350)
            {
                leftClaw.transform.Rotate(0, 0, 1f);
            }
            if (rightClaw.transform.eulerAngles.z > 10)
            {
                rightClaw.transform.Rotate(0, 0, 1f);
            }*/

            /*Quaternion desiredRotation = Quaternion.Euler(0, 0, -40);
            leftClaw.transform.rotation = Quaternion.Lerp(leftClaw.transform.rotation, desiredRotation, 100.0f);

            Quaternion desiredRotationR = Quaternion.Euler(0, 0, 40);
            rightClaw.transform.rotation = Quaternion.Lerp(rightClaw.transform.rotation, desiredRotationR, 100.0f); */
        }

        if (this.currentClawState == CRANE_CLAW_STATE.CLOSE_CLAW)
        {

            if (leftClaw.transform.eulerAngles.z > 10 && leftClaw.transform.eulerAngles.z < 360)
                leftClaw.transform.Rotate(0, 0, 50f * Time.deltaTime);
            else
            {
                leftClaw.transform.eulerAngles = new Vector3(0, 0, 0);
            }

            if (rightClaw.transform.eulerAngles.z > 10 && rightClaw.transform.eulerAngles.z <360)
                rightClaw.transform.Rotate(0, 0, -50f * Time.deltaTime);
            else
            {
                rightClaw.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            //Debug.Log("left"+leftClaw.transform.eulerAngles);
            //Debug.Log("right"+rightClaw.transform.eulerAngles);
            /*if (leftClaw.transform.eulerAngles.z > 300)
            {
                leftClaw.transform.Rotate(0, 0, -1f);
            }
            if (rightClaw.transform.eulerAngles.z < 60)
            {
                rightClaw.transform.Rotate(0, 0, 1f );
            }*/
        }
    }
}
