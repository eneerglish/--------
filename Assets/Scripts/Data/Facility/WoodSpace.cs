using UnityEngine;
using Platformer.Core;
using Platformer.Events;

public class WoodSpace : Facility
{
    enum State
    {
        苗木,
        木
    }
    public GameObject Sapling;
    public GameObject Tree;

    private StateMachine<State> stateMachine = new StateMachine<State>(3);



    public override void DoStartProcess(GameObject target)
    {
        base.DoStartProcess(target);
        AnimatorController saplingAnimatorController = Sapling.GetComponent<AnimatorController>();
        AnimatorController treeAnimatorController = Tree.GetComponent<AnimatorController>();

        int currentPhase = stateMachine.phase;
        currentPhase++;
        stateMachine.ChangePhase(currentPhase);
        if (currentPhase >= stateMachine.maxPhase)
        {
            stateMachine.ChangeState(stateMachine.currentState == State.苗木 ? State.木 : State.苗木);
            saplingAnimatorController.ChangeAnimState(stateMachine.phase);
            treeAnimatorController.ChangeAnimState(2);
            Debug.Log("木のスペースで成長が");
        }
        else
        {
            //苗木のとき
            if (stateMachine.currentState == State.苗木)
            {

                saplingAnimatorController.ChangeAnimState(stateMachine.phase);
                treeAnimatorController.ChangeAnimState(0);
            }
            else
            {
                //それ以外は木

                treeAnimatorController.ChangeAnimState(2);
            }
        }
    }
}