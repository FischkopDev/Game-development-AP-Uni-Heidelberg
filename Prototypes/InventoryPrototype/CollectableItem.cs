using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : Item, Interactable
{
    public Inventory inventory;

    public void interact(){
        if(Input.GetKeyDown(KeyCode.E)){
            inventory.AddItem(this);
        }
        else if(Input.GetKeyDown(KeyCode.R)){
            inventory.RemoveItem(this);
        }
    }
}
