using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    //public AudioClip BGM;
    public AudioClip walking;
    public AudioClip crouching;
    public AudioClip die;

    AudioSource audioSource;

 

    // Start is called before the first frame update
    void Start()

    {

        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {



        if (GetComponent<PlayerMove>().applySpeed == GetComponent<PlayerMove>().walkSpeed)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.clip = walking;
            audioSource.Play();
        }
        else if (GetComponent<PlayerMove>().applySpeed == GetComponent<PlayerMove>().crouchSpeed)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.clip = crouching;
            audioSource.Play();
        }
        



    }

    public void Die()
    {
        audioSource.clip = die;
        audioSource.Play();
    }

    IEnumerator WalkingSoundCor()
    {
        if(audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        yield return null;
        print("11");
        audioSource.clip = walking;
        audioSource.Play();
    }
    IEnumerator crouchingSoundCor()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        yield return null;
        print("11");
        audioSource.clip = walking;
        audioSource.Play();
    }

}
