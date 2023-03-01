using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    CanvasGroup Group;

    public float duration = 1.0f;

    private float time_start;
    private float start_alpha;
    private float finish_alpha;

    private float timechange;
    private float percentage_complete;
    private float new_alpha;

    private void Awake()
    {
        Group = gameObject.GetComponent<CanvasGroup>();
    }

    //void Start()
    //{
    //    Group = gameObject.GetComponent<CanvasGroup>();
    //}

    public IEnumerator Start_Fade(bool fadeIntoImage)
    {
        //determine direction
        if (fadeIntoImage)
        {
            start_alpha = 0.0f;
            finish_alpha = 1.0f;
        }
        else
        {
            start_alpha = 1.0f;
            finish_alpha = 0.0f;
        }

        //get start time
        time_start = Time.time;

        yield return StartCoroutine(Perform_Fade());
    }

    IEnumerator Perform_Fade()
    {
        //while performing fade
        while (percentage_complete < 1.0f)
        {
            //get time change
            timechange = Time.time - time_start;

            //get precentage
            percentage_complete = timechange / duration;

            //get new alpha
            new_alpha = Mathf.Lerp(start_alpha, finish_alpha, percentage_complete);

            //set new alpha
            Group.alpha = new_alpha;

            yield return null; // this tells the routine to go to next tick
        }

        //reset percentage_complete
        percentage_complete = 0.0f;

    }

    public IEnumerator Start_FadeInAndOut(float wait_time)
    {
        yield return StartCoroutine(Start_Fade(true));

        yield return new WaitForSeconds(wait_time);

        yield return StartCoroutine(Start_Fade(false));
    }
}
