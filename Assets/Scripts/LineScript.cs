using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineScript : MonoBehaviour {

    float moveSpeed;

    float minX, maxX;
    Text inputPosition;


	// Use this for initialization
	void Start ()
    {
        moveSpeed = 0.03f;
        minX = -1.5f;
        maxX = 1.5f;

        inputPosition = GameObject.FindGameObjectWithTag("FuzzyLinePositionInputText").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && GetComponent<LineRenderer>().GetPosition(0).x > minX)
        {
            Vector2 newPos0 = GetComponent<LineRenderer>().GetPosition(0);
            Vector2 newPos1 = GetComponent<LineRenderer>().GetPosition(1);
            newPos0 = new Vector2(newPos0.x - moveSpeed, newPos0.y);
            newPos1 = new Vector2(newPos1.x - moveSpeed, newPos1.y);
            GetComponent<LineRenderer>().SetPosition(0, newPos0);
            GetComponent<LineRenderer>().SetPosition(1, newPos1);
        }

        else if (Input.GetKey(KeyCode.RightArrow) && GetComponent<LineRenderer>().GetPosition(0).x < maxX)
        {
            Vector2 newPos0 = GetComponent<LineRenderer>().GetPosition(0);
            Vector2 newPos1 = GetComponent<LineRenderer>().GetPosition(1);
            newPos0 = new Vector2(newPos0.x + moveSpeed, newPos0.y);
            newPos1 = new Vector2(newPos1.x + moveSpeed, newPos1.y);
            GetComponent<LineRenderer>().SetPosition(0, newPos0);
            GetComponent<LineRenderer>().SetPosition(1, newPos1);
        }
    }

    public float GetPositionX()
    {
        return GetComponent<LineRenderer>().GetPosition(0).x;
    }
    public void SetPositionXString()
    {
        float pos = float.Parse(inputPosition.text);

        Vector3 newPos0 = GetComponent<LineRenderer>().GetPosition(0);
        Vector3 newPos1 = GetComponent<LineRenderer>().GetPosition(1);
        newPos0 = new Vector3(pos, newPos0.y, -1);
        newPos1 = new Vector3(pos, newPos1.y, -1);
        GetComponent<LineRenderer>().SetPosition(0, newPos0);
        GetComponent<LineRenderer>().SetPosition(1, newPos1);
    }
}
