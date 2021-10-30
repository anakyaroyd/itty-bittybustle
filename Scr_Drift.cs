using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Drift : MonoBehaviour
{
    Scr_InputController _input;

    public bool isBraking;

    public bool steer;
    public bool power;

    private WheelCollider wheelCollider;

    private WheelFrictionCurve forwardFriction, sidewaysFriction;

    [SerializeField] private float BrakeInput;

    public float BrakeTorque { get; set; }

    public float BrakeInputFriction { get; private set; }

    public float handBrakeFrictionMultiplier = 2;
    public float frictionMultiplier = 2;

    [Header ("Power Wheel Variables")]

    [SerializeField] private float exSlip = 1.5f;
    [SerializeField] private float exValue = 5f;
    [SerializeField] private float asympSlip = 2f;
    [SerializeField] private float asympValue = 4f;
 
    [SerializeField] private float newExSlip = 5f;
    [SerializeField] private float newExValue = 4f;
    [SerializeField] private float newAsympSlip = 5f;
    [SerializeField] private float newAsympValue = 3f;

    [SerializeField] private float fExSlip = 4f;
    [SerializeField] private float fExValue = 5f;
    [SerializeField] private float fAsympSlip = 4f;
    [SerializeField] private float fAsympValue = 4f;

    [SerializeField] private float newFExSlip = 4f;
    [SerializeField] private float newFExValue = 4f;
    [SerializeField] private float newFAsympSlip = 4f;
    [SerializeField] private float newFAsympValue = 3f;

    [Header ("Steer Wheel Variables")]

    [SerializeField] private float exSlipS = 4f;
    [SerializeField] private float exValueS = 8f;
    [SerializeField] private float asympSlipS = 2f;
    [SerializeField] private float asympValueS = 4f;

    [SerializeField] private float newExSlipS = 5.5f;
    [SerializeField] private float newExValueS = 7f;
    [SerializeField] private float newAsympSlipS = 6f;
    [SerializeField] private float newAsympValueS = 3f;

    [SerializeField] private float fExSlipS = 4f;
    [SerializeField] private float fExValueS = 8f;
    [SerializeField] private float fAsympSlipS = 2f;
    [SerializeField] private float fAsympValueS = 4f;

    [SerializeField] private float newFExSlipS = 4f;
    [SerializeField] private float newFExValueS = 4f;
    [SerializeField] private float newFAsympSlipS = 4f;
    [SerializeField] private float newFAsympValueS = 3f;

    void Awake()
    {
        _input = GameObject.FindGameObjectWithTag("GM").GetComponent<Scr_InputController>();

        wheelCollider = GetComponent<WheelCollider>();

        sidewaysFriction = wheelCollider.sidewaysFriction;
        forwardFriction = wheelCollider.forwardFriction;
    }

    private void Start() {
        sidewaysFriction.extremumValue = exValue;
        sidewaysFriction.extremumSlip = exSlip;
        sidewaysFriction.asymptoteSlip = asympSlip;
        sidewaysFriction.asymptoteValue = asympValue;

        forwardFriction.extremumValue = fExValue;
        forwardFriction.extremumSlip = fExSlip;
        forwardFriction.asymptoteSlip = fAsympSlip;
        forwardFriction.asymptoteValue = fAsympValue;
    }

    void FixedUpdate()
    {
        BrakeInput = _input.BrakeInput;

        if (BrakeInput != 0f) {
            isBraking = true;
        } else { isBraking = false; }

        if (power) {
            if (isBraking) {
                sidewaysFriction.extremumValue = newExValue;
                sidewaysFriction.extremumSlip = newExSlip;
                sidewaysFriction.asymptoteSlip = newAsympSlip;
                sidewaysFriction.asymptoteValue = newAsympValue;

                forwardFriction.extremumValue = newFExValue;
                forwardFriction.extremumSlip = newFExSlip;
                forwardFriction.asymptoteSlip = newFAsympSlip;
                forwardFriction.asymptoteValue = newFAsympValue;
            }

            if (!isBraking) {
                sidewaysFriction.extremumValue = exValue;
                sidewaysFriction.extremumSlip = exSlip;
                sidewaysFriction.asymptoteSlip = asympSlip;
                sidewaysFriction.asymptoteValue = asympValue;

                forwardFriction.extremumValue = fExValue;
                forwardFriction.extremumSlip = fExSlip;
                forwardFriction.asymptoteSlip = fAsympSlip;
                forwardFriction.asymptoteValue = fAsympValue;
            }
        }

        if (steer) {
            if (isBraking) {
                sidewaysFriction.extremumValue = newExValueS;
                sidewaysFriction.extremumSlip = newExSlipS;
                sidewaysFriction.asymptoteSlip = newAsympSlipS;
                sidewaysFriction.asymptoteValue = newAsympValueS;

                forwardFriction.extremumValue = newFExValueS;
                forwardFriction.extremumSlip = newFExSlipS;
                forwardFriction.asymptoteSlip = newFAsympSlipS;
                forwardFriction.asymptoteValue = newFAsympValueS;
            }

            if (!isBraking) {
                sidewaysFriction.extremumValue = exValueS;
                sidewaysFriction.extremumSlip = exSlipS;
                sidewaysFriction.asymptoteSlip = asympSlipS;
                sidewaysFriction.asymptoteValue = asympValueS;

                forwardFriction.extremumValue = fExValueS;
                forwardFriction.extremumSlip = fExSlipS;
                forwardFriction.asymptoteSlip = fAsympSlipS;
                forwardFriction.asymptoteValue = fAsympValueS;
            }
        }

        wheelCollider.sidewaysFriction = sidewaysFriction;
        wheelCollider.forwardFriction = forwardFriction;
    }
}

