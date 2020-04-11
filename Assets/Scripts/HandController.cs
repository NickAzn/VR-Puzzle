using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour {

    int layerMask = 1 << 8;
    public Transform raycastIndicator;

    private void Update() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10f, layerMask)) {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            raycastIndicator.position = hit.point;
            Debug.Log("RaycastHit");
        }
    }
}
