using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineScript : MonoBehaviour {

    float moveSpeed;

    float minX, maxX;
    Text inputPosition;
    InputField positionField;


    // Use this for initialization
    void Start ()
    {
        //initialize values
        moveSpeed = 0.03f;
        minX = -1.5f;
        maxX = 1.5f;

        //get the text fields pbjects for the line from the scene
        positionField = GameObject.FindGameObjectWithTag("LinePositionInputField").GetComponent<InputField>();
        inputPosition = GameObject.FindGameObjectWithTag("LinePositionInputText").GetComponent<Text>();
    }
	//
	// Update is called once per frame
	void Update ()
    {
        //only updates the input field text if it is not currently clicked
        if(positionField.isFocused == false)
        {
            positionField.text = transform.position.x.ToString("F3");
        }
        
        //moves the lines x position with the left and right arrows
        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > minX)
        {
            Vector2 newPos = new Vector2(transform.position.x - moveSpeed, transform.position.y);
            transform.position = newPos;
        }
        
        else if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < maxX)
        {
            Vector2 newPos = new Vector2(transform.position.x + moveSpeed, transform.position.y);
            transform.position = newPos;
        }
    }

    //returns the lines x position
    public float GetPositionX()
    {
        return transform.position.x;
    }

    //sets the lines x position from the input field 
    public void SetPositionXString()
    {
        float pos;
        if(inputPosition.text == "")
        {
            pos = 0.0f;
        }
        else
        {
            pos = float.Parse(inputPosition.text);
        }


        Vector2 newPos = new Vector2(pos, transform.position.y);
        transform.position = newPos;
    }
}
//