using Game2D.Define;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneScript : MonoBehaviour
{
    public static Action<CRANE_STATE> OnCraneStateChanged;
    public static Action<CRANE_CLAW_STATE> OnClawStateChanged;

    private float mostOpenLeft = 319f;
    private float mostOpenRight = 43.0f;
    private float mostCloseLeft = 0.0f;
    [SerializeField]private GameObject leftClaw;
    [SerializeField]private GameObject rightClaw;
    [SerializeField] private float openCloseSpeed = 50.0f;

    private CRANE_STATE _currentState;
    public CRANE_STATE currentState
    {
        get
        {
            return _currentState;
        }
        set
        {
            _currentState = value;
            OnCraneStateChanged?.Invoke(_currentState);
        }
    }
    [SerializeField] private CRANE_CLAW_STATE _currentClawState = CRANE_CLAW_STATE.CLOSE_CLAW;
    public CRANE_CLAW_STATE currentClawState
    {
        get
        {
            return _currentClawState;
        }
        set
        {
            _currentClawState = value;
            OnClawStateChanged?.Invoke(_currentClawState);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.currentState = CRANE_STATE.STANDBY;
        this.currentClawState = CRANE_CLAW_STATE.CLOSE_CLAW;
    }

    /// <summary>
    /// GameController のUpdateにて1フレームごと呼び出される
    /// </summary>
    public void OnUpdate()
    {

        if (this.currentClawState == CRANE_CLAW_STATE.OPEN_CLAW)
        {

            if (leftClaw.transform.eulerAngles.z >= 320)
            {
                leftClaw.transform.Rotate(0, 0, (openCloseSpeed *-1) * Time.deltaTime);
            }
            else
            {
                leftClaw.transform.eulerAngles = new Vector3(0,0,mostOpenLeft);
            }
            if(rightClaw.transform.eulerAngles.z <= 40.0f)
            {
                rightClaw.transform.Rotate(0, 0, openCloseSpeed * Time.deltaTime);
            }
            else
            {
                rightClaw.transform.eulerAngles = new Vector3(0, 0, mostOpenRight);
            }
        }

        if (this.currentClawState == CRANE_CLAW_STATE.CLOSE_CLAW)
        {

            if (leftClaw.transform.eulerAngles.z > 10 && leftClaw.transform.eulerAngles.z < 360)
                leftClaw.transform.Rotate(0, 0, openCloseSpeed * Time.deltaTime);
            else
            {
                leftClaw.transform.eulerAngles = new Vector3(0, 0, 0);
            }

            if (rightClaw.transform.eulerAngles.z > 10 && rightClaw.transform.eulerAngles.z <360)
                rightClaw.transform.Rotate(0, 0, (openCloseSpeed * -1) * Time.deltaTime);
            else
            {
                rightClaw.transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
    }
}
