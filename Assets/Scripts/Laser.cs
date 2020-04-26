using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Laser : MonoBehaviour {

    int mask = 1 << 10;
    public LineRenderer lr;
    public float laserWidth = 0.05f;

    private void Start() {
        lr.startWidth = laserWidth;
        lr.endWidth = laserWidth;
    }

    private void Update() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, Mathf.Infinity, mask)) {
            if (hit.transform.tag.Equals("Player")) {
                Debug.Log("Player Destroyed");
                SceneManager.LoadScene(0);
            }

            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, hit.point);
            lr.enabled = true;
        } else {
            lr.enabled = false;
        }
    }
}
