using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardRotator : MonoBehaviour, InteractableObject {

    private HandController heldBy;
    public Transform board;
    public int direction = 0;
    public float distance = 0.5f;

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

    public void LevelReset() {
        if (heldBy != null) {
            heldBy.OnUpdatePosition -= Rotate;
            heldBy = null;
        }
        UpdatePos();
    }

    public void Rotate(Vector3 pos, Vector3 rot, bool onBoard) {
        pos.z = board.transform.position.z;
        float angle = 0;
        if (direction == 0)
            angle = Vector3.SignedAngle(pos - board.transform.position, Vector3.right, -Vector3.forward);
        else if (direction == 1)
            angle = Vector3.SignedAngle(pos - board.transform.position, Vector3.up, -Vector3.forward);
        else if (direction == 2)
            angle = Vector3.SignedAngle(pos - board.transform.position, -Vector3.right, -Vector3.forward);
        else
            angle = Vector3.SignedAngle(pos - board.transform.position, -Vector3.up, -Vector3.forward);
        board.transform.rotation = Quaternion.Euler(0, 0, angle);
        GameController.instance.UpdateRotators();
    }

    public void UpdatePos() {
        transform.rotation = board.rotation;
        if (direction == 0)
            transform.position = board.transform.position + board.transform.right * distance;
        else if (direction == 1)
            transform.position = board.transform.position + board.transform.up * distance;
        else if (direction == 2)
            transform.position = board.transform.position - board.transform.right * distance;
        else
            transform.position = board.transform.position - board.transform.up * distance;
    }

}
