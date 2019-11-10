using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public bool isStart;
    public bool isQuit;
    public bool isCredit;
    void OnMouseUp()
    {
        if (isStart)
        {
            Application.LoadLevel(1);
        }
        if(isCredit)
        {
            Application.LoadLevel(2);
        }
        if (isQuit)
        {
            Application.Quit();
        }
    }
}
