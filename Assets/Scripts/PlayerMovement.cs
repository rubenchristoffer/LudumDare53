using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 8f;
    public LayerMask groundMask;
    public Animator animator;

    private Rigidbody _rigidbody;
    private Camera mainCamera;
    private Entity entity;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        entity = GetComponent<Entity>();
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

        GameManager.Instance.onLevelCleared.AddListener(() => {
            foreach (var collider in GetComponentsInChildren<Collider>()) {
                collider.enabled = false;
            }

            _rigidbody.useGravity = false;
            _rigidbody.velocity = Vector3.zero;
        });

        entity.onEntityDie.AddListener(() => {
            GameManager.Instance.SetLevelFailed();
            _rigidbody.freezeRotation = false;

            var levelClearedUI = FindObjectOfType<LevelClearedUI>(true);
            levelClearedUI.gameObject.SetActive(true);
            levelClearedUI.shown = true;
        });
    }

    void Update()
    {   
        if (GameManager.Instance.isLevelCleared) {
            animator.SetFloat("WalkSpeedNormalized", Mathf.Min(_rigidbody.velocity.magnitude / speed, 1f));
            return;
        }

        if (entity.isDead) {
            animator.SetFloat("WalkSpeedNormalized", 0f);
            return;
        }

        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask)) {
            var direction = hitInfo.point - transform.position;

            transform.forward = direction;
        }

        Vector3 rot = transform.eulerAngles;
        rot.x = 0f;
        rot.z = 0f;

        transform.eulerAngles = rot;

        animator.SetFloat("WalkSpeedNormalized", Mathf.Min(_rigidbody.velocity.magnitude / speed, 1f));
    }

    void FixedUpdate()
    {
        if (entity.isDead || GameManager.Instance.isLevelCleared) {
            return;
        }

        _rigidbody.velocity = new Vector3(-Input.GetAxis("Vertical"), 0f, Input.GetAxis("Horizontal")) * speed;
    }
}
