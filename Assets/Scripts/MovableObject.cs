using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour, InteractableObject {

    private HandController heldBy;

    public bool bindX = false;
    public float minX = 0f;
    public float maxX = 0f;
    public bool bindY = false;
    public float minY = 0f;
    public float maxY = 0f;

    // Object picked up, listen to hand for movement
    public void Grabbed(HandController hand) {
        if (heldBy != null) {
            Released(heldBy);
        }

        heldBy = hand;
        hand.OnUpdatePosition += Move;
    }

    // Object was released, stop listening to hand for movement
    public void Released(HandController hand) {
        if (hand == heldBy) {
            heldBy.OnUpdatePosition -= Move;
            heldBy = null;
        }
    }

    // Move object to position, if it is within the binding values of object
    public void Move(Vector3 pos, Vector3 rot, bool onBoard) {
        if (onBoard) {
            transform.position = pos;
            Vector3 localPos = transform.localPosition;
            if (bindX) {
                if (localPos.x > maxX)
                    localPos.x = maxX;
                else if (localPos.x < minX)
                    localPos.x = minX;
            }
            if (bindY) {
                if (localPos.y > maxY)
                    localPos.y = maxY;
                else if (localPos.y < minY)
                    localPos.y = minY;
            }
            if (localPos != transform.localPosition)
                transform.localPosition = localPos;
        }
    }
}
