using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class HouseScript : MonoBehaviour
{
    [Header("Parameter")]
    [SerializeField] private int botAmount;
    [SerializeField] private int maxBotAmount;
    [SerializeField] private int buildingLevel;
    [SerializeField] private int maxBuildingLevel;
    [SerializeField] private List<GameObject> unitList;

    [SerializeField] private Transform spawnpoint;
    [SerializeField] private GameObject unitPrefab;

    [SerializeField] private int botWoodCost;
    [SerializeField] private int botGoldCost;

    [SerializeField] private int upgradeGoldCost;
    [SerializeField] private int upgradeWoodCost;


    [Header("User Interface related")]
    [SerializeField] private RectTransform buildingUI;
    [SerializeField] private Text buildingName;
    [SerializeField] private RectTransform upgradeBuildingPanel;
    [SerializeField] private RectTransform createUnitPanel;
    
    private void Awake() {
        buildingUI = GameObject.Find("BuildingUI").GetComponent<RectTransform>();
        buildingName = buildingUI.transform.Find("BuildingNameText").GetComponent<Text>();
        upgradeBuildingPanel = buildingUI.transform.Find("UpgradeBuildingPanel").GetComponent<RectTransform>();
        createUnitPanel = buildingUI.transform.Find("CreateUnitPanel").GetComponent<RectTransform>();
        if(buildingName == null || upgradeBuildingPanel == null || createUnitPanel == null) {
            Debug.LogError("buildingName, upgradeBuilding or createUnitPanel is missing");
        }
        spawnpoint = transform.Find("SpawnPoint");
        ProduceUnit(1);
    }

    private void OnMouseDown() {
        //find building UI
        if(buildingUI != null) {
            //display building name
            buildingName.text = gameObject.name;
            //display upgrade panel
            buildingUI.gameObject.SetActive(true);
            //access building level text
            Text levelText = upgradeBuildingPanel.transform.Find("LevelText").GetComponent<Text>();
            Text buildingText = upgradeBuildingPanel.transform.Find("BuildingText").GetComponent<Text>();
            levelText.text = GetCurrentBuildingLevel() + " / " + GetMaxBuildingLevel();
            buildingText.fontSize = 40;
            buildingText.text = "Building";
            Button upgradeButton = upgradeBuildingPanel.transform.Find("UpgradeButton").GetComponent<Button>();
            upgradeBuildingPanel.gameObject.SetActive(true);
            upgradeButton.onClick.RemoveAllListeners();

            upgradeButton.onClick.AddListener(UpgradeBuilding);
            //display produce bot panel
            Text botAmountText = createUnitPanel.transform.Find("BotAmountText").GetComponent<Text>();
            botAmountText.text = GetCurrentBotAmount() + " / " + GetMaxBotAmount();
            Button createUnitButton = createUnitPanel.transform.Find("CreateUnitButton").GetComponent<Button>();
            createUnitButton.transform.parent.gameObject.SetActive(true);
            createUnitButton.onClick.RemoveAllListeners();
            createUnitButton.onClick.AddListener(ProduceUnit);
            //display create unit button
        }
    }

    public int GetCurrentBuildingLevel() {
        return buildingLevel;
    }

    public int GetMaxBuildingLevel() {
        return maxBuildingLevel;
    }

    public int GetCurrentBotAmount() {
        return botAmount;
    }

    public int GetMaxBotAmount() {
        return maxBotAmount;
    }

    public void UpgradeBuilding() {
        if(buildingLevel < maxBuildingLevel){
            //increase level
            buildingLevel++;
            maxBotAmount++;
            Text levelText = upgradeBuildingPanel.transform.Find("LevelText").GetComponent<Text>();
            levelText.text = GetCurrentBuildingLevel() + " / " + GetMaxBuildingLevel();
            //increase unit from this building stats
            foreach(var unit in unitList) {
                //increase movement speed
                unit.GetComponent<NavMeshAgent>().speed += 0.5f;
                //decrease mining time
                unit.GetComponent<Unit>().gatherDelay -= 0.2f;
                unit.GetComponent<Unit>().gatherTimer = unit.GetComponent<Unit>().gatherDelay;
            }
        }
    }

    public void ProduceUnit(int amount) {
        if(botAmount < maxBotAmount){
            for(int i = 0; i < amount; i++) {
                GameObject unit = Instantiate<GameObject>(unitPrefab, spawnpoint.position, Quaternion.identity);
                unitList.Add(unit);

                unit.GetComponent<NavMeshAgent>().speed += 0.5f * buildingLevel;
                //decrease mining time
                unit.GetComponent<Unit>().gatherDelay -= 0.2f * buildingLevel;
                unit.GetComponent<Unit>().gatherTimer = unit.GetComponent<Unit>().gatherDelay;
                botAmount++;
                Text botAmountText = createUnitPanel.transform.Find("BotAmountText").GetComponent<Text>();
                botAmountText.text = GetCurrentBotAmount() + " / " + GetMaxBotAmount();
            }
        }
    }

        public void ProduceUnit() {
        if(botAmount < maxBotAmount){
            GameObject unit = Instantiate<GameObject>(unitPrefab, spawnpoint.position, Quaternion.identity);
            unitList.Add(unit);

            unit.GetComponent<NavMeshAgent>().speed += 0.5f * buildingLevel;
            //decrease mining time
            unit.GetComponent<Unit>().gatherDelay -= 0.2f * buildingLevel;
            unit.GetComponent<Unit>().gatherTimer = unit.GetComponent<Unit>().gatherDelay;
            botAmount++;
            Text botAmountText = createUnitPanel.transform.Find("BotAmountText").GetComponent<Text>();
            botAmountText.text = GetCurrentBotAmount() + " / " + GetMaxBotAmount();
        }
    }
}
