using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeftDestroy : MonoBehaviour
{

    public float timeForDestruction;
    void Start()
    {
        StartCoroutine(DestroySeft(timeForDestruction));
    }

    private IEnumerator DestroySeft(float timeForDestruction)
    {
        yield return new WaitForSeconds(timeForDestruction);

        Destroy(gameObject);
    }

    // Update is called once per frame
    
}
