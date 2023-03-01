using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleLevel : MonoBehaviour
{
    CanvasGroup BlackScreen_CanvasGroup;
    CanvasGroup LogoScreen_CanvasGroup;
    public GameObject TitleScreen;
    public GameObject BlackScreen;
    public GameObject LogoScreen;
    private FadeInOut BlackScreen_FadeInOut;
    private FadeInOut LogoScreen_FadeInOut;

    public bool bSkipLogoOpening = true;
    public bool bSkipTitleScreen = true;

    public SoundLab s_lab;

    public GameObject ball;
    private BallMovement ball_movement;
    Vector3 MainPosition = new Vector3(0.0f, 0.0f, 0.0f);


    private void Awake()
    {
        LogoScreen.transform.position = MainPosition;
        BlackScreen.transform.position = MainPosition;
    }

    void Start()
    {
        //late references
        ball_movement = ball.GetComponent<BallMovement>();
        BlackScreen_CanvasGroup = BlackScreen.GetComponent<CanvasGroup>();
        LogoScreen_CanvasGroup = LogoScreen.GetComponent<CanvasGroup>();
        BlackScreen_FadeInOut = BlackScreen.GetComponent<FadeInOut>();
        LogoScreen_FadeInOut = LogoScreen.GetComponent<FadeInOut>();


        if (!bSkipLogoOpening)
        {
            //start level opening
            StartCoroutine(Sequence_LogoOpening());
        }
        else if (!bSkipTitleScreen)
        {
            //title start
            StartCoroutine(Sequence_TitleStart());
        }
        else
        {
            //game start
            Sequence_GameStart();
        }

        //start ball
        ball_movement.Stop_Ball(false);
    }

    IEnumerator Sequence_LogoOpening()
    {
        //fade in logo
        yield return StartCoroutine(LogoScreen_FadeInOut.Start_FadeInAndOut(1.0f));

        //fade out blackscreen
        yield return StartCoroutine(BlackScreen_FadeInOut.Start_Fade(false));

        StartCoroutine(Sequence_TitleStart());
    }

    IEnumerator Sequence_TitleStart()
    {
        //remove opening
        BlackScreen_CanvasGroup.alpha = 0.0f;
        LogoScreen_CanvasGroup.alpha = 0.0f;

        yield return new WaitForSeconds(0.2f);

        //play start sound
        s_lab.PlaySound_GeneralBlip();

        //set titlescreen in
        TitleScreen.transform.position = MainPosition;

        yield return new WaitForSeconds(5.0f);

        Sequence_GameStart();
    }

    void Sequence_GameStart()
    {
        SceneManager.LoadScene("GameLevel", LoadSceneMode.Single);
    }
}
