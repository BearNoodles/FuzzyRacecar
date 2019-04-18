using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleBased : MonoBehaviour
{

    CarScript car;
    LineScript line;
    float linePosX;
    float carPosX;
    float velocity;
    float steeringValue;
    float maxCarVel;

    float FastLeftVel, LeftVel, RightVel, FastRightVel;
    float FarLeftDist, LeftDist, RightDist, FarRightDist;
    float SteerHardLeft, SteerLeft, SteerRight, SteerHardRight;
    float dist;
    float result;


    // Use this for initialization
    void Start()
    {
        car = GetComponent<CarScript>();
        line = GameObject.FindGameObjectWithTag("Line").GetComponent<LineScript>();

        maxCarVel = car.GetMaxVelocity();
        steeringValue = car.GetSteeringScale();

        FarLeftDist = -1.5f;
        LeftDist = -0.1f;
        RightDist = 0.1f;
        FarRightDist = 1.5f;

        FastLeftVel = -1.0f * maxCarVel;
        LeftVel = -0.05f * maxCarVel;
        RightVel = 0.05f * maxCarVel;
        FastRightVel = 1.0f * maxCarVel;

        SteerHardLeft = -steeringValue;
        SteerLeft = -0.5f * steeringValue;
        SteerRight = 0.5f * steeringValue;
        SteerHardRight = steeringValue;
    }

    // Update is called once per frame
    void Update()
    {
        linePosX = line.GetPositionX();
        carPosX = car.GetPositionX();
        velocity = car.GetVelocityX();
        dist = carPosX - linePosX;

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
                result = SteerHardRight;
            }
            else if (velocity > FastRightVel)
            {
                result = 0;
            }
            else if (velocity > RightVel)
            {
                result = 0;
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
                result = 0;
            }
            else if (velocity < LeftVel)
            {
                result = 0;
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
        

        //Debug.Log(result);
        //Debug.Log("linex" + linePosX);
        //Debug.Log("carx" + carPosX);
        //Debug.Log("dist " + dist);
        //Debug.Log("vel " + velocity);
        //Debug.Log("scale " + steeringValue);
        //Debug.Log("maxvel " + maxCarVel);
        

        //Debug.Log("result " + result);

        car.SetSteering(result);
        
    }
}