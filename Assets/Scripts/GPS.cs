using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPS : Item
{

    public PlayerInventory playerInventory;

    protected override void OnPickup(Collider collider)
    {
        playerInventory.hasFoundGPS = true;
    }

}
