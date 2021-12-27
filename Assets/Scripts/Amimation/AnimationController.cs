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

    public void Attack()
    {
        SS.PlayThenReturn("Attack" + unitCharacter.direction);
    }

    public void GotHit()
    {
        SS.PlayThenReturn("GotHit" + unitCharacter.direction);
    }

    public void GotHit(float delay)
    {
        Invoke("GotHit", delay);
    }

    void Play(string animName)
    {
        animName += unitCharacter.direction;
        if (SS.currentPlay.name != animName) { SS.PlayAnimation(animName); }
    }

    public void Death()
    {
        SS.PlayThenStop("Death" + unitCharacter.direction);
    }

    public void Death(float delay)
    {
        Invoke("Death", delay);
    }

    public float getAnimationTimer(string animName)
    {
        Animation2D animation = SS.thisUnitSprite.GetAnimation(animName);
        float timePerFrame = 1 / animation.frameRate;
        return animation.frames.Count * timePerFrame;
    }

    public float Jump()
    {
        Play("Jump");
        return getAnimationTimer("Jump" + unitCharacter.direction);
    }
}
