using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retro_PlayerController : MonoBehaviour
{

    //place to store controls
    protected PlayerControls Controls;

    public GameLevel game_level;

    public bool IsInCatchMode;
    private float time_stamp_for_button_press;
    public Ball ball;

    public Paddle[] paddle_array;
    private bool IsPaused = false;

    /*ADMIN FUNCTIONS*/

    private void Awake()
    {
        //creates new control object
        Controls = new PlayerControls();

        //clear cursor
        Cursor.visible = false;
    }

    void Start()
    {
        Controls.RetroGame.Mouse_LeftButton.performed += _ => OnLeftButton();
        Controls.RetroGame.Mouse_LeftButton.canceled += _ => OnLeftButton_Release();
        Controls.RetroGame.Keyboard_AnyKey.performed += _ => OnAnyButton_Pressed();
        Controls.RetroGame.Keyboard_Escape.performed += _ => OnEscapeButton_Pressed();
        //Controls.RetroGame.Mouse_RightButton.performed += _ => OnRightButton();


    }

    private void Update()
    {
        //if (IsInCatchMode)
        //{

        //    foreach(Paddle paddle in paddle_array)
        //    {
        //        if (paddle.AbleToCatchBall)
        //        {
        //            ball.Stick_ToThisPaddle(paddle);
        //            IsInCatchMode = false;

        //            break;
        //        }
        //    }
        //}
    }

    public void Exiting_CatchablePhase(Paddle paddle)
    {
        Debug.Log(Time.time - time_stamp_for_button_press);

        if (IsInCatchMode)
        {
            ball.Stick_ToThisPaddle(paddle);
        }
        //else if (Time.time - time_stamp_for_button_press < 1.5f)
        //{

        //    ball.Stick_ToThisPaddle(paddle);
        //}
    }

    public PlayerControls Get_Controls()
    {
        return Controls;
    }

    void OnLeftButton()
    {
        IsInCatchMode = true;
    }

    void OnLeftButton_Release()
    {
        IsInCatchMode = false;
        time_stamp_for_button_press = Time.time;
        ball.Release_Ball();
    }

    void OnAnyButton_Pressed()
    {

        if (IsPaused)
        {
            game_level.UnPauseGame();
            IsPaused = false;
        }
        else
        {
            game_level.PauseGame();

            IsPaused = true;
        }
    }



    void OnEscapeButton_Pressed()
    {
        Debug.Log("should quit");
        Application.Quit();

    }


    //enables controls when this script is active
    private void OnEnable()
    {
        Controls.Enable();
    }

    //disable controls when this script is inactive
    private void OnDisable()
    {
        Controls.Disable();
    }

}
