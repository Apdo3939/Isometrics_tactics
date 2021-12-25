using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSwapper : MonoBehaviour
{
    public SpriteLoader thisUnitSprite;
    SpriteRenderer SR;
    public Animation2D currentPlay;
    public Coroutine playing;
    public Queue<Animation2D> sequenceAnimations;

    void Awake()
    {
        SR = GetComponent<SpriteRenderer>();
        sequenceAnimations = new Queue<Animation2D>();
    }

    IEnumerator Play()
    {
        while (true)
        {
            currentPlay = sequenceAnimations.Dequeue();

            if (sequenceAnimations.Count == 0) { sequenceAnimations.Enqueue(currentPlay); }

            float timePerFrame = 1 / currentPlay.frameRate;

            for (int i = 0; i < currentPlay.frames.Count; i++)
            {
                SR.sprite = currentPlay.frames[i];
                yield return new WaitForSeconds(timePerFrame);
            }

            yield return null;
        }
    }

    public void Stop()
    {
        if (playing != null) { StopCoroutine(playing); }
        sequenceAnimations.Clear();
    }

    public void PlayAnimation(string name)
    {
        Stop();
        sequenceAnimations.Enqueue(thisUnitSprite.GetAnimation(name));
        playing = StartCoroutine(Play());
    }

    public void PlayAnimations(List<string> names)
    {
        Stop();
        foreach (string name in names)
        {
            sequenceAnimations.Enqueue(thisUnitSprite.GetAnimation(name));
        }
        playing = StartCoroutine(Play());
    }

    public void PlayThenReturn(string name)
    {
        Animation2D toPlay = thisUnitSprite.GetAnimation(name);

        Stop();
        sequenceAnimations.Enqueue(toPlay);
        sequenceAnimations.Enqueue(currentPlay);
        playing = StartCoroutine(Play());
    }

    public void PlayAtTheEnd(string name)
    {
        Animation2D toPlay = thisUnitSprite.GetAnimation(name);
        sequenceAnimations.Enqueue(toPlay);
    }
}
