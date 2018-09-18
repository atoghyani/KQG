using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ScreenWrapBehaviour : MonoBehaviour
{
    // Whether to use advancedWrapping or not
 

    Renderer renderer;

    bool isWrappingX = false;

    public float screenWidth;
    public float screenHeight;



    void Start()
    {
        var cam = Camera.main;

        var viewportPosition = cam.WorldToViewportPoint(transform.position);
        renderer = GetComponent<Renderer>();
        float screenWidth = Screen.width / 2;
        float screenHeight = Screen.height / 2;
    }

    // Update is called once per frame
    void Update()
    {
     

            ScreenWrap();
        
    }

   

    void ScreenWrap()
    {
        if (transform.position.x < -Screen.width)
        {
            transform.position = new Vector2(screenWidth, transform.position.y);
        }

        if (transform.position.x < screenWidth)
        {
            transform.position = new Vector2(-screenWidth, transform.position.y);
        }
    }



}