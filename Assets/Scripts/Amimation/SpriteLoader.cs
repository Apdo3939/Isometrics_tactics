using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class SpriteLoader : MonoBehaviour
{
    public static readonly List<string> animationNames = new List<string>() { "Idle", "Walk", "Attack" };
    public static readonly List<string> directions = new List<string>() { "North", "South", "East", "West" };
    public List<Animation2D> animations;
    public float framerate;
    public float delayStart;
    public static Transform holder;

    void Awake()
    {
        if (holder == null) { holder = this.transform.parent; }
    }

    public IEnumerator Load()
    {
        animations = new List<Animation2D>();

        foreach (string anim in animationNames)
        {
            for (int i = 0; i < directions.Count; i++)
            {
                string[] tempName = new string[1] { transform.name + "/" + anim + directions[i] };
                Debug.Log(transform.name + "/" + anim + directions[i]);
                Animation2D tempAnimation2D = new Animation2D();
                tempAnimation2D.frameRate = framerate;
                tempAnimation2D.delayStart = delayStart;
                tempAnimation2D.name = anim + directions[i];
                tempAnimation2D.frames = new List<Sprite>();
                animations.Add(tempAnimation2D);

                AsyncOperationHandle<IList<Sprite>> handle = Addressables.LoadAssets<Sprite>(tempName, null, Addressables.MergeMode.Union);

                yield return handle;
                WhenFinished(handle);
            }
        }
    }

    void WhenFinished(AsyncOperationHandle<IList<Sprite>> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            foreach (Sprite sprite in handle.Result)
            {
                animations[animations.Count - 1].frames.Add(sprite);
            }
        }
    }
}
