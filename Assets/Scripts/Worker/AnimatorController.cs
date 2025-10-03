using UnityEngine;
using UnityEngine.AI;
public class AnimatorController : MonoBehaviour
{
    public Animator anim;
    public AnimatorOverrideController animatorOverrideController;
    //アニメーションの更新
    public NavMeshAgent navMesh;

    void Start()
    {
        anim = GetComponent<Animator>();
        navMesh = GetComponent<NavMeshAgent>();
        animatorOverrideController = new AnimatorOverrideController(anim.runtimeAnimatorController);
        anim.runtimeAnimatorController = animatorOverrideController;
    }
    void Update()
    {
        //アニメーションの更新
        float speed = navMesh.velocity.magnitude;
        anim.SetFloat("speed", speed);
    }

    public void ChangeAnimationClip(AnimationClip newClip)
    {
        if (newClip == null) return;
        animatorOverrideController["Anim_Base"] = newClip;

    }

    public void ChangeAnimState(int state)
    {
        anim.SetInteger("ID", state);
    }
}
