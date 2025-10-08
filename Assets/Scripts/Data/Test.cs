using UnityEngine;
using Platformer.Core;
using Platformer.Events;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class Test : GameAwareBehaviour
{
    void Start()
    {
        //ResetButton.SetActive(false);
        for (int i = 0; i < 2; i++)
        {
            var ev = Simulation.Schedule<SpawnWorker>();
            ev.startPos = model.workerManager.playerSpawnPoint;
        }

        //GeneEnemy(30);
    }

    void Update()
    {
        if (!model.workerManager.isReset) return;
        if (Keyboard.current.rKey.isPressed)
        {
            SceneLoad();
        }
    }


    public void GeneEnemy(int time = 0)
    {
        var ev = Simulation.Schedule<ApeerEnemyEvent>(time);
        ev.transform = model.enemyAppertransform;
    }

    public void SceneLoad()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
