using UnityEngine;
using UnityEngine.AI;
public class Pet : MonoBehaviour
{
    enum State
    {
        待機,
        移動,
        空腹,
        繁殖
    }
    NavMeshAgent navMesh;

    public int foodCount = 0;
    public int hungerSpeed = 5;
    public float hungerValue = 0;
    private State CurrentState = State.待機;

    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        hungerValue = 0;
    }

    void Update()
    {
        hungerValue += Time.deltaTime / hungerSpeed; 
    }
}
