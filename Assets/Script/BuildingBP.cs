using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBP : MonoBehaviour
{
    RaycastHit hit;
    Vector3 movePoint;
    public GameObject building;
    Vector3 spawnPos;

    [SerializeField] private List<Material> buildingMaterial;
    // order 0: Transparent Material
    // order 1: Failed Material

    private void Start() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Renderer renderer = GetComponentInChildren<MeshRenderer>();
        renderer.material = buildingMaterial[0];

        if(Physics.Raycast(ray, out hit, 50000.0f, (1 << 7))) {
            transform.position = hit.point;
        }

        spawnPos = transform.GetChild(0).transform.position;
    }

    private void Update() {
        Debug.Log("Ray is casting");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, 50000.0f, (1 << 7))) {
            transform.position = hit.point;
        }

        if(Input.GetMouseButton(0)) {
            spawnPos = transform.GetChild(0).transform.position;
            spawnPos = new Vector3(spawnPos.x, -6.4f, spawnPos.z);
            Instantiate(building, spawnPos, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay(Collision other) {
        //collide with any object
        if(other.gameObject != null) {
            GetComponentInChildren<Renderer>().material = buildingMaterial[1];
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if(other.gameObject != null) {
            GetComponentInChildren<Renderer>().material = buildingMaterial[0];
        }
    }
}
