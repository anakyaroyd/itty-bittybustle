using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Boost : MonoBehaviour
{
    Scr_InputController _input;

    Rigidbody _rb;

    public bool isBraking;

    [SerializeField] private float _boost;

    public bool steer;
    public bool power;

    void Awake() {
        _input = GameObject.FindGameObjectWithTag("GM").GetComponent<Scr_InputController>();

        _rb = GetComponent<Rigidbody>();
    }

    private void Start() {
        _rb.drag = 1f;
        _rb.angularDrag = 0.2f;
    }

    void FixedUpdate() {

        _boost = _input.BoostInput;

        if(_input.BrakeInput != 0f) {
            isBraking = true;
        } else {
            isBraking = false;
        }

        if (_boost != 0f) {
            if (!isBraking) {
                _rb.drag = 0f;
                _rb.angularDrag = 0.05f;
            } else {
                _rb.drag = 1f;
                _rb.angularDrag = 0.2f;
            }
        } else {
            _rb.drag = 1f;
            _rb.angularDrag = 0.2f;
        }
    }
}

