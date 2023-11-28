using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Target : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI _powerText;
    [SerializeField] protected int _power;
    protected abstract void OnGetShot(Collider other);
    protected abstract void OnPass(Collider other);
}
