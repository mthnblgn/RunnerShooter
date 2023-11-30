using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] GameObject _winText;
    private void OnTriggerEnter(Collider other)
    {
        if (TryGetComponent(out other))
        {
            other.GetComponent<Rigidbody>().AddForce(new Vector3 (Random.Range(0f,1f), Random.Range(0f, 1f), Random.Range(0f, 1f))*500, ForceMode.Impulse);
        }
    }
}
