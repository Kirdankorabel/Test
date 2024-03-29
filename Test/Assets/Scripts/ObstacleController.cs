﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObstacleController : MonoBehaviour
{
    public static ObstacleController Singletone { get; set; }

    public Vector2 size;
    [SerializeField] private Obstacle _obstaclePrefab;
    [SerializeField] private int _number; 
    private List<Obstacle> _obstacles;

    private void Awake()
        => Singletone = this;

    private void Start()
    {
        _obstacles = new List<Obstacle>();
        CreateObstacles();
    }

    public List<Obstacle> GetObstaclesInRadius(Vector3 position, float radius)
    {
        var obstacles = _obstacles.Where
            (obs => (position - obs.transform.position).magnitude < radius).ToList();
        _obstacles.RemoveAll
            (obs => (position - obs.transform.position).magnitude < radius);
        return obstacles;
    }

    public void CreateNewObstacles()
    {
        foreach (var item in _obstacles)
            Destroy(item.gameObject);
        _obstacles = new List<Obstacle>();
        CreateObstacles();
    }    


    private void CreateObstacles()
    {
        for(var i = 0; i < _number; i++)
        {
            var coordinateX = Random.Range(0, size.x);
            var coordinateY = Random.Range(0, size.y);

            var obstacle = Instantiate(_obstaclePrefab, transform);
            obstacle.transform.position = transform.position + new Vector3(coordinateX, 0, coordinateY);
            _obstacles.Add(obstacle);
        }
    }
}
