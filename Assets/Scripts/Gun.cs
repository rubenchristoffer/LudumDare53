using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public Transform aimPoint;
    public GameObject projectilePrefab;

    private Entity entity;

    // Start is called before the first frame update
    void Start()
    {
        entity = GetComponentInParent<Entity>();
    }

    // Update is called once per frame
    void Update()
    {
        if (entity.isDead) {
            return;
        }

        if (Input.GetMouseButtonDown(0)) {
            Instantiate(projectilePrefab, aimPoint.transform.position, aimPoint.transform.rotation);
        }
    }
}
