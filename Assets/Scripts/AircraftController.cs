using UnityEngine;

[RequireComponent(typeof(AircraftInputHandler))]
[RequireComponent(typeof(Rigidbody))]
public class AircraftController : MonoBehaviour
{
    private AircraftFlightModelController _flightModelController;

    void Start()
    {
        _flightModelController = GetComponent<AircraftFlightModelController>();
    }

    void FixedUpdate()
    {
        _flightModelController.ApplyFlightModel();
    }
}