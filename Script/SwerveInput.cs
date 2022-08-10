using System;
using UnityEngine;

public class SwerveInput : MonoBehaviour
{

    private float _lastFrameFingerPositionX;
    private float _moveFactorX;
    
    public float MoveFactorX { get => _moveFactorX; }
    public static SwerveInput I;

    private void Awake()
    {
        I = this;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _lastFrameFingerPositionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            _moveFactorX = Input.mousePosition.x - _lastFrameFingerPositionX;
            _lastFrameFingerPositionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _moveFactorX = 0f;
        }
    }
}



