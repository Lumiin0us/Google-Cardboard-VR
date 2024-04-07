using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class change_colors : MonoBehaviour
{
    public void OnPointerEnter()
    {
        Blue();
    }
    public void OnPointerExit()
    {
        Red();
    }
    public void OnPointerClick()
    {
        Yellow();
    }
    public void Red()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }
    public void Blue()
    {
        GetComponent<Renderer>().material.color = Color.blue;
    }
    public void Yellow()
    {
        GetComponent<Renderer>().material.color = Color.yellow;
    }
}
