using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObstacleController : MonoBehaviour
{
    public static ObstacleController Singletone { get; set; }

    public Vector2 size;
    [SerializeField] private Obstacle _obstaclePrefab;
    private List<Obstacle> _obstacles;
    private void Awake()
    {
        Singletone = this;
    }

    void Start()
    {
        _obstacles = new List<Obstacle>();
        CreateObstacles(100);
    }

    public List<Obstacle> GetObstaclesInRadius(Obstacle obstacle, float radius)
    {
        var obstacles = _obstacles.Where
            (obs => (obstacle.transform.position - obs.transform.position).magnitude < radius).ToList();
        _obstacles.RemoveAll
            (obs => (obstacle.transform.position - obs.transform.position).magnitude < radius);
        return obstacles;
    }

    private void CreateObstacles(int value)
    {
        for(var i = 0; i < value; i++)
        {
            var coordinateX = Random.Range(0, size.x);
            var coordinateY = Random.Range(0, size.y);

            var obstacle = Instantiate(_obstaclePrefab, transform);
            obstacle.transform.position = transform.position + new Vector3(coordinateX, 0, coordinateY);
            _obstacles.Add(obstacle);
        }
    }
}
