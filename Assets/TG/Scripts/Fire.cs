using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Fire : MonoBehaviour
{
    float currentTime;
    public float existTime;
    public float blinkRangeMin = 0.1f;
    public float blinkRangeMax = 3f;
    Light light = null;



    // Start is called before the first frame update
    void Start()
    {
        light = GetComponentInChildren<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        light.intensity = Random.Range(blinkRangeMin, blinkRangeMax);

        currentTime += Time.deltaTime;
        if (currentTime > existTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       
    }
}
