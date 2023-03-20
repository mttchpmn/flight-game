using System;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class AircraftFlightModelController : MonoBehaviour
{
    private AircraftInputHandler _inputHandler;
    private Rigidbody _rigidBody;

    [Header("Aerodynamics configuration")] 
    [SerializeField] private bool enableLift = false;
    [SerializeField] private bool enableDrag = false;
    [SerializeField] private bool enablePitch = false;
    [SerializeField] private bool enableRoll = false;
    [SerializeField] private bool enableYaw = false;
    [SerializeField] private Transform centreOfGravity;
    [SerializeField] private Transform centreOfPressure;

    [Header("Thrust Configuration")]
    [SerializeField] private bool enableThrust = false;
    [SerializeField] private float thrustFactor = 500f;
    [SerializeField] private Transform thrustLocation;

    public float VelocityKnots { get; set; }

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _inputHandler = GetComponent<AircraftInputHandler>();
        
        _rigidBody.centerOfMass = centreOfGravity.localPosition;
    }

    public void ApplyFlightModel()
    {
        ApplyThrust();
        ApplyLift();
        ApplyDrag();

        ApplyPitch();
        ApplyRoll();
        ApplyYaw();

        CalculateVelocity();
    }

    private void CalculateVelocity()
    {
        var velocity = _rigidBody.velocity.magnitude; // TODO - Is this correct?
        VelocityKnots = velocity * 1.94384f;
    }

    private void ApplyThrust()
    {
        if (!enableThrust)
            return;

        var throttleValue = _inputHandler.ThrottleSetting;

        _rigidBody.AddRelativeForce(throttleValue * thrustFactor *
                                    thrustLocation.transform.forward);
        Debug.DrawRay(thrustLocation.transform.position, thrustLocation.transform.forward * 25, Color.magenta);
    }

    private void ApplyLift()
    {
        if (!enableLift)
            return;
        // TODO
    }

    private void ApplyDrag()
    {
        if (!enableDrag)
            return;
        // TODO
    }

    private void ApplyPitch()
    {
        if (!enablePitch)
            return;
        // TODO
    }

    private void ApplyRoll()
    {
        if (!enableRoll)
            return;
        // TODO
    }

    private void ApplyYaw()
    {
        if (!enableYaw)
            return;
        // TODO
    }
    
    // private void HandleElevator()
    // {
    //     var pitchInput = _inputHandler.Pitch;
    //     var normalizedVelocity = _aircraft.velocity.normalized.z;
    //     
    //     // aircraft.AddRelativeForce(aircraft.transform.up * (pitchInput * elevatorForce));
    //     _aircraft.AddRelativeTorque(transform.right * ( normalizedVelocity * pitchInput * elevatorForce));
    // }
    //
    // private void HandleVelocity()
    // {
    //     var velocityMetresPerSecond = _aircraft.velocity.z;
    //     var velocityKnots = velocityMetresPerSecond * 1.94384;
    //     // velocityText.SetText($"Velocity: {Math.Round(velocityKnots, 2)} knots");
    // }
}