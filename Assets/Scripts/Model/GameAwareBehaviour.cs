using UnityEngine;
using Platformer.Core;
public abstract class GameAwareBehaviour : MonoBehaviour
{
    private GameModel _gm;
    protected GameModel GM => _gm = Simulation.GetModel<GameModel>();
}
