using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuleBased : MonoBehaviour
{
    //input fields for clickable text
    GameObject carPositionInputField;
    GameObject linePositionInputField;
    GameObject velocityInputField;

    Text steeringText;

    CarScript car;
    LineScript line;
    float linePosX;
    float carPosX;
    float velocity;
    float steeringValue;
    float maxCarVel;

    //float variable to use as rules
    float FastLeftVel, LeftVel, RightVel, FastRightVel;
    float FarLeftDist, LeftDist, RightDist, FarRightDist;
    float SteerHardLeft, SteerLeft, SteerRight, SteerHardRight;

    float dist;
    float result;


    // Use this for initialization
    void Start()
    {
        //get the car script component from the car
        car = GetComponent<CarScript>();
        
        //get the line script component from the line
        line = GameObject.FindGameObjectWithTag("Line").GetComponent<LineScript>();

        //get max velocity from the car script
        maxCarVel = car.GetMaxVelocity();

        //get the steering scale(acceleration) from the car script
        steeringValue = car.GetSteeringScale();

        //find the input field and text objects that we want to change in the scene and set the mas variables
        carPositionInputField = GameObject.FindGameObjectWithTag("RuleBasedCarPositionInputField");
        linePositionInputField = GameObject.FindGameObjectWithTag("RuleBasedLinePositionInputField");
        velocityInputField = GameObject.FindGameObjectWithTag("RuleBasedVelocityInputField");
        steeringText = GameObject.FindGameObjectWithTag("RuleBasedSteeringText").GetComponent<Text>();

        //set specific values for the rules to use
        //distance variable
        FarLeftDist = -1.5f;
        LeftDist = -0.025f;
        RightDist = 0.025f;
        FarRightDist = 1.5f;

        //velocity variable
        FastLeftVel = -0.75f * maxCarVel;
        LeftVel = -0.25f * maxCarVel;
        RightVel = 0.25f * maxCarVel;
        FastRightVel = 0.75f * maxCarVel;

        //steering output variables
        SteerHardLeft = -0.75f * steeringValue;
        SteerLeft = -0.5f * steeringValue;
        SteerRight = 0.5f * steeringValue;
        SteerHardRight = 0.75f * steeringValue;
    }

    // Update is called once per frame
    void Update()
    {
        //update the position of the line and car and the velocity of the car
        linePosX = line.GetPositionX();
        carPosX = car.GetPositionX();
        velocity = car.GetVelocityX();

        //gives distance car is from line
        dist = carPosX - linePosX;

        //set each rules condition for each variable combination
        //FarLeftRules
        if (dist < FarLeftDist)
        {
            if (velocity < FastLeftVel)
            {
                result = SteerHardRight;
            }
            else if (velocity < LeftVel)
            {
                result = SteerHardRight;
            }
            else if (velocity > FastRightVel)
            {
                result = 0;
            }
            else if (velocity > RightVel)
            {
                result = SteerHardRight;
            }
            else //Velocity is Zero
            {
                result = SteerHardRight;
            }
        }

        //LeftRules
        else if (dist < LeftDist)
        {
            if (velocity < FastLeftVel)
            {
                result = SteerHardRight;
            }
            else if (velocity < LeftVel)
            {
                result = SteerRight;
            }
            else if (velocity > FastRightVel)
            {
                result = SteerLeft;
            }
            else if (velocity > RightVel)
            {
                result = SteerLeft;
            }
            else //Velocity is Zero
            {
                result = SteerRight;
            }
        }

        //FarRightRules
        else if (dist > FarRightDist)
        {
            if (velocity < FastLeftVel)
            {
                result = 0;
            }
            else if (velocity < LeftVel)
            {
                result = SteerHardLeft;
            }
            else if (velocity > FastRightVel)
            {
                result = SteerHardLeft;
            }
            else if (velocity > RightVel)
            {
                result = SteerHardLeft;
            }
            else //Velocity is Zero
            {
                result = SteerHardLeft;
            }
        }

        //RightRules
        else if (dist > RightDist)
        {
            if (velocity < FastLeftVel)
            {
                result = SteerRight;
            }
            else if (velocity < LeftVel)
            {
                result = SteerRight;
            }
            else if (velocity > FastRightVel)
            {
                result = SteerHardLeft;
            }
            else if (velocity > RightVel)
            {
                result = SteerLeft;
            }
            else //Velocity is Zero
            {
                result = SteerLeft;
            }
        }

        //CentreRules
        else //Dist is centre
        {
            if (velocity < FastLeftVel)
            {
                result = SteerHardRight;
            }
            else if (velocity < LeftVel)
            {
                result = SteerRight;
            }
            else if (velocity > FastRightVel)
            {
                result = SteerHardLeft;
            }
            else if (velocity > RightVel)
            {
                result = SteerLeft;
            }
            else //Velocity is Zero
            {
                result = 0;
            }
        }
        
        
        //set car steering as the result returned by the rules
        car.SetSteering(result);

        //set steering text to display the result
        steeringText.text = ("Steering: " + result.ToString("F3"));
    }
}