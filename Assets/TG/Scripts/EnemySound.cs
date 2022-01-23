using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : MonoBehaviour
{

    public AudioClip idleSound1;
    public AudioClip idleSound2;
    public AudioClip idleSound3;
    public AudioClip idleSound4;
    public AudioClip idleSound5;
    public AudioClip attackSound;
    public AudioClip foundSound;
    public AudioClip foundSound2;


    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {


        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.5f;
        GetComponent<EnemySound>().IdleMoveSound();

        //audioSource.clip = idleSound1;
        //audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {



    }

    public void IdleMoveSound()
    {
        StopAllCoroutines();
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        StartCoroutine(IDleMoveSoundCor());

    }

    IEnumerator IDleMoveSoundCor()
    {
        while (true)
        {
            print("들어옴1");
            audioSource.clip = idleSound2;
            audioSource.Play();
            yield return new WaitForSeconds(4);
            print("들어옴2");
            audioSource.clip = idleSound3;
            audioSource.Play();
            yield return new WaitForSeconds(4);
            print("들어옴3");
            audioSource.clip = idleSound4;
            audioSource.Play();
            yield return new WaitForSeconds(4);
            print("들어옴4");
            audioSource.clip = idleSound5;
            audioSource.Play();
        }




    }





    public void FoundSound()
    {
        StopAllCoroutines();
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        StartCoroutine(FoundSoundCor());

    }

    IEnumerator FoundSoundCor()
    {

        while (true)
        {
            audioSource.clip = foundSound;
            audioSource.volume = 0.3f;
            audioSource.Play();
            yield return new WaitForSeconds(3);
            audioSource.clip = foundSound2;
            audioSource.volume = 0.3f;
            audioSource.Play();
            yield return new WaitForSeconds(3);
        }
    }

    public void AttackSound()
    {

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        StartCoroutine(AttackSoundcor());

    }

    IEnumerator AttackSoundcor()
    {
        yield return new WaitForSeconds(0);

        audioSource.clip = attackSound;
        audioSource.Play();
    }

}
