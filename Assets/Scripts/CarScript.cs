using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour {


    float acceleration;
    float speed;

    float minPos, maxPos;

	// Use this for initialization
	void Start ()
    {
        speed = 0;
        acceleration = 0;
        minPos = -3.0f;
        maxPos = 4.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdatePosition();
        speed += acceleration;

        Debug.Log("speed " + speed);
        Debug.Log("acceleration " + acceleration);

        if (Input.GetKeyDown(KeyCode.A))
        {
            SetAcceleration(-0.003f);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            SetAcceleration(0.003f);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            SetAcceleration(0.0f);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            SetAcceleration(0.0f);
        }

    }

    public void SetAcceleration(float accel)
    {
        acceleration = accel;
    }

    private void UpdatePosition()
    {
        if(transform.position.x < minPos && speed < 0)
        {
            //acceleration = 0;
            speed = 0;
            return;
        }
        else if(transform.position.x > maxPos && speed > 0)
        {
            speed = 0;
            //acceleration = 0;
            return;
        }
        Vector2 newPos = new Vector2(transform.position.x + speed, transform.position.y);
        transform.position = newPos;
    }
}
