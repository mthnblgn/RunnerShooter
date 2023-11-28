using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : Target
{
    [SerializeField] Transform _model;
    [SerializeField] Coin _coin;
    private void Start()
    {
        _powerText.text = _power.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        OnGetShot(other);
        OnPass(other);
    }
    protected override void OnGetShot(Collider other)
    {
        if (other.TryGetComponent(out Bullet bullet))
        {
            bullet._isTriggered = true;
            bullet.Deactivate(0);
            _power -= bullet.Multiplier;
            if (_power <= 0) { DestroyObstacle(); }
            _powerText.text = _power.ToString();
        }
    }
    protected override void OnPass(Collider other)
    {
        if (other.TryGetComponent<Gun>(out Gun gun))
        {
            gun.GameOver();
        }
    }
    private void DestroyObstacle()
    {
        _model.gameObject.SetActive(false);
        _powerText.canvas.enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        Rigidbody coinBody= _coin.gameObject.GetComponent<Rigidbody>();
        coinBody.AddForce(Vector3.up*100,ForceMode.Force);
        coinBody.useGravity = true;     
    }
}
