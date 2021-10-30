using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Scr_Car : MonoBehaviour {
    public Transform centerOfMass;

    public float SteerInput { get; set; }
    public float ThrottleInput { get; set; }
    public float BrakeInput { get; set; }

    private Rigidbody _rigidbody;

    private Scr_Wheel[] wheels;

    public float carVel;

    void Awake() {
        wheels = GetComponentsInChildren<Scr_Wheel>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = centerOfMass.localPosition;
    }

    void FixedUpdate() {
        carVel = _rigidbody.velocity.magnitude;
    }

}