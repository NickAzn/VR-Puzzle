using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HandController : MonoBehaviour {

    int layerMask = 1 << 8;
    public Selector handSelector;

    public SteamVR_Action_Boolean grab;
    public SteamVR_Input_Sources handType;

    GameObject heldObject = null;

    private void Start() {
        grab.AddOnStateDownListener(TriggerDown, handType);
        grab.AddOnStateUpListener(TriggerUp, handType);
    }

    private void Update() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10f, layerMask)) {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            handSelector.transform.position = hit.point;
            Debug.Log("RaycastHit");
        }

        if (heldObject != null) {
            heldObject.transform.position = handSelector.transform.position;
        }
    }

    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) {
        GameObject highlight = handSelector.GetHighlighted();
        heldObject = highlight;
    }

    public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) {
        heldObject = null;
    }
}
