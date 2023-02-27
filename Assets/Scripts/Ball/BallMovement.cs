using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    Rigidbody2D r_body;
    float currentSpeed;


    //speeds
    float normalSpeed = 18.0f;
    float slowSpeed = 1.0f;
    
    void Start()
    {
        //get rigidbody
        r_body = gameObject.GetComponent<Rigidbody2D>();

        //find rand x
        int rand_x = Random.Range(-30, 31);

        //find rand y
        int rand_y = Random.Range(5, 31);

        //set initial direction
        Vector2 taco = new Vector2(rand_x, rand_y);

        //add initial force
        r_body.AddForce(taco, ForceMode2D.Impulse);

        //set speed
        currentSpeed = normalSpeed;

        Stop_Ball(true);
    }

    
    void Update()
    {
        //control speed
        r_body.velocity = currentSpeed * (r_body.velocity.normalized);
    }

    public void ChangeSpeed_Slow()
    {
        currentSpeed = slowSpeed;
    }

    public void ChangeSpeed_Normal()
    {
        currentSpeed = normalSpeed;
    }

    public void Stop_Ball(bool stopped)
    {
        if (stopped)
        {
            r_body.simulated = false;
            this.enabled = false;
        }
        else
        {
            r_body.simulated = true;
            this.enabled = true;
        }
    }
}
