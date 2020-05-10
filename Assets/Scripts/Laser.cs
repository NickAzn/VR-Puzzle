using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Laser : MonoBehaviour {

    int mask = 1 << 10;
    public LineRenderer lr;
    public float laserWidth = 0.05f;
    public Transform particles;

    private void Start() {
        particles.gameObject.SetActive(false);
        lr.startWidth = laserWidth;
        lr.endWidth = laserWidth;
    }

    private void Update() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, Mathf.Infinity, mask)) {
            if (hit.transform.tag.Equals("Player")) {
                Debug.Log("Player Destroyed (Laser)");
                GameController.instance.PlayerDestroyed(hit.transform.gameObject);
            }

            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, hit.point);
            lr.enabled = true;
            particles.position = hit.point;
            particles.rotation = transform.rotation;
            if (!particles.gameObject.activeInHierarchy)
                particles.gameObject.SetActive(true);
        } else {
            lr.enabled = false;
            particles.gameObject.SetActive(false);
        }
    }
}
