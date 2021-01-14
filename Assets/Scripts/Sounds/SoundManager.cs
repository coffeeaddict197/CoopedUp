using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class SoundManager : MonoSingleton<SoundManager>
{
    // Start is called before the first frame update
    public Sound[] soundLoops;
    public Sound[] soundFXs;

    public static int onSound = 1;
    public static int onMusic = 1;

    const string KEY_SOUND = "KEY_SOUND";
    const string KEY_MUSIC = "KEY_MUSIC";

    //LIST MUSIC NAME
    public static string BG_BIRDS = "bg_birds";
    public static string BG_MAIN = "bg_main";
    public static string JUMP = "jump";
    public static string ROPE = "rope";
    public static string BUG = "bug";
    public static string DEAD = "dead";
    public static string BRANCH = "branch";
    public static string SMILE = "smile";
    public static string COLLECTED_1 = "collected_1";
    public static string COLLECTED_2 = "collected_2";
    public static string COLLECTED_3 = "collected_3";
    public static string COLLECTED_4 = "collected_4";
    public static string COLLECTED_5 = "collected_5";
    public static string COLLECTED_6 = "collected_6";
    public static string COLLECTED_7 = "collected_7";
    public static string COLLECTED_8 = "collected_8";
    public static string COLLECTED_9 = "collected_9";
    public static string COLLECTED_10 = "collected_10";
    public static string WOOHOO = "wohoo";
    public static string STRETCH = "stretch";





    private void Awake()
    {

        onSound = PlayerPrefs.GetInt(KEY_SOUND, 1);
        onMusic = PlayerPrefs.GetInt(KEY_MUSIC, 1);
        Debug.Log("Awake");

        Initialize();

    }

    void Initialize()
    {
        foreach (Sound s in soundLoops)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = true;
        }

        foreach (Sound s in soundFXs)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
    }


    public void PlayLoop(string name)
    {

        if (onMusic == 1)
        {
            Sound s = Array.Find(soundLoops, sound => sound.name == name);

            if (s == null)
            {
                Debug.LogWarning("Sound " + name + " not found ");
                return;
            }
            s.source.volume = 1f;
            s.source.Play();
        }
    }


    public void Play(string name)
    {
        if (onSound == 1)
        {
            Sound s = Array.Find(soundLoops , sound => sound.name == name);

            if (s == null)
            {
                Debug.LogWarning("Sound " + name + " not found ");
                return;
            }
            s.source.volume = 1f;
            s.source.Play();
        }
    }


    public void Stop(string name)
    {
        Sound s = Array.Find(soundFXs, sound => sound.name == name);


        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found ");
            return;
        }

        s.source.Stop();
    }

    public void PlayOneShot(string name)
    {
        if (onSound == 1)
        {
            Sound s = Array.Find(soundFXs, sound => sound.name == name);


            if (s == null)
            {
                Debug.LogWarning("Sound " + name + " not found ");
                return;
            }

            s.source.PlayOneShot(s.source.clip);
        }
    }


    public void CheckToggleSound(bool on)
    {
        onSound = on == true ? 1 : 0;
        PlayerPrefs.SetInt(KEY_SOUND, onSound);

    }

    public void CheckToggleMusic(bool on)
    {
        onMusic = on == true ? 1 : 0;
        PlayerPrefs.SetInt(KEY_MUSIC, onMusic);
    }



    public void AwakeAllMusic()
    {

        if(onMusic==1)
        {
            foreach (Sound s in soundLoops)
            {
                s.source.volume = 1f;
            }
        }
    }

    public void StopAllLoop()
    {
        foreach (Sound s in soundLoops)
        {
            s.source.volume = 0f;
        }
    }



}
