using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    [SerializeField] AudioClip mouseClickClip;
    [SerializeField] AudioClip containerSound;
    [SerializeField] AudioSource source;
    public static PlayAudio instance;

    void Awake()
    {
        if(instance != null)
            Destroy(gameObject);
        instance = this;
    } 
    void Start()
    {
        source = Camera.main.GetComponent<AudioSource>();
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && source)
        {
            source.PlayOneShot(mouseClickClip);
        }
    }

    public void PlayAudioClip()
    {
        source.PlayOneShot(containerSound);
    }
}
