using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HandController : MonoBehaviour {

    int boardMask = 1 << 8;
    int rotatorMask = 1 << 9;
    public Selector handSelector;
    bool onBoard = false;

    public SteamVR_Action_Boolean grab;
    public SteamVR_Input_Sources handType;

    Vector3 prevRotation = Vector3.zero;

    GameObject heldObject = null;

    public delegate void UpdatePosition(Vector3 selPos, Vector3 rotChange, bool onBoard);
    public event UpdatePosition OnUpdatePosition;

    private void Start() {
        grab.AddOnStateDownListener(TriggerDown, handType);
        grab.AddOnStateUpListener(TriggerUp, handType);
    }

    private void OnDestroy() {
        grab.RemoveOnStateDownListener(TriggerDown, handType);
        grab.RemoveOnStateUpListener(TriggerUp, handType);
    }

    private void Update() {
        RaycastHit hit;
        onBoard = false;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, boardMask)) {
            handSelector.transform.position = hit.point;
            onBoard = true;
        } else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, rotatorMask)) {
            handSelector.transform.position = hit.transform.position;
        } else {
            handSelector.transform.position = transform.position + transform.forward * 8f;
        }
    }

    private void FixedUpdate() {

        Vector3 rotation = transform.rotation.eulerAngles;
        Vector3 rotationChange = rotation - prevRotation;
        prevRotation = rotation;

        if (heldObject != null) {
            OnUpdatePosition?.Invoke(handSelector.transform.position, rotationChange, onBoard);
        }
    }

    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) {
        if (fromSource == handType) {
            GameObject highlight = handSelector.GetHighlighted();
            highlight.GetComponent<InteractableObject>().Grabbed(this);
            heldObject = highlight;
        }
    }

    public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) {
        if (fromSource == handType) {
            if (heldObject != null) {
                heldObject.GetComponent<InteractableObject>().Released(this);
                heldObject = null;
            }
        }
    }
}
