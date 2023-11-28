using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateRandomizer : MonoBehaviour
{
    [SerializeField]Gate _gatePrefab;
    [SerializeField] int _startPoint;
    [SerializeField] int _endPoint;
    [SerializeField] int _distance;
    void Start()
    {
        int layerCount = Mathf.FloorToInt((_endPoint - _startPoint) / _distance);
        for (int i = 0; i < layerCount; i++)
        {
           if(fiftyPercent()==1) Instantiate(_gatePrefab, new Vector3(-2, 2, _startPoint + _distance * i), Quaternion.identity);
            if(fiftyPercent()==1)Instantiate(_gatePrefab, new Vector3( 2, 2, _startPoint + _distance * i), Quaternion.identity);
        }
    }
    int fiftyPercent()
    {
        return Random.Range(0, 2);
    }
}
