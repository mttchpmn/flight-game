using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(AircraftInputManager))]
public class AircraftManager : MonoBehaviour
{
    private AircraftInputManager inputManager;

    public TextMeshProUGUI throttleText;
    
    void Start()
    {
        inputManager = GetComponent<AircraftInputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        var throttleValue = inputManager.ThrottleSetting;
        throttleText.SetText($"Throttle: {Math.Round(throttleValue, 2):P}");
        
    }
}
