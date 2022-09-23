using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static AudioClip wrongAnswer;
    public static AudioClip correctAnswer;
    public static AudioClip takeCube;

    private static AudioSource audioComponent;

    void Start()
    {

        wrongAnswer = Resources.Load<AudioClip>("negative");
        correctAnswer = Resources.Load<AudioClip>("sfx-magic");
        takeCube = Resources.Load<AudioClip>("ButtonClick");

        audioComponent = GetComponent<AudioSource>();

    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "negative":
                audioComponent.PlayOneShot(wrongAnswer);
                break;
            case "sfx-magic":
                audioComponent.PlayOneShot(correctAnswer);
                break;
            case "ButtonClick":
                audioComponent.PlayOneShot(takeCube);
                break;
        }
    }

}
