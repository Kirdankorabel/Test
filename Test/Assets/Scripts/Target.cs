using System;
using UnityEngine;

public class Target : MonoBehaviour
{
    public static event Action victory;
    [SerializeField] private GameObject _quad;
    [SerializeField] private AreaChecker _area;
    [SerializeField] private Animator _animator;

    private void Start()
        => DestroyObtacles();

    public bool GetAreaStatus() 
        => _area.GetStatus();

    public void SetTargetComponentsTransform(GameObject gameObject)
    {
        SetCheckedAreaTransform(gameObject);
        SetQuadTransform(gameObject);
    }

    private void SetQuadTransform(GameObject gameObject)
    {
        _quad.transform.localScale = new Vector3((gameObject.transform.position - transform.position).magnitude,
            gameObject.transform.localScale.y, gameObject.transform.position.z);
        _quad.transform.position = new Vector3(gameObject.transform.position.x - _quad.transform.localScale.x / 2,
            _quad.transform.position.y, _quad.transform.position.z);

        if (_quad.transform.localScale.x < 20)
        {
            _animator.SetBool("IsOpen", true);
            victory?.Invoke();
        }
    }

    private void SetCheckedAreaTransform(GameObject gameObject)
    {
        _area.transform.localScale = new Vector3(gameObject.transform.localScale.x + 10,
            _area.transform.localScale.y, _area.transform.localScale.z);
        _area.transform.position = new Vector3(gameObject.transform.position.x - 25,
            _area.transform.position.y, _area.transform.position.z);
    }

    private void DestroyObtacles()
    {
        var obsts = ObstacleController.Singletone.GetObstaclesInRadius(transform.position, 15);
        foreach (var item in obsts)
            Destroy(item.gameObject);
    }
}
