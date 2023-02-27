using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverBase : MonoBehaviour
{
    public bool bLinearMovement = true;

    bool ActiveMove = false;

    public float target_x = 0.0f;
    public float target_y = 1.0f;
    public float target_z = -10.0f;
    public float duration = 1.0f;
    public float lerp_speed = 4.50f;
    public float lerp_snap_distance = 0.03f;


    private Vector3 TargetPosition;
    private Vector3 StartPosition;
    private Vector3 OutsidePosition;
    private Vector3 InsidePosition;
    private float time_start;



    // Start is called before the first frame update
    void Awake()
    {
        StartPosition = gameObject.transform.position;
        TargetPosition = new Vector3(target_x, target_y, target_z);
        InsidePosition = gameObject.transform.position;
        OutsidePosition = new Vector3(target_x, target_y, target_z);

        //Debug.Log("inside" + InsidePosition);
        //Debug.Log("out" + OutsidePosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (ActiveMove)
        {
            MoveTowardsTarget();
        }
    }

    public void Activate_Move()
    {
        ActiveMove = true;
        time_start = Time.time;
    }

    public void Change_ToSlowerSpeed(bool IsPartOfFinalizeSequence)
    {
        duration = IsPartOfFinalizeSequence ? 4.0f : 1.0f;

        Debug.Log(duration);
    }

    void MoveTowardsTarget()
    {
        if (bLinearMovement)
        {
            //get time change
            float timechange = Time.time - time_start;

            //get precentage
            float percentage_complete = timechange / duration;

            Vector3 NewPosition = Vector3.Lerp(StartPosition, TargetPosition, percentage_complete);

            //set new start position
            gameObject.transform.position = NewPosition;

            if (percentage_complete >= 1.0f)
            {
                ActiveMove = false;
            }
        }
        else
        {
            //get current
            Vector3 CurrentPosition = gameObject.transform.position;

            //if not close enough
            if (Vector3.Distance(CurrentPosition, TargetPosition) > lerp_snap_distance)
            {
                //keep going
                gameObject.transform.position = Vector3.Lerp(CurrentPosition, TargetPosition, lerp_speed * Time.deltaTime);
            }
            //close enough, turn off
            else
            {
                //set position to target
                gameObject.transform.position = TargetPosition;

                //turn off movement
                ActiveMove = false;
            }
        }
    }

    public void Change_TargetPosition(Vector3 position)
    {
        TargetPosition = position;
    }

    public void InstantChange_ToTargetPosition()
    {
        //set position to target
        gameObject.transform.position = TargetPosition;
    }

    public void Swap_TargetAndStartPositions()
    {
        Vector3 temp = StartPosition;
        StartPosition = TargetPosition;
        TargetPosition = temp;
    }
    public void Move_In()
    {
        TargetPosition = InsidePosition;
        StartPosition = OutsidePosition;

        InstantChange_ToTargetPosition();
    }

    public void Move_Out()
    {
        TargetPosition = OutsidePosition;
        StartPosition = InsidePosition;

        Activate_Move();
    }
}
