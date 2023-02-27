using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    MoverBase mover;


    // Start is called before the first frame update
    void Start()
    {
        mover = gameObject.GetComponent<MoverBase>();
        mover.InstantChange_ToTargetPosition();
    }

    public void BringIn_PauseMenu()
    {
        mover.Move_In();
    }

    public void BringOut_PauseMenu()
    {
        mover.Move_Out();
    }


}
