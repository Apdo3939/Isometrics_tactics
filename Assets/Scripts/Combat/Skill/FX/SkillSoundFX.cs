using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSoundFX : MonoBehaviour
{
    public AudioClip sound;
    public float delay;

    public void Play()
    {
        Turn.unitCharacter.audioSource.clip = sound;
        Turn.unitCharacter.audioSource.PlayDelayed(delay);
    }
}
