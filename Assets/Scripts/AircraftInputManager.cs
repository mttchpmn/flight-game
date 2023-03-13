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

    [Header("Input Attributes")]
    public int MaxFlapIncrements = 2;
    public KeyCode BrakeKey = KeyCode.Space;
    public float throttleIncrement = 0.1f;

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
        HandleThrottleChange();

        Brake = Input.GetKey(BrakeKey) ? 1f : 0f;

        if (Input.GetKeyDown(KeyCode.F))
            Flaps += 1;
        if (Input.GetKeyDown(KeyCode.G))
            Flaps -= 1;

        Flaps = Mathf.Clamp(Flaps, 0, MaxFlapIncrements);
    }

    private void HandleThrottleChange()
    {
        if (Input.GetKey(KeyCode.D))
        {
            ThrottleSetting += (throttleIncrement * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            ThrottleSetting -= (throttleIncrement * Time.deltaTime);
        }
        // ThrottleSetting += (Throttle * ThrottleSpeed * Time.deltaTime);
        
        ThrottleSetting = Mathf.Clamp01(ThrottleSetting);
    }
}