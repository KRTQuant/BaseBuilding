using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBP : MonoBehaviour
{
    RaycastHit hit;
    Vector3 movePoint;
    public GameObject building;
    Vector3 spawnPos;

    List<Material> buildingMaterial;

    private void Start() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Renderer renderer = GetComponent<Renderer>();
        renderer.material = buildingMaterial[1];

        if(Physics.Raycast(ray, out hit, 50000.0f, (1 << 0))) {
            transform.position = hit.point;
        }

        spawnPos = transform.GetChild(0).transform.position;
    }

    private void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, 50000.0f, (1 << 0))) {
            transform.position = hit.point;
        }

        if(Input.GetMouseButton(0)) {
            spawnPos = transform.GetChild(0).transform.position;
            Instantiate(building, spawnPos, transform.rotation);
            Destroy(gameObject);
        }
    }
}
