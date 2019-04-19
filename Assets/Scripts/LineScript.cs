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

        //if (Input.GetKey(KeyCode.LeftArrow) && GetComponent<LineRenderer>().GetPosition(0).x > minX)
        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > minX)
        {
            //Vector2 newPos0 = GetComponent<LineRenderer>().GetPosition(0);
            //Vector2 newPos1 = GetComponent<LineRenderer>().GetPosition(1);
            //newPos0 = new Vector2(newPos0.x - moveSpeed, newPos0.y);
            //newPos1 = new Vector2(newPos1.x - moveSpeed, newPos1.y);
            //GetComponent<LineRenderer>().SetPosition(0, newPos0);
            //GetComponent<LineRenderer>().SetPosition(1, newPos1);


            Vector2 newPos = new Vector2(transform.position.x - moveSpeed, transform.position.y);
            transform.position = newPos;
        }

        //else if (Input.GetKey(KeyCode.RightArrow) && GetComponent<LineRenderer>().GetPosition(0).x < maxX)
        else if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < maxX)
        {
            //Vector2 newPos0 = GetComponent<LineRenderer>().GetPosition(0);
            //Vector2 newPos1 = GetComponent<LineRenderer>().GetPosition(1);
            //newPos0 = new Vector2(newPos0.x + moveSpeed, newPos0.y);
            //newPos1 = new Vector2(newPos1.x + moveSpeed, newPos1.y);
            //GetComponent<LineRenderer>().SetPosition(0, newPos0);
            //GetComponent<LineRenderer>().SetPosition(1, newPos1);

            Vector2 newPos = new Vector2(transform.position.x + moveSpeed, transform.position.y);
            transform.position = newPos;
        }
    }

    public float GetPositionX()
    {
        //return GetComponent<LineRenderer>().GetPosition(0).x;
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

        //Vector3 newPos0 = GetComponent<LineRenderer>().GetPosition(0);
        //Vector3 newPos1 = GetComponent<LineRenderer>().GetPosition(1);
        //newPos0 = new Vector3(pos, newPos0.y, -1);
        //newPos1 = new Vector3(pos, newPos1.y, -1);
        //GetComponent<LineRenderer>().SetPosition(0, newPos0);
        //GetComponent<LineRenderer>().SetPosition(1, newPos1);


        Vector2 newPos = new Vector2(pos, transform.position.y);
        transform.position = newPos;
    }
}
//