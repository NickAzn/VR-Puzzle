﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardRotator : MonoBehaviour, InteractableObject {

    private HandController heldBy;
    public Transform board;
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
        transform.position = board.transform.position + board.transform.right * distance;
    }

    public void Rotate(Vector3 pos, Vector3 rot, bool onBoard) {
        pos.z = board.transform.position.z;
        float angle = Vector3.Angle(pos - board.transform.position, Vector3.right);
        if (pos.y < board.transform.position.y)
            angle *= -1;
        board.transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.position = board.transform.position + board.transform.right * distance;
        transform.rotation = board.rotation;
    }

}
