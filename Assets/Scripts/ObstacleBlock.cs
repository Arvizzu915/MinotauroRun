using UnityEngine;

public class ObstacleBlock : MonoBehaviour
{
    [SerializeField] private Vector3 snapPos;
    [SerializeField] private Transform spawnPoint;

    private Vector3 initialPos;
    private GameObject currentObstacle;
    private string currentObstacleName;

    private void Start()
    {
        initialPos = transform.position;
        SpawnNewObstacle();
    }

    private void Update()
    {
        transform.Translate(MinotaurMovement.singleton.currentSpeed * Time.deltaTime * Vector2.left);

        if (transform.position.x <= snapPos.x)
        {
            Snap();
        }
    }

    private void Snap()
    {
        transform.position = initialPos;

        if (currentObstacle != null)
        {
            ObstaclePoolManager.Instance.Return(currentObstacleName, currentObstacle);
        }

        SpawnNewObstacle();
    }

    private void SpawnNewObstacle()
    {
        GameObject prefab = ObstaclePoolManager.Instance.obstaclePrefabs[Random.Range(0, ObstaclePoolManager.Instance.obstaclePrefabs.Length)];
        currentObstacleName = prefab.name;

        currentObstacle = ObstaclePoolManager.Instance.Get(prefab.name, spawnPoint.position, transform);
    }
}
