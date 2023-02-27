using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ot_OBJECTTYPE;

public class Paddle : MonoBehaviour, ICanCollideWith
{
    public Retro_PlayerController retro_controller;

    public ot_OBJECTTYPE object_type { get; set; }

    /* STORAGE MEMBERS */
    float screen_width;
    float screen_height;
    float retro_min_x_position = -16.625f;
    float retro_max_x_position = 2.625f;
    float min_x_position = -16.0625f;
    float max_x_position = 10.0625f;
    float min_y_position = -11.0625f;
    float max_y_position = 11.0625f;
    float mouse_x;
    float mouse_y;
    float new_x;
    float new_y;
    Vector3 pos;


    PlayerControls Controls;

    public bool IsSidePaddle = false;
    public bool IsRetroPaddle = false;

    public bool AbleToCatchBall = false;

    private void Start()
    {
        //get controls
        Controls = retro_controller.Get_Controls();

        //if side paddle
        if (IsSidePaddle)
        {
            //make new position variable
            pos = new Vector3(gameObject.transform.position.x, 0.0f, 0.0f);
        }
        //if not side paddle
        else
        {
            //make new position variable
            pos = new Vector3(0.0f, gameObject.transform.position.y, 0.0f);
        }

        //set object type
        object_type = IsRetroPaddle ? ot_RETROPADDLE : ot_PADDLE;

        //get screen width
        screen_width = Screen.width; //1920
        screen_height = Screen.height; //907
        //screen_width = Display.displays[0].renderingWidth; //1920
        //screen_height = Display.displays[0].renderingHeight; //907

        //Debug.Log(screen_width);
        //Debug.Log(screen_height);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsRetroPaddle)
        {
            Set_PaddlePosition_Retro();
        }
        else
        {
            Set_PaddlePosition();
        }
    }

    void Set_PaddlePosition()
    {
        /*
         
         
        Take center of screen
        75% left is full movement
         
         
         
         
         
         
         
         
         get perc of your mouse across your display
         
         
         
         
         
         
         */





















        //if side paddle
        if (IsSidePaddle)
        {
            //Vector2 mouseposition = Controls.RetroGame.Mouse_PointerLocation.ReadValue<Vector3>();



            //get mouse_y
            mouse_y = Controls.RetroGame.Mouse_PointerLocation.ReadValue<Vector2>().y;

            //get new_y
            new_y = min_y_position + ((mouse_y / (screen_height * 0.75f)) * (max_y_position - min_y_position));

            //clamp, if necessary
            new_y = Mathf.Clamp(new_y, min_y_position, max_y_position);

            //change position
            pos.y = new_y;
        }
        //if not side paddle
        else
        {
            //get mouse_x
            mouse_x = Controls.RetroGame.Mouse_PointerLocation.ReadValue<Vector2>().x;

            //get new_x
            new_x = min_x_position + ((mouse_x / (screen_width * 0.75f)) * (max_x_position - min_x_position));

            //clamp, if necessary
            new_x = Mathf.Clamp(new_x, min_x_position, max_x_position);

            //change position
            pos.x = new_x;
        }

        //set new position
        gameObject.transform.position = pos;
    }

    public void Set_PaddlePosition_Retro()
    {
        //get mouse_x
        mouse_x = Controls.RetroGame.Mouse_PointerLocation.ReadValue<Vector2>().x;

        //get new_x
        new_x = retro_min_x_position + ((mouse_x / (screen_width * 0.75f)) * (retro_max_x_position - retro_min_x_position));

        //clamp, if necessary
        new_x = Mathf.Clamp(new_x, retro_min_x_position, retro_max_x_position);

        //change position
        pos.x = new_x;
    
        //set new position
        gameObject.transform.position = pos;
    }

    public void Destroy_Paddle()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        ICanCollideWith collided_object = collider.gameObject.GetComponent<ICanCollideWith>();

        if (collided_object != null)
        {
            //if ball
            if (collided_object.object_type == ot_BALL)
            {
                AbleToCatchBall = true;
            }
        }   
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        ICanCollideWith collided_object = collider.gameObject.GetComponent<ICanCollideWith>();

        if (collided_object != null)
        {
            //if ball
            if (collided_object.object_type == ot_BALL)
            {
                AbleToCatchBall = false;

                retro_controller.Exiting_CatchablePhase(this);
            }
        }
    }
}
