using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorageScript : MonoBehaviour
{
    private RectTransform buildingUI;
    private RectTransform housePanel;
    private RectTransform blankPanel;
    private Text buildingName;
    private Text buildingBPName;
    [SerializeField] private int houseWoodCost;
    [SerializeField] private int houseGoldCost;
    private Text costText;

    private Button buyButton;
    [SerializeField] private GameObject houseBlueprintPrefab;

    private void Awake() {
        buildingUI = GameObject.Find("BuildingUI").GetComponent<RectTransform>();
        housePanel = buildingUI.transform.Find("UpgradeBuildingPanel").GetComponent<RectTransform>();
        buildingName = buildingUI.transform.Find("BuildingNameText").GetComponent<Text>();
        buildingBPName = housePanel.transform.Find("BuildingText").GetComponent<Text>();
        costText = housePanel.transform.Find("LevelText").GetComponent<Text>();
        buyButton = housePanel.transform.Find("UpgradeButton").GetComponent<Button>();
    }
    private void OnMouseDown() {
        buildingName.text = gameObject.name;
        buildingBPName.text = "House";
        housePanel.gameObject.SetActive(true);
        costText.fontSize = 20;
        costText.text = "Gold: "+houseGoldCost.ToString() + " \n" + "Wood: " + houseWoodCost;
        buildingUI.transform.Find("CreateUnitPanel").gameObject.SetActive(false);
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(BuyHouse);
    }

    private void BuyHouse() {
        Debug.Log("Buy house was called");
        Debug.Log(GameResources.GetGoldAmount() + " " + GameResources.GetWoodAmount());
        if(GameResources.GetGoldAmount() >= houseGoldCost && GameResources.GetWoodAmount() >= houseWoodCost) {
            Debug.Log("Build a house");
            GameResources.DecreaseGoldAmount(houseGoldCost);
            GameResources.DecreaseWoodAmount(houseWoodCost);
            Instantiate(houseBlueprintPrefab);
        }
    }
}
