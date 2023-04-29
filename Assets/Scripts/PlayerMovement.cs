using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 8f;
    public LayerMask groundMask;

    private Rigidbody _rigidbody;
    private Camera mainCamera;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask)) {
            var direction = hitInfo.point - transform.position;

            transform.forward = direction;
        }

        Vector3 rot = transform.eulerAngles;
        rot.x = 0f;
        rot.z = 0f;

        transform.eulerAngles = rot;
    }

    void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(-Input.GetAxis("Vertical"), 0f, Input.GetAxis("Horizontal")) * speed;
    }
}
