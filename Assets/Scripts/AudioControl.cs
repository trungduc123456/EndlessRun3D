using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTonic.MasterAudio;

public class AudioControl : MonoBehaviour
{

    public static AudioControl Instance;
    public static bool enbaleSound = true;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {

        DontDestroyOnLoad(transform.gameObject);
    }


    public void OpenSound(string name)
    {
        if (enbaleSound)
        {
            MasterAudio.PlaySound(name);
        }
    }
   
    public void StopSound(string name)
    {
        MasterAudio.StopAllOfSound(name);
    }

    public void StopAllSound()
    {
        MasterAudio.StopEverything();
    }
}
