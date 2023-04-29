using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{

    public AudioSource knockAudio;
    public bool correctHouse;
    public bool knocked;
    public Transform door;
    public Camera cutsceneCamera;
    public Transform playerStandPosition;

    private HUD hud;
    private Transform _player;
    private float cameraSwitchTimer;

    void Awake()
    {
        hud = GameObject.FindAnyObjectByType<HUD>();
        _player = GameObject.FindWithTag("Player").transform;
    }

    IEnumerator Cutscene()
    {
        yield return new WaitForSeconds(3.1f);

        GameObject.FindWithTag("MainCamera").SetActive(false);
        cutsceneCamera.gameObject.SetActive(true);
        _player.transform.position = playerStandPosition.position;
        _player.transform.rotation = playerStandPosition.rotation;

        hud.gameObject.SetActive(false);

        foreach (var enemy in GameObject.FindObjectsOfType<Enemy>())
        {
            enemy.gameObject.SetActive(false);
        }

        var rigidbody = _player.GetComponent<Rigidbody>();

        yield return new WaitForSeconds(2f);

        rigidbody.velocity = playerStandPosition.forward * 2f;
    }

    void LateUpdate()
    {
        if (knocked)
        {
            return;
        }

        float distance = Vector3.Distance(door.position, _player.position);

        if (distance <= 5f)
        {
            hud.displayKnockText = true;

            if (Input.GetButton("Interact"))
            {
                knockAudio.Play();
                knocked = true;

                if (correctHouse)
                {
                    StartCoroutine(Cutscene());
                    hud.fadePanelAnimator.SetTrigger("Start");
                    GameManager.Instance.SetLevelCleared();
                }
                else
                {
                    hud.nobodyRespondsText.GetComponent<Animator>().SetTrigger("Active");
                }
            }
        }
    }

}
