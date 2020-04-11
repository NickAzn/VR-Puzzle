using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour {

    bool held = false;

    public void Grabbed() {
        held = true;
    }

    public void Released() {
        held = false;
    }
}
