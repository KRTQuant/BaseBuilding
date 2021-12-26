using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameResources
{
    public static event EventHandler OnGoldAmountChanged;
    public static event EventHandler OnWoodAmountChanged;

    private static int goldAmount;
    private static int woodAmount;

    public static void AddGoldAmount(int amount) {
        goldAmount += amount;
        if(OnGoldAmountChanged != null)
            OnGoldAmountChanged(null, EventArgs.Empty);
    }
    
    public static int GetGoldAmount() {
        return goldAmount;
    }    
    
    public static void AddWoodAmount(int amount) {
        woodAmount += amount;
        if(OnWoodAmountChanged != null)
            OnWoodAmountChanged(null, EventArgs.Empty);
    }
    
    public static int GetWoodAmount() {
        return woodAmount;
    }

    public static void DecreaseGoldAmount(int amount) {
        goldAmount -= amount;
        if(OnGoldAmountChanged != null)
            OnGoldAmountChanged(null, EventArgs.Empty);
    }

    public static void DecreaseWoodAmount(int amount) {
        woodAmount -= amount;
        if(OnWoodAmountChanged != null)
            OnWoodAmountChanged(null, EventArgs.Empty);
    }
}
