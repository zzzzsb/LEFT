using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamParticle : MonoBehaviour
{
    public float fireTime = 2;
    float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.name.Contains("Player"))
        {
            other.GetComponent<PlayerCount>().steamCount += 1;

            if(other.GetComponent<PlayerCount>().steamCount == 2)
            {
                other.GetComponent<PlayerDie>().Die();
            }
        }
        
    }

    public void Replay(ParticleSystem particle)
    {
        particle.Stop();
        particle.Play();
    }

}
