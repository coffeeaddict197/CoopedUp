using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroPlayMusic : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void PlayMusic(string music)
    {
        SoundManager.Instance.PlayOneShot(music);
    }
}
