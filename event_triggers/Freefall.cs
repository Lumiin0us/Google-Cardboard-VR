using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Freefall : MonoBehaviour
{
    public void OnPointerEnter()
    {
        Drop();
    }

    public void Drop()
    {
        GetComponent<Rigidbody>().useGravity = true;
    }
}
