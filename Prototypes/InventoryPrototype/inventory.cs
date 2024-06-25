using System;
using System.Collections.Generic;
using UnityEngine;

/*
    This class is a structure to represent the player's inventory.
    The component needs to be added to the actual player object.

    @author
        Timo Skrobanek
*/
public class Inventory : MonoBehaviour
{
    private List<Item> items;

    public void Start(){
        items = new List<Item>();
    }  

    /*
        Add items to the existing inventory
        @param item
            The actual item to add
    */
    public void AddItem(Item item){
        //TODO visualize to the player
        items.Add(item);
    } 

    /*
        Remove items from inventory
        @param
            the item to remove from inventory
    */
    public void RemoveItem(Item item){
        items.Remove(item);
    }

    /*
        Clear the inventory, meaning: Remove every
        item.
    */
    public void ClearInventory(){
        items.Clear();
    }

    /*
        Return the inventory
        @return
            list of all items
    */
    public List<Item> getAllItems(){
        return items;
    }

    public void LoadAll(){
        //TODO connect to save game
    }

    public int GetAmount(){
        return items.Count;
    }
}
