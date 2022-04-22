using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovingCube : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private bool _isMoving = false;
    [SerializeField] private float _force;
    public void StartMove()
    {
        _isMoving = true;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_isMoving)
        {
            _rigidbody.AddForce(Vector3.forward * _force);
            _isMoving = false;
        }
    }

    private void CubeIsMoving()
    {
    }

}
