using UnityEngine;
interface Interactable
{
    //referenced method to call when object is hit
    public void Interact(GameObject obj);
    public void IsAccessable();
}
