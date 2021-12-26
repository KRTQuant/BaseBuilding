using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Keypad1)) {
            IncreaseWood();
        }
        if(Input.GetKeyDown(KeyCode.Keypad2)) {
            IncreaseGold();
        }
    }

    private void IncreaseWood() {
        GameResources.AddWoodAmount(50);
    }
    private void IncreaseGold() {
        GameResources.AddGoldAmount(50);
    }
}
