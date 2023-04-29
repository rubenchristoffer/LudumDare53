using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public float x = 14f;
    public float y = 24f;
    public float smoothFactor = 0.1f;

    private Transform _transformToFollow;
    private Vector3 targetPosition;
    private Vector3 targetPositionV;

    void Awake()
    {
        _transformToFollow = GameObject.FindWithTag("Player").transform;
    }

    void Update() 
    {
        targetPosition = new Vector3(_transformToFollow.position.x + x, _transformToFollow.position.y + y, _transformToFollow.position.z);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref targetPositionV, smoothFactor);
    }

}
