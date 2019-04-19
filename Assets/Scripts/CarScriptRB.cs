using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarScriptRB : MonoBehaviour
{


    float acceleration;
    float velocity;
    float maxVelocity;

    float steeringScale;

    float minPos, maxPos;

    Text inputPosition;
    Text inputVelocity;

    public enum Cartype { fuzzy, ruleBased};

    Cartype cType;

    bool isPaused;

    // Use this for initialization
    void Awake()
    {
        velocity = 0;
        acceleration = 0.0f;
        steeringScale = 0.003f;
        minPos = -1.5f;
        maxPos = 1.5f;

        maxVelocity = 0.03f;

        if(cType == Cartype.fuzzy)
        {
            inputPosition = GameObject.FindGameObjectWithTag("FuzzyCarPositionInputText").GetComponent<Text>();
            inputVelocity = GameObject.FindGameObjectWithTag("FuzzyCarVelocityInputText").GetComponent<Text>();
        }

        else
        {
            inputPosition = GameObject.FindGameObjectWithTag("RuleBasedCarPositionInputText").GetComponent<Text>();
            inputVelocity = GameObject.FindGameObjectWithTag("BasedCarVelocityInputText").GetComponent<Text>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        inputPosition.text = transform.position.x.ToString();
        if (isPaused)
        {
            return;
        }
        UpdatePosition();
        velocity += acceleration;

        //Debug.Log("velocity " + velocity);
        //Debug.Log("acceleration " + acceleration * 100000);

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

        if (transform.position.x < minPos && velocity < 0)
        {
            //acceleration = 0;
            velocity = 0;
            return;
        }
        else if (transform.position.x > maxPos && velocity > 0)
        {
            //acceleration = 0;
            velocity = 0;
            return;
        }

        if (velocity > maxVelocity)
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
    public void Pause()
    {
        isPaused = !isPaused;
    }

    public void SetPositionXString()
    {
        Debug.Log("H");
        float pos;
        if (inputPosition.text == "")
        {
            pos = 0.0f;
        }
        else
        {
            pos = float.Parse(inputPosition.text);
        }

        if (pos < -1.499f)
        {
            pos = -1.499f;
        }
        else if (pos > 1.499f)
        {
            pos = 1.499f;
        }

        Vector2 newPos = new Vector2(pos, transform.position.y);
        transform.position = newPos;
    }
    public void SetVelocityXString()
    {
        float vel;
        if (inputPosition.text == "")
        {
            vel = 0.0f;
        }
        else
        {
            vel = float.Parse(inputPosition.text);
        }
        velocity = vel;
    }
}
