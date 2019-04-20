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
        moveSpeed = 0.03f;
        minX = -1.5f;
        maxX = 1.5f;

        positionField = GameObject.FindGameObjectWithTag("LinePositionInputField").GetComponent<InputField>();
        inputPosition = GameObject.FindGameObjectWithTag("LinePositionInputText").GetComponent<Text>();
    }
	//
	// Update is called once per frame
	void Update ()
    {
        if(positionField.isFocused == false)
        {
            positionField.text = transform.position.x.ToString("F3");
        }
        
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

    public float GetPositionX()
    {
        return transform.position.x;
    }
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