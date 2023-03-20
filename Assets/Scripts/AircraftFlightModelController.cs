using System;
using System.ComponentModel;
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
    [SerializeField] private bool enableYaw = false;
    [SerializeField] private Transform centreOfGravity;
    [SerializeField] private Transform centreOfPressure;
    [SerializeField] private float maxAirspeed = 120f;
    [SerializeField] private AnimationCurve controlEffectiveness = AnimationCurve.EaseInOut(0,0,1,1);
    
    [Header("Pitch Configuration")]
    [SerializeField] private bool enablePitch = true;
    [SerializeField] private float pitchMultiplier = 50;
    
    [Header("Roll Configuration")]
    [SerializeField] private bool enableRoll = true;
    [SerializeField] private float rollMultiplier = 100;

    [Header("Thrust Configuration")] 
    [SerializeField] private bool enableThrust = true;
    [SerializeField] private float thrustFactor = 500f;
    [SerializeField] private Transform thrustLocation;

    public float testLiftForce;

    public float Velocity { get; set; }
    public float VelocityKnots { get; set; }
    public float VelocityNormalized { get; set; }
    public float AngleOfAttack { get; set; }

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _inputHandler = GetComponent<AircraftInputHandler>();

        _rigidBody.centerOfMass = centreOfGravity.localPosition;
    }

    public void ApplyFlightModel()
    {
        CalculateVelocity();
        CalculateAngleOfAttack();

        ApplyThrust();
        ApplyLift();
        ApplyDrag();

        ApplyPitch();
        ApplyRoll();
        ApplyYaw();
    }

    private void CalculateAngleOfAttack()
    {
    }

    private void CalculateVelocity()
    {
        Velocity = _rigidBody.velocity.magnitude; // TODO - Is this correct?
        VelocityKnots = Velocity * 1.94384f;
        
        // Test
        VelocityNormalized = (Velocity / maxAirspeed) * 10;

        Debug.DrawRay(_rigidBody.position, _rigidBody.velocity * 50, Color.green);
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

        // Draw lift vector
        Debug.DrawRay(centreOfPressure.position, centreOfPressure.transform.up * 25, Color.cyan);

        // TODO
        // Test force
        Debug.DrawRay(centreOfPressure.position, centreOfPressure.transform.up * 25, Color.cyan);
        _rigidBody.AddRelativeForce(centreOfPressure.transform.up * testLiftForce);
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

        var pitchInput = _inputHandler.Pitch;

        // var pitchForce = (VelocityFactor * pitchInput * elevatorFactor);
        var effectiveness = controlEffectiveness.Evaluate(VelocityNormalized);
        var pitchForce = (pitchInput * effectiveness * pitchMultiplier);
        Debug.Log($"Pitch force: {pitchForce}");
        
        _rigidBody.AddRelativeTorque(transform.right * pitchForce, ForceMode.Impulse);
    }

    private void ApplyRoll()
    {
        if (!enableRoll)
            return;
        
        var rollInput = _inputHandler.Roll;

        // var pitchForce = (VelocityFactor * pitchInput * elevatorFactor);
        var effectiveness = controlEffectiveness.Evaluate(VelocityNormalized);
        var rollForce = (rollInput * effectiveness * rollMultiplier);
        Debug.Log($"Roll force: {rollForce}");
        
        _rigidBody.AddRelativeTorque(transform.forward * rollForce, ForceMode.Impulse);
    }

    private void ApplyYaw()
    {
        if (!enableYaw)
            return;
        // TODO
    }
}