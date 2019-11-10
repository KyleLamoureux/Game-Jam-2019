using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credit : MonoBehaviour
{
    public bool isTitle;
    public bool isQuit;

    void Start()
    {
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicClass>().PlayMusic();
    }

    void OnMouseUp()
    {
        if (isTitle)
        {
            Application.LoadLevel(0);
        }
        if (isQuit)
        {
            Application.Quit();
        }
    }
}
