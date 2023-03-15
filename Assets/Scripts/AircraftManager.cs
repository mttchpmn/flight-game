using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(AircraftInputManager))]
[RequireComponent(typeof(Rigidbody))]
public class AircraftManager : MonoBehaviour
{
    private AircraftInputManager inputManager;
    private Rigidbody aircraft;
    
    public Transform aircraftCog;
    public Transform aircraftEngine;
    public TextMeshProUGUI throttleText;
    public TextMeshProUGUI velocityText;
    public float maxThrust;

    void Start()
    {
        inputManager = GetComponent<AircraftInputManager>();
        aircraft = GetComponent<Rigidbody>();

        aircraft.centerOfMass = aircraftCog.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        var throttleValue = inputManager.ThrottleSetting;
        throttleText.SetText($"Throttle: {Math.Round(throttleValue, 2):P}");
        
        aircraft.AddRelativeForce(throttleValue * maxThrust * aircraftEngine.transform.forward); // TODO - Apply to engine transform
        Debug.DrawRay(aircraftEngine.transform.position, aircraftEngine.transform.forward * 25, Color.magenta);
        
        var velocityMetresPerSecond = aircraft.velocity.z;
        var velocityKnots = velocityMetresPerSecond * 1.94384;
        velocityText.SetText($"Velocity: {Math.Round(velocityKnots, 2)} knots");
    }
}
