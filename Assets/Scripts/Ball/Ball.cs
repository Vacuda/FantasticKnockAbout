using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ot_OBJECTTYPE;
using static bt_BRICKTYPE;

public class Ball : MonoBehaviour, ICanCollideWith
{
    Animator anim;
    Rigidbody2D r_body;
    public GameLevel game_level;
    public Retro_PlayerController controller;
    private BallMovement ball_movement;
    private LineScript linescript;
    bool IsStuckToPaddle = false;

    private bool IsCatchable;
    private Paddle collided_paddle;
    private float time_stamp_for_catch;
    private float time_difference_for_holding;
    private float hold_threshold = 2.0f;

    Vector2 StoredVelocity;
    public ot_OBJECTTYPE object_type { get; set; } = ot_BALL;

    public bool IsKilledBySpikes = true;

    void Start()
    {
        anim = gameObject.transform.Find("BallSplatter").GetComponent<Animator>();
        r_body = gameObject.GetComponent<Rigidbody2D>();
        ball_movement = gameObject.GetComponent<BallMovement>();
        linescript = gameObject.transform.Find("Line").GetComponent<LineScript>();
        StoredVelocity = new Vector2(0.0f, 0.0f);    
    }

    void Update()
    {
        if (IsStuckToPaddle)
        {
            time_difference_for_holding = Time.time - time_stamp_for_catch;

            if(time_difference_for_holding > hold_threshold)
            {
                Release_Ball();
            }
        }
    }


    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    ICanCollideWith collided_object = collision.gameObject.GetComponent<ICanCollideWith>();

    //    if (collided_object != null)
    //    {
    //        //if paddle
    //        if (collided_object.object_type == ot_PADDLE)
    //        {

    //        }
    //    }
    //}


    private void OnCollisionEnter2D(Collision2D collision)
    {
        game_level.s_lab.PlaySound_Hit();

        ICanCollideWith collided_object = collision.gameObject.GetComponent<ICanCollideWith>();

        if(collided_object != null)
        {
            //if spike
            if(collided_object.object_type == ot_SPIKE)
            {
                //Debug
                if (IsKilledBySpikes)
                {
                    Death();
                }


            }

            //if brick
            if (collided_object.object_type == ot_BRICK)
            {
                bt_BRICKTYPE type = collision.gameObject.GetComponent<Brick>().brick_type;

                switch (type)
                {
                    case bt_YELLOW:
                        game_level.Change_Score(1);
                        break;
                    case bt_GREEN:
                        game_level.Change_Score(3);
                        break;
                    case bt_ORANGE:
                        game_level.Change_Score(5);
                        break;
                    case bt_RED:
                        game_level.Change_Score(7);
                        break;
                    default:
                        break;

                }
            }

            ////if paddle
            //if(collided_object.object_type == ot_PADDLE)
            //{
            //    if (gameObject.GetComponent<Paddle>().AbleToCatchBall)
            //    {

            //    }

            //    //store paddle
            //    collided_paddle = collision.gameObject.GetComponent<Paddle>();

            //    if (controller.IsInCatchMode == true)
            //    {


            //        IsStuckToPaddle = true;
            //        r_body.simulated = false;
            //        gameObject.transform.parent = collision.gameObject.transform;
            //        ball_movement.enabled = false;




            //        linescript.Trigger_LineDrawing(true);
        }
    }

    public void Stick_ToThisPaddle(Paddle paddle)
    {
        game_level.s_lab.PlaySound_Grab();

        IsStuckToPaddle = true;
        ball_movement.Stop_Ball(true);
        gameObject.transform.parent = paddle.gameObject.transform;
        linescript.Trigger_LineDrawing(true);

        time_stamp_for_catch = Time.time;
    }

    //void OnCollisionExit2D(Collision2D collision)
    //{
    //    ICanCollideWith collided_object = collision.gameObject.GetComponent<ICanCollideWith>();

    //    if (collided_object != null)
    //    {
    //        if (collided_object.object_type == ot_PADDLE)
    //        {
    //            if (controller.IsInCatchMode == true)
    //            {


    //                IsStuckToPaddle = true;
    //                r_body.simulated = false;
    //                gameObject.transform.parent = collision.gameObject.transform;
    //                ball_movement.enabled = false;




    //                linescript.Trigger_LineDrawing(true);
    //            }




    //        }
    //    }

    //    if (collided_object.object_type == ot_PADDLE)
    //    {
    //        if (controller.IsInCatchMode == true)
    //        {


    //            IsStuckToPaddle = true;
    //            r_body.simulated = false;
    //            gameObject.transform.parent = collision.gameObject.transform;
    //            ball_movement.enabled = false;




    //            linescript.Trigger_LineDrawing(true);
    //        }




    //    }
    //}

    public void Release_Ball()
    {
        if (IsStuckToPaddle)
        {

            game_level.s_lab.PlaySound_Release();
            IsStuckToPaddle = false;
            ball_movement.Stop_Ball(false);
            gameObject.transform.parent = null;
            linescript.Trigger_LineDrawing(false);
        }

    }
    
    public void Death()
    {
        anim.Play("state_Exploding");
        ball_movement.Stop_Ball(true);
        game_level.Trigger_OnDeath();
    }
}
