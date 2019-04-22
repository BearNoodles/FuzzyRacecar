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

    //to be set in the editor to differentiate between the 2 cars
    public enum Cartype { fuzzy, ruleBased };
    public Cartype cType;

    bool isPaused;

	// Use this for initialization
	void Awake ()
    {
        //intialize values
        velocity = 0;
        acceleration = 0.0f;
        steeringScale = 3.0f;
        minPos = -1.5f;
        maxPos = 1.5f;

        maxVelocity = 3.0f;

        //get the displayed position and velocity fields and texts depending on which car this is
        //fuzzy car
        if (cType == Cartype.fuzzy)
        {
            inputPosition = GameObject.FindGameObjectWithTag("FuzzyCarPositionInputText").GetComponent<Text>();
            inputVelocity = GameObject.FindGameObjectWithTag("FuzzyCarVelocityInputText").GetComponent<Text>();

            positionField = GameObject.FindGameObjectWithTag("FuzzyCarPositionInputField").GetComponent<InputField>();
            velocityField = GameObject.FindGameObjectWithTag("FuzzyVelocityInputField").GetComponent<InputField>();
        }
        
        //rule based car
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
        //if the car is paused dont update
        if (isPaused)
        {
            return;
        }

        //calculate the new position based on the previous frames velocity
        UpdatePosition();

        //adjust the velocity based on the current acceleration multiplied by delta time
        velocity += acceleration * Time.deltaTime;
        
        //update the text in the velocity and position fields only if the are not currently focussed i.e have been clicked
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
        //stop the care if it tries to move off of the track in either drection
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

        //clamp the cars velocity to the max and min values
        if(velocity > maxVelocity)
        {
            velocity = maxVelocity;
        }
        else if (velocity < -maxVelocity)
        {
            velocity = -maxVelocity;
        }

        //add the velocity multiplied by delta time to the cars tranforms x position
        Vector2 newPos = new Vector2(transform.position.x + (velocity * Time.deltaTime), transform.position.y);

        //set the new position
        transform.position = newPos;
    }

    //public function to return the x position of this car
    public float GetPositionX()
    {
        return transform.position.x;
    }

    //public function to return the x velocity of this car
    public float GetVelocityX()
    {
        return velocity;
    }

    //public function to return the max x velocity of this car
    public float GetMaxVelocity()
    {
        return maxVelocity;
    }

    //public function to return the steering scale of this car
    public float GetSteeringScale()
    {
        return steeringScale;
    }

    //public function to set the steering scale of this car
    public void SetSteering(float accel)
    {
        acceleration = accel;
    }

    //public function which pauses the movement of this car
    public void Pause()
    {
        isPaused = !isPaused;
    }

    //public function to set the x position of this car based on the position input field
    public void SetPositionXString()
    {
        float pos;

        //stops an error when a field is accidentally clicked and no value is set
        if(inputPosition.text == "")
        {
            pos = 0.0f;
        }
        //changes the text to a floar
        else
        {
            pos = float.Parse(inputPosition.text);
        }

        //stop the car being set to values outside the variable limits
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
        
        //sets the text field to this new value
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
