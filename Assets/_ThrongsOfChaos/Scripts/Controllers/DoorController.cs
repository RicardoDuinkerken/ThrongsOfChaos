using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private ObjectExplosion obj;
    [SerializeField] private float CleanupWaitTime;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            obj.Explode();
            StartCoroutine(Cleanup());
        }
    }

    private IEnumerator Cleanup()
    {
        yield return new WaitForSeconds(CleanupWaitTime);
        Destroy(this.gameObject);
        foreach (var rb in obj.GetRbShatteredPieces)
        {
            Destroy(rb.gameObject);
            //rb.isKinematic = true;
            //rb.useGravity = false;
        }
        Destroy(this.gameObject);
    }
}
