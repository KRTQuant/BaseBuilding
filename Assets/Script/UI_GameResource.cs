using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameResource : MonoBehaviour
{
    private void Awake() {
        GameResources.OnGoldAmountChanged += delegate (object sender, EventArgs e) {
            UpdateResourceTextObject();
        };
        GameResources.OnWoodAmountChanged += delegate (object sender, EventArgs e) {
            UpdateResourceTextObject();
        };
        UpdateResourceTextObject();
    }
    
    private void UpdateResourceTextObject() {
        GameObject.Find("GoldAmount").GetComponent<Text>().text = "Gold: " + GameResources.GetGoldAmount();
        GameObject.Find("WoodAmount").GetComponent<Text>().text = "Wood: " + GameResources.GetWoodAmount();
    }

}
