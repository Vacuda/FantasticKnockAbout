using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    MoverBase mover;


    // Start is called before the first frame update
    void Start()
    {
        mover = gameObject.GetComponent<MoverBase>();
    }

    public void BringIn_GameOverMenu()
    {
        mover.Set_In();
    }

    public void BringOut_GameOverMenu()
    {
        mover.Move_Out();
    }
}
