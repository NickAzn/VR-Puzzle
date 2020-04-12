using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InteractableObject {

    void Grabbed(HandController hand);
    void Released(HandController hand);
}
