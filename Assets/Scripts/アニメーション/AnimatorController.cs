using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "AnimatorController", menuName = "AnimatorController", order = 0)]
public class AnimatorController : ScriptableObject
{
    public List<AnimationClip> clips = new List<AnimationClip>();

    public AnimationClip GetClip(string name)
    {
        return clips.FirstOrDefault(clip => clip.name == name);
    }
}