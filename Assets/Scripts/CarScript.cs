using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarScript : MonoBehaviour {


    float acceleration;
    float velocity;
    float maxVelocity;

    float steeringScale;

    float minPos, maxPos;

    InputField positionField;
    InputField velocityField;
    Text inputPosition;
    Text inputVelocity;

    public enum Cartype { fuzzy, ruleBased };
    public Cartype cType;

    bool isPaused;

	// Use this for initialization
	void Awake ()
    {
        velocity = 0;
        acceleration = 0.0f;
        steeringScale = 3.0f;
        minPos = -1.5f;
        maxPos = 1.5f;

        maxVelocity = 3.0f;

        if (cType == Cartype.fuzzy)
        {
            inputPosition = GameObject.FindGameObjectWithTag("FuzzyCarPositionInputText").GetComponent<Text>();
            inputVelocity = GameObject.FindGameObjectWithTag("FuzzyCarVelocityInputText").GetComponent<Text>();

            positionField = GameObject.FindGameObjectWithTag("FuzzyCarPositionInputField").GetComponent<InputField>();
            velocityField = GameObject.FindGameObjectWithTag("FuzzyVelocityInputField").GetComponent<InputField>();
        }

        else
        {
            inputPosition = GameObject.FindGameObjectWithTag("RuleBasedCarPositionInputText").GetComponent<Text>();
            inputVelocity = GameObject.FindGameObjectWithTag("RuleBasedCarVelocityInputText").GetComponent<Text>();

            positionField = GameObject.FindGameObjectWithTag("RuleBasedCarPositionInputField").GetComponent<InputField>();
            velocityField = GameObject.FindGameObjectWithTag("RuleBasedVelocityInputField").GetComponent<InputField>();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isPaused)
        {
            return;
        }
        UpdatePosition();
        velocity += acceleration * Time.deltaTime;
        
        if(positionField.isFocused == false)
        {
            positionField.text = transform.position.x.ToString("F3");
        }
        if(velocityField.isFocused == false)
        {
            velocityField.text = velocity.ToString("F3");
        }

    }

    

    private void UpdatePosition()
    {

        if(transform.position.x < minPos && velocity < 0)
        {
            velocity = 0;
            return;
        }
        else if(transform.position.x > maxPos && velocity > 0)
        {
            velocity = 0;
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
        Vector2 newPos = new Vector2(transform.position.x + (velocity * Time.deltaTime), transform.position.y);
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
        float pos;
        if(inputPosition.text == "")
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
        else if(pos > 1.499f)
        {
            pos = 1.499f;
        }

        Vector2 newPos = new Vector2(pos, transform.position.y);
        transform.position = newPos;
        
        positionField.text = transform.position.x.ToString("F3");
    }
    public void SetVelocityXString()
    {
        float vel;
        if (inputVelocity.text == "")
        {
            vel = 0.0f;
        }
        else
        {
            vel = float.Parse(inputVelocity.text);
        }

        if (vel <= -maxVelocity)
        {
            vel = -maxVelocity + 0.001f;
        }
        else if (vel >= maxVelocity)
        {
            vel = maxVelocity - 0.001f;
        }

        if (vel > 0.00001f && vel < 0.00001f)
        {
            vel = 0;
        }
        velocity = vel;
        //
        velocityField.text = velocity.ToString("F3");
    }
}
