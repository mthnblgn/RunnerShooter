using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward);
        if (transform.position.y <= .75f)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.UpdateMoney(100);
            transform.parent.gameObject.SetActive(false);         
        }
    }

}
