using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    private bool checkEnter = false;
    private Color currentColor;
    private float timer = 0f;
    public GameObject playerCameraGameObject;

    public void StartRender()
    {
        checkEnter = true;
        currentColor = GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.color = Color.green;
    }
    public void OnPointerEnter()
    {
        StartRender();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(checkEnter)
        {
            timer += Time.deltaTime;
            if(timer >= 2f)
            {
                GetComponent<Renderer>().material.color = Color.Lerp(currentColor, Color.green, Time.deltaTime/2f);
                Vector3 teleportPosition = new Vector3(transform.position.x, playerCameraGameObject.transform.position.y, transform.position.z);
                playerCameraGameObject.transform.position = teleportPosition;
                timer = 0;
                checkEnter = false;
                GetComponent<Renderer>().material.color = currentColor;
            }
        }
    }
}
