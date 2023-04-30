using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInSeconds : MonoBehaviour
{

    public float seconds = 60f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyInSecondsCoroutine());
    }

    IEnumerator DestroyInSecondsCoroutine () {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
