using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixLevels : MonoBehaviour
{
    public AudioMixer masterMixer;


    public void SetSfxLvl(float sfxLvl)
    {
        masterMixer.SetFloat("SfxVol", sfxLvl);
    }

    public void SetSMusicLvl(float musicLvl)
    {
        masterMixer.SetFloat("MusicVol", musicLvl);
    }
}
