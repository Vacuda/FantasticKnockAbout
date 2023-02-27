using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static w_WAVETYPE;

public class FloatMechanics : MonoBehaviour
{
    public Vector3 StartPosition;

    [Header("X")]
    public float x_speed = 1.0f;
    public float x_distance = 0.2f;
    public w_WAVETYPE x_wavetype = w_SIN;

    [Header("Y")]
    public float y_speed = 1.0f;
    public float y_distance = 0.2f;
    public w_WAVETYPE y_wavetype = w_SIN;

    [Header("Z")]
    public float z_speed = 1.0f;
    public float z_distance = 0.2f;
    public w_WAVETYPE z_wavetype = w_SIN;

    [Header("ROLL")]
    public float roll_speed = 1.0f;
    public float roll_rotation = 0.0f;
    public w_WAVETYPE roll_wavetype = w_SIN;

    [Header("PITCH")]
    public float pitch_speed = 1.0f;
    public float pitch_rotation = 0.0f;
    public w_WAVETYPE pitch_wavetype = w_SIN;

    [Header("YAW")]
    public float yaw_speed = 1.0f;
    public float yaw_rotation = 0.0f;
    public w_WAVETYPE yaw_wavetype = w_SIN;


    //TEMPORARY VECTOR CHANGE
    Vector3 vector_change = new Vector3(0.0f, 0.0f, 0.0f);

    public bool ActiveFloat = true;

    void Start()
    {
        StartPosition = gameObject.transform.position;
    }

    void Update()
    {
        if (ActiveFloat)
        {
            CalculateAndPerform_Floating();
        }

    }

    void CalculateAndPerform_Floating()
    {
        Vector3 CurrentPosition = gameObject.transform.position;

        //position change
        {
            //x change
            vector_change.x = Calculate_PositionChange(x_wavetype, x_distance, x_speed);
            //y change
            vector_change.y = Calculate_PositionChange(y_wavetype, y_distance, y_speed);
            //z change
            vector_change.z = Calculate_PositionChange(z_wavetype, z_distance, z_speed);

            //make position change with StartPosition in mind
            gameObject.transform.position = StartPosition + vector_change;
        }

        //rotation change
        {
            //x change
            vector_change.x = Calculate_PositionChange(roll_wavetype, roll_rotation, roll_speed);
            //y change
            vector_change.y = Calculate_PositionChange(pitch_wavetype, pitch_rotation, pitch_speed);
            //z change
            vector_change.z = Calculate_PositionChange(yaw_wavetype, yaw_rotation, yaw_speed);

            //make rotation change
            gameObject.transform.Rotate(vector_change);

        }
    }

    private float Calculate_PositionChange(w_WAVETYPE wavetype, float distance, float speed)
    {
        switch (wavetype)
        {
            case w_SIN: return Mathf.Cos(Time.time) * distance * speed;
            case w_COS: return Mathf.Sin(Time.time) * distance * speed;
            case w_TAN: return Mathf.Tan(Time.time) * distance * speed;
            case w_PINGPONG: return (Mathf.PingPong(Time.time, distance) * speed) - (distance / 2);
            default: return Mathf.Sin(Time.time) * distance * speed;
        }


    }
}
