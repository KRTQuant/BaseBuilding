using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour {
    [SerializeField] private Transform resourceNodeTransform;

    [SerializeField] public int resourceAmount;
    [SerializeField] private BoxCollider boxCollider;

    private enum ResourceType {
        Wood,
        Gold
    }

    private void Awake() {
        resourceNodeTransform = this.transform;
        boxCollider = GetComponent<BoxCollider>();
    }

    public Vector3 GetPosition() {
        return resourceNodeTransform.position;
    }

    public void GrabResource() {
        //Debug.Log(resourceAmount);
        resourceAmount -= 1;
        if (resourceAmount <= 0) {
            resourceNodeTransform.gameObject.SetActive(false);
        }
    }

    public bool HasResource() {
        return resourceAmount > 0;
    }

    private void OnMouseDown() {
        Debug.Log(resourceNodeTransform.name);
    }
}