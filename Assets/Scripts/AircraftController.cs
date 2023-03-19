using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(AircraftInputHandler))]
[RequireComponent(typeof(Rigidbody))]
public class AircraftController : MonoBehaviour
{
    private AircraftInputHandler _inputHandler;
    private Rigidbody aircraft;
    
    public Transform aircraftCog;
    public Transform aircraftEngine;
    
    public float elevatorForce = 100f;
    
    public TextMeshProUGUI throttleText;
    public TextMeshProUGUI velocityText;
    
    public float maxThrust;

    void Start()
    {
        _inputHandler = GetComponent<AircraftInputHandler>();
        aircraft = GetComponent<Rigidbody>();

        aircraft.centerOfMass = aircraftCog.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        HandleThrottle();
        HandleVelocity();

        HandleElevator();
    }

    private void HandleElevator()
    {
        var pitchInput = _inputHandler.Pitch;
        var normalizedVelocity = aircraft.velocity.normalized.z;
        
        // aircraft.AddRelativeForce(aircraft.transform.up * (pitchInput * elevatorForce));
        aircraft.AddRelativeTorque(transform.right * ( normalizedVelocity * pitchInput * elevatorForce));
    }

    private void HandleVelocity()
    {
        var velocityMetresPerSecond = aircraft.velocity.z;
        var velocityKnots = velocityMetresPerSecond * 1.94384;
        velocityText.SetText($"Velocity: {Math.Round(velocityKnots, 2)} knots");
    }

    private void HandleThrottle()
    {
        var throttleValue = _inputHandler.ThrottleSetting;
        throttleText.SetText($"Throttle: {Math.Round(throttleValue, 2):P}");

        aircraft.AddRelativeForce(throttleValue * maxThrust *
                                  aircraftEngine.transform.forward); // TODO - Apply to engine transform
        Debug.DrawRay(aircraftEngine.transform.position, aircraftEngine.transform.forward * 25, Color.magenta);
    }
}
