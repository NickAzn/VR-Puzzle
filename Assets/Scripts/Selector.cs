using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour {

    public Material highlightMaterial;

    GameObject highlightedObject = null;
    Material originalMaterial = null;

    private void OnTriggerEnter(Collider other) {
        if (other.tag.Equals("Interactable")) {
            if (highlightedObject == null) {
                highlightedObject = other.gameObject;
                Renderer objRenderer = highlightedObject.GetComponent<Renderer>();
                originalMaterial = objRenderer.material;
                objRenderer.material = highlightMaterial;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject == highlightedObject) {
            highlightedObject.GetComponent<Renderer>().material = originalMaterial;
            highlightedObject = null;
        }
    }

    public GameObject GetHighlighted() {
        return highlightedObject;
    }

}
