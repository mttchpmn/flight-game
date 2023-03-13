using UnityEngine;

public class AircraftInputManager : MonoBehaviour
{
    public float Pitch { get; protected set; } = 0f;
    public float Roll { get; protected set; } = 0f;
    public float Yaw { get; protected set; } = 0f;
    public int Flaps { get; protected set; } = 0;
    public float Brake { get; protected set; } = 0f;
    public float Throttle { get; protected set; } = 0f;
    public float ThrottleSetting { get; protected set; } = 0f;

    [Header("Input Configuration")]
    public int MaxFlapIncrements = 2;
    public float throttleIncrement = 0.1f;
    
    public KeyCode BrakeKey = KeyCode.Space;
    public KeyCode FlapsDown = KeyCode.F;
    public KeyCode FlapsUp = KeyCode.G;
    public KeyCode ThrottleUp = KeyCode.D;
    public KeyCode ThrottleDown = KeyCode.S;
    

    void Update()
    {
        HandleInput();
    }

    protected virtual void HandleInput()
    {
        Pitch = Input.GetAxis("Vertical");
        Roll = Input.GetAxis("Horizontal");
        Yaw = Input.GetAxis("Yaw");
        Throttle = Input.GetAxis("Throttle");
        Brake = Input.GetKey(BrakeKey) ? 1f : 0f;
        
        HandleThrottleChange();
        HandleFlapsChange();
    }

    private void HandleFlapsChange()
    {
        if (Input.GetKeyDown(FlapsDown))
            Flaps += 1;
        if (Input.GetKeyDown(FlapsUp))
            Flaps -= 1;

        Flaps = Mathf.Clamp(Flaps, 0, MaxFlapIncrements);
    }

    private void HandleThrottleChange()
    {
        if (Input.GetKey(ThrottleUp))
        {
            ThrottleSetting += (throttleIncrement * Time.deltaTime);
        }
        if (Input.GetKey(ThrottleDown))
        {
            ThrottleSetting -= (throttleIncrement * Time.deltaTime);
        }
        
        ThrottleSetting = Mathf.Clamp01(ThrottleSetting);
    }
}