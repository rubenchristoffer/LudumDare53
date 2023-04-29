using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public float x = 14f;
    public float y = 24f;

    private Transform _transformToFollow;

    void Awake()
    {
        _transformToFollow = GameObject.FindWithTag("Player").transform;
    }

    void Update() 
    {
        transform.position = new Vector3(_transformToFollow.position.x + x, _transformToFollow.position.y + y, _transformToFollow.position.z);
    }

}
