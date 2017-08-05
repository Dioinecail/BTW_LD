using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSoundes : MonoBehaviour

{
    private AudioSource source;
    public AudioClip music;
    public AudioClip intro;
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
}
