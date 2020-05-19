using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalLock : MonoBehaviour {

    public Material lockMat;
    public Material unlockMat;
    public Material highlightMat;
    public delegate void LockPress(GoalLock gl);
    public event LockPress OnLockPress;
    public AudioSource sfxSource;
    public AudioClip unlockSfx;
    public AudioClip failSfx;

    private void OnTriggerEnter(Collider collision) {
        if (collision.tag.Equals("Player")) {
            Debug.Log("LockPressed");
            OnLockPress?.Invoke(this);
        }
    }

    public void FailSound() {
        sfxSource.clip = failSfx;
        sfxSource.Play();
    }

    public void Unlock() {
        GetComponent<MeshRenderer>().material = unlockMat;
        sfxSource.clip = unlockSfx;
        sfxSource.Play();
    }

    public void Lock() {
        GetComponent<MeshRenderer>().material = lockMat;
    }

    public void Highlight() {
        GetComponent<MeshRenderer>().material = highlightMat;
    }
}
