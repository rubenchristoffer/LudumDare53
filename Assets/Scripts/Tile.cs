using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    public Vector3 populationRotation;

    public void PopulateTile (GameObject obj) {
        Instantiate<GameObject>(obj, transform.position, Quaternion.Euler(populationRotation), transform);
    }

}
