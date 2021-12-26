using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class SpriteLoader : MonoBehaviour
{
    public static readonly List<string> animationNames = new List<string>() { "Idle", "Walk", "Attack", "GotHit" };
    public static readonly List<string> directions = new List<string>() { "North", "South", "East", "West" };
    public List<Animation2D> animations;
    public float framerate;
    public float delayStart;
    public static Transform holder;
    public Dictionary<string, Animation2D> animationsFinder;

    void Awake()
    {
        if (holder == null) { holder = this.transform.parent; }
    }

    //Load the animations... but not instantiate... some miss...
    public IEnumerator Load()
    {
        animations = new List<Animation2D>();
        animationsFinder = new Dictionary<string, Animation2D>();

        foreach (string anim in animationNames)
        {
            for (int i = 0; i < directions.Count; i++)
            {
                string[] tempName = new string[1] { transform.name + "/" + anim + directions[i] };
                Animation2D tempAnimation2D = new Animation2D();
                tempAnimation2D.frameRate = framerate;
                tempAnimation2D.delayStart = delayStart;
                tempAnimation2D.name = anim + directions[i];
                tempAnimation2D.frames = new List<Sprite>();
                animations.Add(tempAnimation2D);
                animationsFinder.Add(tempAnimation2D.name, tempAnimation2D);

                //But it is obsolete...
                AsyncOperationHandle<IList<Sprite>> handle =
                Addressables.LoadAssets<Sprite>(tempName, null, Addressables.MergeMode.Union);

                //Not work...
                //yield return Addressables.LoadAssets<Sprite>(tempName, callback, Addressables.MergeMode.Union);

                //test with code unit manual adressables 1.16.19... Not work...
                //AsyncOperationHandle<Sprite> handle = Addressables.LoadAssetAsync<Sprite>(tempName);
                //end test code unit


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

    public Animation2D GetAnimation(string name)
    {
        Animation2D rtv = null;
        animationsFinder.TryGetValue(name, out rtv);
        return rtv;
    }

    // Not work... version adressables 1.16.19
    /*void callback(IAsyncOperation<Sprite> op){
       if(op.Result!=null){
           animations[animations.Count-1].frames.Add(op.Result);
       }
   }*/
}
