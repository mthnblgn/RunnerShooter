using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{

    private IObjectPool<Bullet> objectPool;
    private int _multiplier=1;
    public bool _isTriggered=false;

    // public property to give the projectile a reference to its ObjectPool
    public IObjectPool<Bullet> ObjectPool { set => objectPool = value; }
    public int Multiplier { set => _multiplier = value; get => _multiplier; }

    public void Deactivate(float range)
    {
        StartCoroutine(DeactivateRoutine(range));
    }

    IEnumerator DeactivateRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        // reset the moving Rigidbody
        Rigidbody rBody = GetComponent<Rigidbody>();
        rBody.velocity = new Vector3(0f, 0f, 0f);
        rBody.angularVelocity = new Vector3(0f, 0f, 0f);
        // release the projectile back to the pool
        objectPool.Release(this);
    }
    private void OnDisable()
    {
        _isTriggered = false;
    }
}
