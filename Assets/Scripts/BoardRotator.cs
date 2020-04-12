using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardRotator : MonoBehaviour, InteractableObject {

    private HandController heldBy;
    public Transform board;

    public void Grabbed(HandController hand) {
        if (heldBy != null) {
            Released(heldBy);
        }

        heldBy = hand;
        transform.rotation = board.rotation;
        hand.OnUpdatePosition += Rotate;
    }

    public void Released(HandController hand) {
        if (hand == heldBy) {
            heldBy.OnUpdatePosition -= Rotate;
            heldBy = null;
        }
    }

    public void Rotate(Vector3 pos, Vector3 rot, bool onBoard) {
        rot.x = 0;
        rot.y = 0;
        transform.Rotate(rot);
        board.rotation = transform.rotation;
    }

}
