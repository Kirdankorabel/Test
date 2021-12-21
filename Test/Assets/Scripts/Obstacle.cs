using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public bool isInfected = false;
    [SerializeField] private float _timeInSec = 3;
    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private Collider _collider;
    [SerializeField] private Color _marker;
    [SerializeField] private Color _unmarker;
    private bool _isMarked = false;

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

    public void Markerd()
    {
        if (!_isMarked)
        {
            _mesh.material.color = _marker;
            _isMarked = true;
        }
    }

    public void Unmarkerd()
    {
        if (_isMarked)
        {
            _mesh.material.color = _unmarker;
            _isMarked = false;
        }
    }

    private void GetInfected()
    {
        isInfected = true;
        _collider.enabled = false;
        _mesh.material.color = Color.yellow;
        StartCoroutine(Destroyer());
    }

    private IEnumerator Destroyer()
    {
        yield return new WaitForSeconds(_timeInSec);
        Destroy(this.gameObject);
    }
}
