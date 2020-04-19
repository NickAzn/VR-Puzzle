﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalLock : MonoBehaviour {

    public Material lockMat;
    public Material unlockMat;
    public delegate void LockPress(GoalLock gl);
    public event LockPress OnLockPress;

    private void OnTriggerEnter(Collider collision) {
        if (collision.tag.Equals("Player")) {
            OnLockPress?.Invoke(this);
        }
    }

    public void Unlock() {
        GetComponent<MeshRenderer>().material = unlockMat;
    }

    public void Lock() {
        GetComponent<MeshRenderer>().material = lockMat;
    }

}
