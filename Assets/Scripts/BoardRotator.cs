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
        hand.OnUpdatePosition += Rotate;
    }

    public void Released(HandController hand) {
        if (hand == heldBy) {
            heldBy.OnUpdatePosition -= Rotate;
            heldBy = null;
        }
    }

    public void Rotate(Vector3 pos, Vector3 rot, bool onBoard) {
        pos.z = board.transform.position.z;
        float angle = Vector3.Angle(pos - board.transform.position, Vector3.right);
        if (pos.y < board.transform.position.y)
            angle *= -1;
        board.transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.position = board.transform.position + board.transform.right * 5;
        transform.rotation = board.rotation;
    }

}
