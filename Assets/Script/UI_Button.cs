using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Button : MonoBehaviour
{
    /* for using require: 
        BuildingBP within scene
    */

    public GameObject blueprint;
    public void CallPrefab() {
        Instantiate(blueprint);
    }
}
