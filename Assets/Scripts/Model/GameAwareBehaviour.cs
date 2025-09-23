using UnityEngine;
using Platformer.Core;

//GameModelをセットしておくスクリプト
//他のクラスは基本的にこれを継承しておく
public abstract class GameAwareBehaviour : MonoBehaviour
{
    private GameModel _gm;
    protected GameModel model => _gm = Simulation.GetModel<GameModel>();
}
