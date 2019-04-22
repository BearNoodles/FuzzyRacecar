using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FLS;
using FLS.Rules;
using UnityEngine.UI;


public class Fuzzy : MonoBehaviour {

    GameObject carPositionInputField;
    GameObject linePositionInputField;
    GameObject velocityInputField;
    Text steeringText;

    CarScript car;
    LineScript line;
    double linePosX;
    double carPosX;
    double velocity;
    double steeringValue;
    double maxCarVel;
    IFuzzyEngine fuzzyEngine;

    bool isPaused = false;

    double result;

    // Use this for initialization
    void Start ()
    {
        car = GetComponent<CarScript>();
        line = GameObject.FindGameObjectWithTag("Line").GetComponent<LineScript>();
        maxCarVel = car.GetMaxVelocity();
        //Debug.Log(maxCarVel);
        steeringValue = car.GetSteeringScale();

        carPositionInputField = GameObject.FindGameObjectWithTag("FuzzyCarPositionInputField");
        linePositionInputField = GameObject.FindGameObjectWithTag("FuzzyLinePositionInputField");
        velocityInputField = GameObject.FindGameObjectWithTag("FuzzyVelocityInputField");
        steeringText = GameObject.FindGameObjectWithTag("FuzzySteeringText").GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        linePosX = line.GetPositionX();
        carPosX = car.GetPositionX();
        velocity = car.GetVelocityX();
        double dist = carPosX - linePosX;


        var carPosToLine = new LinguisticVariable("CarPosition");
        var carFarLeft = carPosToLine.MembershipFunctions.AddTrapezoid("FarLeft", -3.0, -3.0, -1.5, -0.75);
        var carLeft = carPosToLine.MembershipFunctions.AddTriangle("Left", -1.5, -0.75, 0.0);
        var carCentre = carPosToLine.MembershipFunctions.AddTriangle("Centre", -0.75, 0.0, 0.75);
        var carRight = carPosToLine.MembershipFunctions.AddTriangle("Right", 0.0, 0.75, 1.5);
        var carFarRight = carPosToLine.MembershipFunctions.AddTrapezoid("FarRight", 0.75, 1.5, 3.0, 3.0);
        
        
        var carVel = new LinguisticVariable("CarVelocity");
        var velFastLeft = carVel.MembershipFunctions.AddTrapezoid("FastLeft", -maxCarVel, -maxCarVel, -0.5* maxCarVel, -0.25 * maxCarVel);
        var velLeft = carVel.MembershipFunctions.AddTriangle("Left", -0.5 * maxCarVel, -0.25 * maxCarVel, 0.0);
        var velStill = carVel.MembershipFunctions.AddTriangle("Still", -0.25 * maxCarVel, 0.0, 0.25 * maxCarVel);
        var velRight = carVel.MembershipFunctions.AddTriangle("Right", 0.0, 0.25 * maxCarVel, 0.5 * maxCarVel);
        var velFastRight = carVel.MembershipFunctions.AddTrapezoid("FastRight", 0.25 * maxCarVel, 0.5 * maxCarVel, maxCarVel, maxCarVel);
        

        var carSteering = new LinguisticVariable("CarSteering");
        var steerHardLeft = carSteering.MembershipFunctions.AddTriangle("SteerHardLeft", -steeringValue, -steeringValue, -0.5 * steeringValue);
        var steerLeft = carSteering.MembershipFunctions.AddTriangle("SteerLeft", -steeringValue, -0.5 * steeringValue, 0.0);
        var noSteering = carSteering.MembershipFunctions.AddTriangle("NoSteering", -0.5 * steeringValue, 0.0, 0.5 * steeringValue);
        var steerRight = carSteering.MembershipFunctions.AddTriangle("SteerRight", 0.0, 0.5 * steeringValue, steeringValue);
        var steerHardRight = carSteering.MembershipFunctions.AddTriangle("SteerHardRight", 0.5 * steeringValue, steeringValue, steeringValue);
        

        fuzzyEngine = new FuzzyEngineFactory().Default();


        var rule1 = Rule.If(carPosToLine.Is(carFarLeft).And(carVel.Is(velFastLeft))).Then(carSteering.Is(steerHardRight));
        var rule2 = Rule.If(carPosToLine.Is(carFarLeft).And(carVel.Is(velLeft))).Then(carSteering.Is(steerHardRight));
        var rule3 = Rule.If(carPosToLine.Is(carFarLeft).And(carVel.Is(velStill))).Then(carSteering.Is(steerRight));
        var rule4 = Rule.If(carPosToLine.Is(carFarLeft).And(carVel.Is(velRight))).Then(carSteering.Is(steerRight));
        var rule5 = Rule.If(carPosToLine.Is(carFarLeft).And(carVel.Is(velFastRight))).Then(carSteering.Is(steerRight));

        var rule6 = Rule.If(carPosToLine.Is(carLeft).And(carVel.Is(velFastLeft))).Then(carSteering.Is(steerHardRight));
        var rule7 = Rule.If(carPosToLine.Is(carLeft).And(carVel.Is(velLeft))).Then(carSteering.Is(steerHardRight));
        var rule8 = Rule.If(carPosToLine.Is(carLeft).And(carVel.Is(velStill))).Then(carSteering.Is(steerRight));
        var rule9 = Rule.If(carPosToLine.Is(carLeft).And(carVel.Is(velRight))).Then(carSteering.Is(noSteering));
        var rule10 = Rule.If(carPosToLine.Is(carLeft).And(carVel.Is(velFastRight))).Then(carSteering.Is(steerLeft));

        var rule11 = Rule.If(carPosToLine.Is(carCentre).And(carVel.Is(velFastLeft))).Then(carSteering.Is(steerHardRight));
        var rule12 = Rule.If(carPosToLine.Is(carCentre).And(carVel.Is(velLeft))).Then(carSteering.Is(steerRight));
        var rule13 = Rule.If(carPosToLine.Is(carCentre).And(carVel.Is(velStill))).Then(carSteering.Is(noSteering));
        var rule14 = Rule.If(carPosToLine.Is(carCentre).And(carVel.Is(velRight))).Then(carSteering.Is(steerLeft));
        var rule15 = Rule.If(carPosToLine.Is(carCentre).And(carVel.Is(velFastRight))).Then(carSteering.Is(steerHardLeft));

        var rule16 = Rule.If(carPosToLine.Is(carRight).And(carVel.Is(velFastLeft))).Then(carSteering.Is(steerRight));
        var rule17 = Rule.If(carPosToLine.Is(carRight).And(carVel.Is(velLeft))).Then(carSteering.Is(noSteering));
        var rule18 = Rule.If(carPosToLine.Is(carRight).And(carVel.Is(velStill))).Then(carSteering.Is(steerLeft));
        var rule19 = Rule.If(carPosToLine.Is(carRight).And(carVel.Is(velRight))).Then(carSteering.Is(steerHardLeft));
        var rule20 = Rule.If(carPosToLine.Is(carRight).And(carVel.Is(velFastRight))).Then(carSteering.Is(steerHardLeft));

        var rule21 = Rule.If(carPosToLine.Is(carFarRight).And(carVel.Is(velFastLeft))).Then(carSteering.Is(steerLeft));
        var rule22 = Rule.If(carPosToLine.Is(carFarRight).And(carVel.Is(velLeft))).Then(carSteering.Is(steerLeft));
        var rule23 = Rule.If(carPosToLine.Is(carFarRight).And(carVel.Is(velStill))).Then(carSteering.Is(steerLeft));
        var rule24 = Rule.If(carPosToLine.Is(carFarRight).And(carVel.Is(velRight))).Then(carSteering.Is(steerHardLeft));
        var rule25 = Rule.If(carPosToLine.Is(carFarRight).And(carVel.Is(velFastRight))).Then(carSteering.Is(steerHardLeft));

        fuzzyEngine.Rules.Add(rule1, rule2, rule3, rule4, rule5, rule6, rule7, rule8, rule9, rule10, rule11, rule12, rule13,
                              rule14, rule15, rule16, rule17, rule18, rule19, rule20, rule21, rule22, rule23, rule24, rule25);

        //Debug.Log(result);
        //Debug.Log("linex" + linePosX);
        //Debug.Log("carx" + carPosX);
        //Debug.Log("dist " + dist);
        //Debug.Log("vel " + velocity);
        //Debug.Log("scale " + steeringValue);
        //Debug.Log("maxvel " + maxCarVel);
        

        result = fuzzyEngine.Defuzzify(new { CarPosition = dist, CarVelocity = velocity });

        //Debug.Log("result " + (double)result);

         car.SetSteering((float)result);
        
        float resultf = (float)result;
        steeringText.text = ("Steering: " + resultf.ToString("F3"));
    }
    
}
