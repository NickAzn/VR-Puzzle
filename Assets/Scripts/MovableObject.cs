using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour, InteractableObject {

    private HandController heldBy;

    public void Grabbed(HandController hand) {
        if (heldBy != null) {
            Released(heldBy);
        }

        heldBy = hand;
        hand.OnUpdatePosition += Move;
    }

    public void Released(HandController hand) {
        if (hand == heldBy) {
            heldBy.OnUpdatePosition -= Move;
            heldBy = null;
        }
    }

    public void Move(Vector3 pos, Vector3 rot, bool onBoard) {
        if (onBoard)
            transform.position = pos;
    }
}
