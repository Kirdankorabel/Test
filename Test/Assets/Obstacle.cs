using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float _timeInSec = 3;
    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private SphereCollider _meshCollider;

    public void Infect(float radius)
    {
        var obsts = ObstacleController.Singletone.GetObstaclesInRadius(this, radius);
        foreach(var item in obsts)
            item.GetInfected();
        GetInfected();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.gameObject.CompareTag("Obstacle"))
        {
            var obstacle = other.gameObject.GetComponent<Obstacle>();
            obstacle.GetInfected();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other);
        if (other.gameObject.CompareTag("Obstacle"))
        {
            var obstacle = other.gameObject.GetComponent<Obstacle>();
            obstacle.GetInfected();
        }
    }

    public void GetInfected()
    {
        _mesh.material.color = Color.yellow;
        StartCoroutine(Destroyer());
    }

    IEnumerator Destroyer()
    {
        yield return new WaitForSeconds(_timeInSec);
        Destroy(this.gameObject);
    }
}
