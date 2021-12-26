using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Unit : MonoBehaviour {
    private enum Task {
        Idle,
        MoveToResource,
        GatheringResource,
        MoveToStorage,
    }

    [Header("Resource Node")]

    [SerializeField] public ResourceType resourceType;
    public enum ResourceType {
        Wood,
        Gold
    }
    [SerializeField] private ResourceNode resourceNode;
    [SerializeField] private Transform storageNode;

    [Header("NavMeshAgent")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private bool isIdle;

    [SerializeField] private Action callbackFunc;

    [Header("Ability")]
    [SerializeField] private Task task;
    [SerializeField] private float resourceInventoryAmount;
    [SerializeField] public float gatherTimer;
    [SerializeField] public float gatherDelay;
    [SerializeField] private float stopDistOffset;
    [SerializeField] private TextMesh inventoryTextMesh;

    [Header("InfoButton")]
    [SerializeField] private Text unitNameText;
    [SerializeField] private Button button1;
    [SerializeField] private Button button2;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        gatherTimer = gatherDelay;
        isIdle = true;
        inventoryTextMesh = transform.Find("InventoryTextMesh").GetComponent<TextMesh>();
        UpdateInventoryText();

        button1 = GameObject.Find("Button1").GetComponent<Button>();
        button2 = GameObject.Find("Button2").GetComponent<Button>();
    }

    private void Update()
    {
        UpdateTask();
        CheckPath();
    }

    private void UpdateTask() {
        switch (task) {
            case Task.Idle:
                //Debug.Log("Idle");
                callbackFunc = null;
                if(resourceType == ResourceType.Wood)
                    resourceNode = GameManager.GetWoodNode_Static();
                if(resourceType == ResourceType.Gold)
                    resourceNode = GameManager.GetMineNode_Static();
                //get resource node position from GameManager
                //Debug.Log(resourceNode);
                if(resourceNode != null) {
                //set task from "Idle" to "MoveToResource"
                    task = Task.MoveToResource;
                }
                break;

            case Task.MoveToResource:
               //Debug.Log("MoveToResource");
                //check is Idle
                if(IsAvaible()) { //if idle == true
                    //Move to resource node and pass a funtion to execute after finish
                    if(resourceNode != null) {
                        MoveTo(resourceNode.GetPosition(), () => {
                            //after arrived resource node
                            //set task from "MoveToResource" to "GatheringResource"
                            task = Task.GatheringResource;
                            //Debug.Log(task);
                        });
                    } else {
                        task = Task.Idle;
                    }
                }
                break;

            case Task.MoveToStorage:
                //Debug.Log("MoveToStorage");
                if(IsAvaible()) {  //if idle == true
                    //Move to storage node and pass a funtion to execute after finish
                    MoveTo(storageNode.position, () => { 
                        //after arrived resource node
                        if(resourceInventoryAmount > 0) { //if hold any resource
                            if(resourceType == ResourceType.Gold) {
                                GameResources.AddGoldAmount((int)resourceInventoryAmount);
                                Debug.Log(GameResources.GetGoldAmount());
                                GameManager.IncreaseGold_Static((int)resourceInventoryAmount); //increase gold in GameManager
                                resourceInventoryAmount = 0; //set resouce in inventory to zero
                            }
                            if(resourceType == ResourceType.Wood) {
                                GameResources.AddWoodAmount((int)resourceInventoryAmount);
                                Debug.Log(GameResources.GetWoodAmount());
                                GameManager.IncreaseWood_Static((int)resourceInventoryAmount); //increase gold in GameManager
                                resourceInventoryAmount = 0; //set resouce in inventory to zero
                            }
                        }
                        //set task from "MoveToStorage" to "Idle"
                        task = Task.Idle;
                    });
                }
                break;

            case Task.GatheringResource:
                //Debug.Log("Task.GatheringResource");
                if(IsAvaible()) { //if avaible
                    if(resourceInventoryAmount > 3 ||   resourceNode.resourceAmount <= 0) { //if hold any resource
                        storageNode = GameManager.GetStorageNode_Static(); //Get storage node from GameManager
                        //
                        MoveTo(storageNode.position, null);
                        task = Task.MoveToStorage;
                        agent.ResetPath();
                        Debug.Log(task);
                        // 
                    }
                    else { //if unavaible
                        if(gatherTimer > 0) { //if gathering
                            //Debug.Log("Gathering"); 
                            gatherTimer -= Time.deltaTime; //decrease gathering time over time
                        }
                        else if (gatherTimer <= 0){
                            //Debug.Log("Gathered : " + IsAvaible());
                            gatherTimer = gatherDelay; //reset gathering timer
                            resourceNode.GrabResource();
                            resourceInventoryAmount++; //increase resource in inventory
                            UpdateInventoryText();
                        }
                    }};
                break;
        }
    }

    private void MoveTo(Vector3 destination, Action OnArrived) {
        //Debug.Log("MoveTo was called");
        agent.SetDestination(destination);
        callbackFunc = OnArrived;
    }

    private bool IsEndPath()
    {
        if (agent.remainingDistance <= agent.stoppingDistance + stopDistOffset)
        {
            return true;
        }
        return false;
    }

    private bool IsAvaible()
    {
        if(IsEndPath() && isIdle) {
            // isIdle = false;
            // isEndPath = false;
            return true;
        }
        else 
            return false;
    }

    private void CheckPath()
    {
        //Debug.Log(agent.remainingDistance);
        if(agent.hasPath && agent.remainingDistance <= agent.stoppingDistance) {
            if(callbackFunc!=null)
            {
                callbackFunc();
            }
        }
    }

    private void UpdateInventoryText() {
        if(resourceInventoryAmount > 0) {
            inventoryTextMesh.text = "" + resourceInventoryAmount;
        } else {
            inventoryTextMesh.text = "";
        }
    }

    public void SetGoldNode() {
        resourceNode = GameManager.GetMineNode_Static();
        resourceInventoryAmount = 0;
        resourceType = ResourceType.Gold;
    }
    public void SetWoodNode() {
        resourceNode = GameManager.GetWoodNode_Static();
        resourceInventoryAmount = 0;
        resourceType = ResourceType.Gold;
    }

    private void OnMouseDown()  {
        Debug.Log("He clicked me");
        unitNameText = GameObject.Find("UnitName").GetComponent<Text>();
        unitNameText.text = gameObject.name;
        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();
        button1.onClick.AddListener(SetGoldNode);
        button1.transform.Find("Text").GetComponent<Text>().text = "Mining";
        button2.onClick.AddListener(SetWoodNode);
        button2.transform.Find("Text").GetComponent<Text>().text = "Wood Cutting";
    }
}