using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineScript : MonoBehaviour
{
    LineRenderer line_rend;
    Rigidbody2D r_body;
    Vector3 parent_position;
    Vector3 normalized_direction;
    Vector3 blank_vector;

    public float PercentHead = 0.4f;
    public Vector3 ArrowOrigin;
    public Vector3 ArrowTarget;


    void Start()
    {
        line_rend = gameObject.GetComponent<LineRenderer>();
        line_rend.positionCount = 2;
        r_body = gameObject.transform.parent.GetComponent<Rigidbody2D>();
        blank_vector = new Vector3();
        this.enabled = false;
    }

    void Update()
    {
        DrawLine();
        testc();
    }

    void DrawLine()
    {
        //get parent position
        parent_position = gameObject.transform.parent.transform.position;

        //get normalized vector
        normalized_direction = r_body.velocity.normalized;

        //set start of line
        line_rend.SetPosition(0, parent_position);

        //set end of line
        line_rend.SetPosition(1, parent_position + normalized_direction * 3);
    }

    public void Trigger_LineDrawing(bool activate)
    {
        if (activate)
        {
            this.enabled = true;
        }
        else
        {
            line_rend.SetPosition(0,blank_vector);
            line_rend.SetPosition(1, blank_vector);
            this.enabled = false;
        }


    }

    public void testc()
    {
        //get parent position
        parent_position = gameObject.transform.parent.transform.position;

        //get normalized vector
        normalized_direction = r_body.velocity.normalized;

        float AdaptiveSize = (float)(PercentHead / Vector3.Distance(parent_position, normalized_direction));


        line_rend.widthCurve = new AnimationCurve(
            new Keyframe(0, 0.4f)
            , new Keyframe(0.999f - AdaptiveSize, 0.4f)  // neck of arrow
            , new Keyframe(1 - AdaptiveSize, 1f)  // max width of arrow head
            , new Keyframe(1, 0f));  // tip of arrow
        line_rend.SetPositions(new Vector3[] {
               ArrowOrigin
               , Vector3.Lerp(parent_position, normalized_direction, 0.999f - AdaptiveSize)
               , Vector3.Lerp(parent_position, normalized_direction, 1 - AdaptiveSize)
               , normalized_direction
        });

        //set start of line
        line_rend.SetPosition(0, parent_position);

        //set end of line
        line_rend.SetPosition(1, parent_position + normalized_direction * 3);
    }
}
