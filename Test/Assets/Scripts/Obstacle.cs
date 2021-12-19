using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float _timeInSec = 3;
    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private SphereCollider _meshCollider;

    private void OnMouseDrag()
        => GameController.Singletone.plyer.Decrease();

    private void OnMouseUp()
        => GameController.Singletone.plyer.Shot();

    public void Infect(float radius)
    {
        var obsts = ObstacleController.Singletone.GetObstaclesInRadius(this.transform.position, radius);
        foreach (var item in obsts)
            item.GetInfected();
        GetInfected();
    }

    public void GetInfected()
    {
        _mesh.material.color = Color.yellow;
        StartCoroutine(Destroyer());
    }

    private IEnumerator Destroyer()
    {
        yield return new WaitForSeconds(_timeInSec);
        Destroy(this.gameObject);
    }
}
