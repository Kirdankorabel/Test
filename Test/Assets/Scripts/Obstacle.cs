using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float _timeInSec = 3;
    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private Collider _collider;
    [SerializeField] private Color _marker;
    [SerializeField] private Color _unmarker;

    private void OnMouseDrag()
        => GameController.Singletone.player.Decrease();

    private void OnMouseUp()
        => GameController.Singletone.player.Shot();

    public void Infect(float radius)
    {
        var obsts = ObstacleController.Singletone.GetObstaclesInRadius(this.transform.position, radius);
        foreach (var item in obsts)
            item.GetInfected();
        GetInfected();
    }

    private void GetInfected()
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
