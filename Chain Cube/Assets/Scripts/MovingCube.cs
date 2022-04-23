using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class MovingCube : MonoBehaviour
{
    [SerializeField] private Material _material;
    [SerializeField] private float _movingForce;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _rebound;
    [SerializeField] private GameObject _aim;
    [SerializeField] private List<TMP_Text> _cubeValues;
    private Rigidbody _rigidbody;
    private GameManager _gameManager;
    private bool _isMoving = false;
    private bool _isUpdating = false;

    public bool IsUsed { get; set; } = true;
    public int Value { get; set; }

    private void OnCollisionEnter(Collision collision)
    {
            if(collision.gameObject.tag == gameObject.tag && collision.gameObject.GetComponent<MovingCube>().Value == Value)
            {
                if(!_isUpdating)
                {
                     _isUpdating = true;
                     _gameManager.CreateNewCube(collision.gameObject.GetComponent<MovingCube>());
                }
                Destroy(gameObject);
            }
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_isMoving)
        {
            _rigidbody.AddForce(Vector3.forward * _movingForce);
            _isMoving = false;
        }
    }
    private IEnumerator SetUsedFalse()
    {
        yield return new WaitForSeconds(1f);
        IsUsed = false;
    }
    public void StartMove()
    {
        Destroy(_aim);
        StartCoroutine(SetUsedFalse());
        _isMoving = true;
    }

    public void Jump()
    {
        _rigidbody.isKinematic = true;
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(Vector3.up * _jumpForce);
        _rigidbody.AddForce(Vector3.right * _rebound);
    }

    public void SetParametrs(GameManager gamaManager, CubeConfigStorage cubeValues)
    {
        _gameManager = gamaManager;
        gameObject.GetComponent<MeshRenderer>().material = cubeValues.CubeMaterial;
        foreach (var value in _cubeValues)
        {
            value.text = cubeValues.CubeValue;
        }
        Value = Convert.ToInt32(cubeValues.CubeValue);
    }
}
