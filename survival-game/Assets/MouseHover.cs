using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHover : MonoBehaviour
{
    Renderer r;
    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Renderer>();
        r.material.color = new Color(0.9647059f, 0.8196079f, 0f, 0.75f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseEnter()
    {
        r.material.color = new Color(0.9647059f, 0.8196079f, 0f, 1f);
    }

    void OnMouseExit()
    {
        r.material.color = new Color(0.9647059f, 0.8196079f, 0f, 0.75f);
    }
}
