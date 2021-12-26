using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    UnitCharacter unitCharacter;
    SpriteSwapper SS;

    void Awake()
    {
        unitCharacter = GetComponent<UnitCharacter>();
        SS = transform.Find("Jumper/Sprite").GetComponent<SpriteSwapper>();
    }

    public void Idle()
    {
        Play("Idle");
    }

    public void Walk()
    {
        Play("Walk");
    }

    void Play(string animName)
    {
        animName += unitCharacter.direction;
        if (SS.currentPlay.name != animName) { SS.PlayAnimation(animName); }
    }
}
