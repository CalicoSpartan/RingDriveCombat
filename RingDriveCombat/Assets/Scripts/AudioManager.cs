﻿using UnityEngine.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;


    // Start is called before the first frame update
    void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        //Play("Theme");
    }

    public void Play(string name)
    {

        if (name == "BulletImpact")
        {
            int choice = UnityEngine.Random.Range(0, 3);
            if (choice == 0)
            {
                name = "BulletImpact1";
            }
            else if (choice == 1)
            {
                name = "BulletImpact2";
            }
            else
            {
                name = "BulletImpact3";
            }
        }
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();

    }
}
