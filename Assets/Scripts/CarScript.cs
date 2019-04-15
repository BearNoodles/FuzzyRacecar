using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour {


    float acceleration;
    float velocity;
    float maxVelocity;

    float steeringScale;

    float minPos, maxPos;

	// Use this for initialization
	void Awake ()
    {
        velocity = 0;
        acceleration = 0;
        steeringScale = 0.003f;
        minPos = -1.5f;
        maxPos = 1.5f;

        maxVelocity = 0.06f;

	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdatePosition();
        velocity += acceleration;

        //Debug.Log("velocity " + velocity);
        //Debug.Log("acceleration " + acceleration);

        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    SetSteering(-steeringScale);
        //}
        //else if (Input.GetKeyDown(KeyCode.D))
        //{
        //    SetSteering(steeringScale);
        //}
        //else if (Input.GetKeyUp(KeyCode.A))
        //{
        //    SetSteering(0.0f);
        //}
        //else if (Input.GetKeyUp(KeyCode.D))
        //{
        //    SetSteering(0.0f);
        //}

    }

    

    private void UpdatePosition()
    {
        if(transform.position.x < minPos && velocity < 0)
        {
            //acceleration = 0;
            velocity = 0;
            return;
        }
        else if(transform.position.x > maxPos && velocity > 0)
        {
            velocity = 0;
            //acceleration = 0;
            return;
        }

        if(velocity > maxVelocity)
        {
            velocity = maxVelocity;
        }

        else if (velocity < -maxVelocity)
        {
            velocity = -maxVelocity;
        }
        Vector2 newPos = new Vector2(transform.position.x + velocity, transform.position.y);
        transform.position = newPos;
    }

    public float GetPositionX()
    {
        return transform.position.x;
    }

    public float GetVelocityX()
    {
        return velocity;
    }

    public float GetMaxVelocity()
    {
        return maxVelocity;
    }

    public float GetSteeringScale()
    {
        return steeringScale;
    }
    public void SetSteering(float accel)
    {
        acceleration = accel;
    }
}
