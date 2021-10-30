using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Scr_Wheel : MonoBehaviour {
    Scr_InputController _input;

    public Rigidbody carRB;

    public bool steer;
    public bool invertSteer;
    public bool power;

    public bool isBraking;

    public float motorTorque = 700f; /* 2500f for full size car*/
    public float maxSteer = 20f;  /* 20f for full size car*/
    public float brakeTorque = 40f;  /* 200f for full size car*/

    private WheelCollider wheelCollider;
    private Transform wheelTransform;

    private WheelFrictionCurve forwardFriction, sidewaysFriction;

    private float SteerInput { get; set; }
    private float ThrottleInput { get; set; }
    private float BrakeInput { get; set; }

    public float SteerAngle { get; set; }
    public float Torque { get; set; }
    public float BrakeTorque { get; set; }

    void Awake() {
        // call input controller
        _input = GameObject.FindGameObjectWithTag("GM").GetComponent<Scr_InputController>();

        // get collider and mesh
        wheelCollider = GetComponentInChildren<WheelCollider>();
        wheelTransform = GetComponentInChildren<MeshRenderer>().GetComponent<Transform>();
    }

    void Update() {

        // update wheel position
        wheelCollider.GetWorldPose(out Vector3 pos, out Quaternion rot);
        wheelTransform.position = pos;
        wheelTransform.rotation = rot;

    }

    private void FixedUpdate() {

        // call input values from input controller
        ThrottleInput = _input.ThrottleInput;
        SteerInput = _input.SteerInput;
        BrakeInput = _input.BrakeInput;

        // deadzone for BrakeInput
        if (BrakeInput > .05f) {
            isBraking = true;
        } else {
            isBraking = false;
        }

        // if wheel is designated 'steer' wheel, then apply turn input
        if (steer) {
            wheelCollider.steerAngle = SteerInput * maxSteer * (invertSteer ? -1 : 1);
        }

        // if wheel is designated 'power' wheel, then apply throttle/brake
        if (power) {
            if (isBraking) {
                wheelCollider.motorTorque = 0f;
                wheelCollider.brakeTorque = BrakeInput * brakeTorque;
            } else {
                wheelCollider.motorTorque = ThrottleInput * motorTorque;
                wheelCollider.brakeTorque = 0f;
            }
        }

    }
}
