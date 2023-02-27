using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameLevel : MonoBehaviour
{

    public GameObject gate;
    public Camera cam;
    public GameObject ball;

    CanvasGroup BlackScreen_CanvasGroup;
    public GameObject BlackScreen;
    public GameObject LogoScreen;
    private FadeInOut BlackScreen_FadeInOut;
    private FadeInOut LogoScreen_FadeInOut;

    public bool bSkipOpening = true;
    public bool bSkipRetro = false;

    public GameObject Player_1_Score;
    private Text texter_score;
    public string text_score;
    public int game_score = 0;

    public GameObject Player_Lives;
    private Text texter_lives_1;
    private Text texter_lives_2;

    public GameObject Player_Rank;
    private Text texter_rank_1;
    private Text texter_rank_2;

    public GameObject Burst_Rank;
    public GameObject Burst_Lives;
    public PauseMenu pause_menu;
    public GameObject game_over_menu;
    
    private LevelBuilder _level_builder;
    private MoverBase gate_mover;
    private MoverBase cam_mover;
    private BallMovement ball_movement;

    public SoundLab s_lab;
    public EndRetro_Volume endretro_volume;

    bool FrozenDeaths = false;

    private void Awake()
    {
        gate_mover = gate.GetComponent<MoverBase>();
        cam_mover = cam.GetComponent<MoverBase>();
        ball_movement = ball.GetComponent<BallMovement>();
        _level_builder = gameObject.GetComponent<LevelBuilder>();
        texter_score = Player_1_Score.GetComponent<Text>();

        texter_lives_1 = Player_Lives.transform.Find("Num-Back").GetComponent<Text>();
        texter_lives_2 = Player_Lives.transform.Find("Num-Front").GetComponent<Text>();

        texter_rank_1 = Player_Rank.transform.Find("Num-Back").GetComponent<Text>();
        texter_rank_2 = Player_Rank.transform.Find("Num-Front").GetComponent<Text>();

    }





    void Start()
    {
        Lives_Update();
        Rank_Update();
        Check_FirstLoad();

        game_over_menu.GetComponent<MoverBase>().InstantChange_ToTargetPosition();

        //set references
        BlackScreen_CanvasGroup = BlackScreen.GetComponent<CanvasGroup>();
        BlackScreen_FadeInOut = BlackScreen.GetComponent<FadeInOut>();
        LogoScreen_FadeInOut = LogoScreen.GetComponent<FadeInOut>();





        if (!bSkipOpening)
        {
            //start level opening
            StartCoroutine(Sequence_LogoOpening());
        }
        else
        {
            StartCoroutine(Sequence_LevelOpening());
        }

        StartCoroutine(StartUp());
    }

    IEnumerator StartUp()
    {
        //mini delay
        yield return new WaitForSeconds(0.01f);

        //initialize level
        LevelInfo info;

        //find level info
        if(GameStateInfo.Instance.level != null)
        {
            //get previous level
            info = GameStateInfo.Instance.level;
        }
        //new level needed
        else
        {
            //get rank
            int rank = GameStateInfo.Instance.rank;

            //get level
            info = LevelHouse.BuildALevel(rank);
        }

        //build level
        _level_builder.BuiltOut_ThisLevel(info);

        //store level in persistent game object
        GameStateInfo.Instance.level = info;

        //if rank not zero
        if (GameStateInfo.Instance.rank > 0)
        {
            bSkipRetro = true;
        }


        if (bSkipRetro)
        {
            ball.transform.position = new Vector3(-3.0f, -11.0f, 0.0f);
            cam_mover.InstantChange_ToTargetPosition();
            endretro_volume.Destroy_ThisVolume();
        }
        else
        {
            //reset gate
            gate_mover.InstantChange_ToTargetPosition();
            gate_mover.Swap_TargetAndStartPositions();
        }
    }

    IEnumerator Sequence_LogoOpening()
    {
        //set blackscreen full
        BlackScreen_CanvasGroup.alpha = 1.0f;

        //fade in logo
        yield return StartCoroutine(LogoScreen_FadeInOut.Start_FadeInAndOut(1.0f));

        //fade out blackscreen
        yield return StartCoroutine(BlackScreen_FadeInOut.Start_Fade(false));

        StartCoroutine(Sequence_LevelOpening());
    }

    IEnumerator Sequence_LevelOpening()
    {
        yield return new WaitForSeconds(1.0f);

        //start ball
        ball_movement.Stop_Ball(false);

        yield break;
    }




    public void Entered_EndRetro_Volume()
    {
        cam_mover.Activate_Move();
    }

    IEnumerator SlowBall()
    {
        //slow ball
        ball_movement.ChangeSpeed_Slow();

        yield return new WaitForSeconds(0.7f);

        //return to normal speed
        ball_movement.ChangeSpeed_Normal();



        //end
        yield break;
    }

    public void Close_Gate()
    {
        StartCoroutine(SlowBall());

        gate_mover.Activate_Move();
    }

    public void Trigger_OnDeath()
    {
        StartCoroutine(OnDeath());
    }

    public void Trigger_SuccessfulExit()
    {
        StartCoroutine(SuccessfulExit());
    }

    public void PauseGame()
    {
        //bring up menu screen
        pause_menu.BringIn_PauseMenu();

        s_lab.PlaySound_Pause();

        Time.timeScale = 0;
    }

    public void UnPauseGame()
    {
        //bring out menu screen
        pause_menu.BringOut_PauseMenu();

        s_lab.PlaySound_Pause();

        Time.timeScale = 1;
    }

    public void Change_Score(int num)
    {
        game_score += num;
        Score_Update();
    }

    public void Score_Update()
    {
        string updated_string;


        if(game_score < 10)
        {
            updated_string = "00" + game_score.ToString();
        }
        else if(game_score < 100)
        {
            updated_string = "0" + game_score.ToString();
        }
        else if(game_score < 1000)
        {
            updated_string = game_score.ToString();
        }
        else
        {
            updated_string = "999";
        }



        texter_score.text = updated_string;

    }

    public void Lives_Update()
    {
        int lives = GameStateInfo.Instance.lives_remaining;


        texter_lives_1.text = "0" + lives;
        texter_lives_2.text = "0" + lives;

    }

    public void Rank_Update()
    {
        //get rank
        int rank = GameStateInfo.Instance.rank;

        //UI check
        if(rank > 99)
        {
            rank = 99;
        }

        if (rank <= 9)
        {
            texter_rank_1.text = "0" + rank;
            texter_rank_2.text = "0" + rank;
        }
        else
        {
            texter_rank_1.text = rank.ToString();
            texter_rank_2.text = rank.ToString();
        }
    }

    IEnumerator OnDeath()
    {
        if (!FrozenDeaths)
        {
            FrozenDeaths = true;
            GameStateInfo.Instance.lives_remaining--;
        }


        Lives_Update();

        Burst_Lives.GetComponent<Animator>().Play("state_Bursting");

        s_lab.PlaySound_Death();
        //bring up menu
        //stop physics

        //game over check
        if (GameStateInfo.Instance.lives_remaining <= 0)
        {
            GameOver();
        }

        yield return new WaitForSeconds(3.0f);


        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        yield break;
    }

    IEnumerator SuccessfulExit()
    {
        GameStateInfo.Instance.rank++;
        Rank_Update();
        Burst_Rank.GetComponent<Animator>().Play("state_Bursting");

        GameStateInfo.Instance.level = null;
        ball_movement.Stop_Ball(true);

        s_lab.PlaySound_Fanfare();

        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    void GameOver()
    {
        GameStateInfo.Instance.rank = 0;
        GameStateInfo.Instance.lives_remaining = 5;
        GameStateInfo.Instance.level = null;
        game_over_menu.GetComponent<MoverBase>().Move_In();
        s_lab.PlaySound_GameOver();

    }

    void Check_FirstLoad()
    {
        //if first load
        if (GameStateInfo.Instance.IsFirstLoad == true)
        {
            GameStateInfo.Instance.IsFirstLoad = false;
        }
        //if NOT first load
        else
        {
            bSkipOpening = true;
            //bSkipRetro = true;
        }
    }

}
