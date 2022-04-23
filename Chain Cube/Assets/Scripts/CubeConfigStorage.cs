using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "cube", menuName = "CubeConfig/CubeValueSettings")]
public class CubeConfigStorage : ScriptableObject
{
    [SerializeField] private string _cubeValue;
    [SerializeField] private Material _cubeMaterial;
    
    public string CubeValue => _cubeValue;
    public Material CubeMaterial => _cubeMaterial;
}
