using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;

    public static GameManager Instance {
        get {
            if (_instance == null) {
                _instance = GameObject.FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }

    public UnityEvent onLevelCleared { get; private set; } = new UnityEvent();

    public bool isLevelCleared { get; private set; }

    public void SetLevelCleared () {
        if (!isLevelCleared) {
            isLevelCleared = true;
            onLevelCleared.Invoke();

            Debug.Log("level cleared");
        }
    }

}