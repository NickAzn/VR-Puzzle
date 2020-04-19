using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    int mask = 1 << 10;
    public LineRenderer lr;

    private void Start() {
        lr.startWidth = 0.1f;
        lr.endWidth = 0.05f;
    }

    private void Update() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, Mathf.Infinity, mask)) {
            if (hit.transform.tag.Equals("Player")) {
                Debug.Log("Player Destroyed");
            }

            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, hit.point);
            lr.enabled = true;
        } else {
            lr.enabled = false;
        }
    }
}
