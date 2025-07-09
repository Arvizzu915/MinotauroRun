using UnityEngine;
using UnityEngine.InputSystem;

public class ObstaclesManager : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclesList, currentObstacles;

    [SerializeField] private float currentSpeed, thrustingSpeed, normalSpeed, thrustingAcceleration;

    [SerializeField] private float spawnTimer, blockLifetime;
    private float lastSpawned = 0;

    private bool thrusting = false;

    private void Update()
    {
        SpawnBlock();

        MoveStage();

        Thrust();
    }

    private void MoveStage()
    {
        transform.Translate(currentSpeed * Time.deltaTime * Vector2.right);
    }

    private void Thrust()
    {
        if (thrusting)
        {
            Mathf.MoveTowards(currentSpeed, thrustingSpeed, thrustingAcceleration);
        }
        else
        {
            Mathf.MoveTowards(currentSpeed, normalSpeed, thrustingAcceleration);
        }
    }

    private void SpawnBlock()
    {
        if (Time.time - lastSpawned > spawnTimer)
        {
            lastSpawned = Time.time;

        }
    }

    public void ThrustInput(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            thrusting = true;
        }

        if (ctx.canceled)
        {
            thrusting = false;
        }
    }
}
