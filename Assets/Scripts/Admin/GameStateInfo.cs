using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateInfo : MonoBehaviour
{
    public int lives_remaining = 5;
    public int rank = 0;

    public LevelInfo level;

    public bool IsFirstLoad = true;




    public static GameStateInfo Instance;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }



    //    public int Get_Lives_Remaining()
    //    {
    //        return lives_remaining;
    //    }

    //    public int Get_Rank()
    //    {
    //        return rank;
    //    }

    //    public void Deduct_Life()
    //    {
    //        lives_remaining--;
    //    }
}
