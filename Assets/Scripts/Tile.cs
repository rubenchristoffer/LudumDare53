using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    public void PopulateTile (GameObject obj) {
        Instantiate<GameObject>(obj, transform);
    }

}
