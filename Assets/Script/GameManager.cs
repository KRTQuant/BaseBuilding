using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField] private Unit unit;
    [SerializeField] private Transform buildingNode;
    [SerializeField] private Transform storageNode;

    [SerializeField] private int goldStock;

    [SerializeField] private List<ResourceNode> mineNode;
    [SerializeField] private List<ResourceNode> tempMineNode;

    private void Awake()
    {
        instance = this;
        unit = GetComponent<Unit>();

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
}