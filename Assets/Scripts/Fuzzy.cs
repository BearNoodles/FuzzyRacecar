using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FLS;
using FLS.Rules;


public class Fuzzy : MonoBehaviour {

    CarScript car;
    LineScript line;
    double linePosX;
    double carPosX;
    double velocity;
    double steeringValue;
    double maxCarVel;
    IFuzzyEngine fuzzyEngine;

    double result;

    // Use this for initialization
    void Start ()
    {
        car = GameObject.FindGameObjectWithTag("Car").GetComponent<CarScript>();
        line = GameObject.FindGameObjectWithTag("Line").GetComponent<LineScript>();
        maxCarVel = car.GetMaxVelocity();
        Debug.Log(maxCarVel);
        steeringValue = car.GetSteeringScale();

        //var linePos = new LinguisticVariable("Line Position");
        //var left = linePos.MembershipFunctions.AddTrapezoid("Left", 0, 0, 20, 40);
        //var middle = linePos.MembershipFunctions.AddTriangle("Middle", 30, 50, 70);
        //var right = linePos.MembershipFunctions.AddTrapezoid("Right", 50, 80, 100, 100);



        
        var carPosToLine = new LinguisticVariable("CarPosition");
        var carFarLeft = carPosToLine.MembershipFunctions.AddTrapezoid("FarLeft", -3.0, -3.0, -2.0, -1.0);
        var carLeft = carPosToLine.MembershipFunctions.AddTriangle("Left", -2.0, -1.0, 0.0);
        var carCentre = carPosToLine.MembershipFunctions.AddTriangle("Centre", -1.0, 0.0, 1.0);
        var carRight = carPosToLine.MembershipFunctions.AddTriangle("Right", 0.0, 1.0, 2.0);
        var carFarRight = carPosToLine.MembershipFunctions.AddTrapezoid("FarRight", 1.0, 2.0, 3.0, 3.0);

        //mems.Add(carPosToLine);
        //
        var carVel = new LinguisticVariable("CarVelocity");
        var velFastLeft = carVel.MembershipFunctions.AddTrapezoid("FastLeft", -1.5 * maxCarVel, -1.5 * maxCarVel, -maxCarVel, -0.5 * maxCarVel);
        var velLeft = carVel.MembershipFunctions.AddTriangle("Left", -maxCarVel, -0.5 * maxCarVel, 0.0);
        var velStill = carVel.MembershipFunctions.AddTriangle("Still", -0.5 * maxCarVel, 0.0, 0.5 * maxCarVel);
        var velRight = carVel.MembershipFunctions.AddTriangle("Right", 0.0, 0.5 * maxCarVel, maxCarVel);
        var velFastRight = carVel.MembershipFunctions.AddTrapezoid("FastRight", 0.5 * maxCarVel, maxCarVel, 1.5 * maxCarVel, 1.5 * maxCarVel);

        //mems.Add(carVel);

        var carSteering = new LinguisticVariable("CarSteering");
        var steerHardLeft = carSteering.MembershipFunctions.AddTriangle("SteerHardLeft", -steeringValue, -steeringValue, -0.5 * steeringValue);
        var steerLeft = carSteering.MembershipFunctions.AddTriangle("SteerLeft", -steeringValue, -0.5 * steeringValue, 0.0);
        var noSteering = carSteering.MembershipFunctions.AddTriangle("NoSteering", -0.5 * steeringValue, 0.0, 0.5 * steeringValue);
        var steerRight = carSteering.MembershipFunctions.AddTriangle("SteerRight", 0.0, 0.5 * steeringValue, steeringValue);
        var steerHardRight = carSteering.MembershipFunctions.AddTriangle("SteerHardRight", 0.5 * steeringValue, steeringValue, steeringValue);

        //
        //IFuzzyEngine fuzzyEngine = new FuzzyEngineFactory().Default();
        fuzzyEngine = new FuzzyEngineFactory().Default();
        //fuzzyEngine = new FuzzyEngineFactory().Create(new MoMDefuzzification());

        var rule1 = Rule.If(carPosToLine.Is(carFarLeft).And(carVel.Is(velFastLeft))).Then(carSteering.Is(steerHardRight));
        var rule2 = Rule.If(carPosToLine.Is(carFarLeft).And(carVel.Is(velLeft))).Then(carSteering.Is(steerHardRight));
        var rule3 = Rule.If(carPosToLine.Is(carFarLeft).And(carVel.Is(velStill))).Then(carSteering.Is(steerHardRight));
        var rule4 = Rule.If(carPosToLine.Is(carFarLeft).And(carVel.Is(velRight))).Then(carSteering.Is(steerHardRight));
        var rule5 = Rule.If(carPosToLine.Is(carFarLeft).And(carVel.Is(velFastRight))).Then(carSteering.Is(noSteering));

        var rule6 = Rule.If(carPosToLine.Is(carLeft).And(carVel.Is(velFastLeft))).Then(carSteering.Is(steerHardRight));
        var rule7 = Rule.If(carPosToLine.Is(carLeft).And(carVel.Is(velLeft))).Then(carSteering.Is(steerHardRight));
        var rule8 = Rule.If(carPosToLine.Is(carLeft).And(carVel.Is(velStill))).Then(carSteering.Is(steerRight));
        var rule9 = Rule.If(carPosToLine.Is(carLeft).And(carVel.Is(velRight))).Then(carSteering.Is(noSteering));
        var rule10 = Rule.If(carPosToLine.Is(carLeft).And(carVel.Is(velFastRight))).Then(carSteering.Is(noSteering));

        var rule11 = Rule.If(carPosToLine.Is(carCentre).And(carVel.Is(velFastLeft))).Then(carSteering.Is(steerHardRight));
        var rule12 = Rule.If(carPosToLine.Is(carCentre).And(carVel.Is(velLeft))).Then(carSteering.Is(steerRight));
        var rule13 = Rule.If(carPosToLine.Is(carCentre).And(carVel.Is(velStill))).Then(carSteering.Is(noSteering));
        var rule14 = Rule.If(carPosToLine.Is(carCentre).And(carVel.Is(velRight))).Then(carSteering.Is(steerLeft));
        var rule15 = Rule.If(carPosToLine.Is(carCentre).And(carVel.Is(velFastRight))).Then(carSteering.Is(steerHardLeft));

        var rule16 = Rule.If(carPosToLine.Is(carRight).And(carVel.Is(velFastLeft))).Then(carSteering.Is(noSteering));
        var rule17 = Rule.If(carPosToLine.Is(carRight).And(carVel.Is(velLeft))).Then(carSteering.Is(noSteering));
        var rule18 = Rule.If(carPosToLine.Is(carRight).And(carVel.Is(velStill))).Then(carSteering.Is(steerLeft));
        var rule19 = Rule.If(carPosToLine.Is(carRight).And(carVel.Is(velRight))).Then(carSteering.Is(steerHardLeft));
        var rule20 = Rule.If(carPosToLine.Is(carRight).And(carVel.Is(velFastRight))).Then(carSteering.Is(steerHardLeft));

        var rule21 = Rule.If(carPosToLine.Is(carFarRight).And(carVel.Is(velFastLeft))).Then(carSteering.Is(noSteering));
        var rule22 = Rule.If(carPosToLine.Is(carFarRight).And(carVel.Is(velLeft))).Then(carSteering.Is(steerHardLeft));
        var rule23 = Rule.If(carPosToLine.Is(carFarRight).And(carVel.Is(velStill))).Then(carSteering.Is(steerHardLeft));
        var rule24 = Rule.If(carPosToLine.Is(carFarRight).And(carVel.Is(velRight))).Then(carSteering.Is(steerHardLeft));
        var rule25 = Rule.If(carPosToLine.Is(carFarRight).And(carVel.Is(velFastRight))).Then(carSteering.Is(steerHardLeft));

        fuzzyEngine.Rules.Add(rule1, rule2, rule3, rule4, rule5, rule6, rule7, rule8, rule9, rule10, rule11, rule12, rule13,
                              rule14, rule15, rule16, rule17, rule18, rule19, rule20, rule21, rule22, rule23, rule24, rule25);
        
        

    }

    // Update is called once per frame
    void Update()
    {
        linePosX = line.GetPositionX();
        carPosX = car.GetPositionX();
        velocity = car.GetVelocityX();
        double dist = carPosX - linePosX;

        //Debug.Log(result);
        //Debug.Log("linex" + linePosX);
        //Debug.Log("carx" + carPosX);
        //Debug.Log("dist " + dist);
        //Debug.Log("vel " + velocity);
        //Debug.Log("scale " + steeringValue);
        //Debug.Log("maxvel " + maxCarVel);

        //var result = fuzzyEngine.Defuzzify(new { carPosToLine = (double)(carPosX - linePosX), carVel = (double)velocity });

        result = fuzzyEngine.Defuzzify(new { CarPosition = dist, CarVelocity = velocity });

        //Debug.Log("result " + (double)result);

        car.SetSteering((float)result);

        
    }
}
