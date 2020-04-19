using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

    public Material lockMat;
    public Material unlockMat;
    private bool locked;
    private int nextLock = 0;
    public GoalLock[] locks;

    private void Start() {
        if (locks.Length > 0) {
            locked = true;
            GetComponent<MeshRenderer>().material = lockMat;
            foreach (GoalLock l in locks) {
                l.OnLockPress += LockPressed;
            }
        } else {
            locked = false;
            GetComponent<MeshRenderer>().material = unlockMat;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag.Equals("Player") && !locked) {
            Debug.Log("You Win");
        }
    }

    private void LockPressed(GoalLock gl) {
        if (!locked) {
            return;
        }
        if (locks[nextLock] == gl) {
            locks[nextLock].Unlock();
            nextLock++;
            if (nextLock >= locks.Length) {
                locked = false;
                GetComponent<MeshRenderer>().material = unlockMat;
            }
        } else {
            for (int i = nextLock; i >= 0; i--) {
                locks[i].Lock();
            }
            nextLock = 0;
        }
    }

}
