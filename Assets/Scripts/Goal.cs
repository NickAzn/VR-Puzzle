using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour {

    public Material lockMat;
    public Material unlockMat;
    private bool locked;
    private int nextLock = 0;
    public GoalLock[] locks;
    public GameObject[] lockedObjects;

    private void OnEnable() {
        if (locks.Length > 0) {
            locked = true;
            GetComponent<MeshRenderer>().material = lockMat;
            foreach (GoalLock l in locks) {
                l.OnLockPress += LockPressed;
                if (l == locks[0])
                    l.Highlight();
            }
        } else {
            locked = false;
            GetComponent<MeshRenderer>().material = unlockMat;
        }
    }

    private void OnDisable() {
        foreach (GoalLock l in locks) {
            l.OnLockPress -= LockPressed;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag.Equals("Player") && !locked) {
            Debug.Log("You Win");
            other.gameObject.SetActive(false);
            GameController.instance.WinLevel();
        }
    }

    private void LockPressed(GoalLock gl) {
        Debug.Log("LockPressedGoal");
        if (!locked) {
            return;
        }
        if (locks[nextLock] == gl) {
            locks[nextLock].Unlock();
            if (lockedObjects.Length > nextLock) 
                lockedObjects[nextLock].SetActive(false);
            nextLock++;
            if (nextLock >= locks.Length) {
                locked = false;
                GetComponent<MeshRenderer>().material = unlockMat;
            } else {
                locks[nextLock].Highlight();
            }
        } else {
            if (nextLock > 0) {
                if (locks[nextLock - 1] == gl)
                    return;
            }
            for (int i = nextLock; i >= 0; i--) {
                locks[i].Lock();
                if (lockedObjects.Length > i)
                    lockedObjects[i].SetActive(true);
            }
            gl.FailSound();
            nextLock = 0;
            locks[nextLock].Highlight();
        }
    }

}
