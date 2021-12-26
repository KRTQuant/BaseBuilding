using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    [SerializeField] private int woodGoal;
    [SerializeField] private int goldGoal;
    [SerializeField] private Unit unit;
    [SerializeField] private Transform buildingNode;
    [SerializeField] private Transform storageNode;

    [SerializeField] private int goldStock;
    [SerializeField] private int woodStock;

    [SerializeField] private List<ResourceNode> mineNode;
    [SerializeField] private List<ResourceNode> tempMineNode;
    [SerializeField] private List<ResourceNode> woodNode;
    [SerializeField] private List<ResourceNode> tempWoodNode;

    [SerializeField] private Transform winPanel;
    [SerializeField] private bool winGame;

    private void Awake()
    {
        instance = this;
        unit = GetComponent<Unit>();
        winPanel = GameObject.Find("Canvas").transform.Find("WinPanel");
        winPanel.gameObject.SetActive(false);
    }

    private ResourceNode GetMineNode() {
        List<ResourceNode> tmpResourceNodeList = new List<ResourceNode>(mineNode);
        tempMineNode = tmpResourceNodeList;
        for(int i = 0; i < tmpResourceNodeList.Count; i++) {
            if(!tmpResourceNodeList[i].HasResource()) {
                tmpResourceNodeList.RemoveAt(i);
                i--;
            }
        }
        if(tmpResourceNodeList.Count > 0) {
            ResourceNode node = mineNode[UnityEngine.Random.Range(0, mineNode.Count)];
            Debug.Log(node.gameObject.name);
            return node;
        } else {
            return null;
        }
    }

    public static ResourceNode GetMineNode_Static() {
        return instance.GetMineNode();
    }

    private ResourceNode GetWoodNode() {
        List<ResourceNode> tmpWoodNode = new List<ResourceNode>(woodNode);
        tempWoodNode = tmpWoodNode;
        for(int i = 0; i < tmpWoodNode.Count; i++) {
            if(!tmpWoodNode[i].HasResource()) {
                tmpWoodNode.RemoveAt(i);
                i--;
            }
        }
        if(tmpWoodNode.Count > 0) {
            ResourceNode node = woodNode[UnityEngine.Random.Range(0, woodNode.Count)];
            Debug.Log(node.gameObject.name);
            return node;
        } else {
            return null;
        }
    }

    public static ResourceNode GetWoodNode_Static() {
        return instance.GetWoodNode();
    }
    public Transform GetStorage() {
        return storageNode;
    }

    public static Transform GetStorageNode_Static() {
        return instance.GetStorage();
    }

    public int IncreaseGold(int amount) {
        return goldStock += amount;
    }

    public static int IncreaseGold_Static(int amount) {
        return instance.IncreaseGold(amount);
    }

    public int IncreaseWood(int amount) {
        return woodStock += amount;
    }

    public static int IncreaseWood_Static(int amount) {
        return instance.IncreaseWood(amount);
    }

    private void Update() {
        int woodInStock = GameResources.GetWoodAmount();
        int goldInStock = GameResources.GetGoldAmount();

        if(woodInStock >= woodGoal && goldInStock >= goldGoal && winGame == false)  {
            Debug.Log("You win");
            FindObjectOfType<AudioManager>().Play("Victory");
            winPanel.gameObject.SetActive(true);
            winGame = true;
            StartCoroutine(DelayAndAction(ChangeScene));
        }
    }

    private IEnumerator DelayAndAction(Action func) {
        yield return new WaitForSeconds(3);
        func();
    }

    private void ChangeScene() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        FindObjectOfType<AudioManager>().Stop("Stage1");
        FindObjectOfType<AudioManager>().Play("MainMenu");
    }
}