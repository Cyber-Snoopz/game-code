using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerOpen : MonoBehaviour
{
    
    void OnEnable()
    {
        if(PlayAudio.instance)
            PlayAudio.instance.PlayAudioClip();
    }
    void OnDisable()
    {   
        if(PlayAudio.instance)
            PlayAudio.instance.PlayAudioClip();
    }
}
