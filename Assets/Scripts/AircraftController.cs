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

    void Update()
    {
        _flightModelController.ApplyFlightModel();
    }
}