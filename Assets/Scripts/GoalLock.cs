using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalLock : MonoBehaviour {

    public Material lockMat;
    public Material unlockMat;
    public Material highlightMat;
    public delegate void LockPress(GoalLock gl);
    public event LockPress OnLockPress;

    private void OnTriggerEnter(Collider collision) {
        if (collision.tag.Equals("Player")) {
            Debug.Log("LockPressed");
            OnLockPress?.Invoke(this);
        }
    }

    public void Unlock() {
        GetComponent<MeshRenderer>().material = unlockMat;
    }

    public void Lock() {
        GetComponent<MeshRenderer>().material = lockMat;
    }

    public void Highlight() {
        GetComponent<MeshRenderer>().material = highlightMat;
    }
}
