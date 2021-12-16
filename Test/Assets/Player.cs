using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float decreaseSpeed = 0.1f;
    [SerializeField] private Camera _mailnCamera;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private GameObject _projectileSpawn;
    private Projectile _projectile;
    private Plane _plane;

    private void Update()
    {
        _plane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = _mailnCamera.ScreenPointToRay(Input.mousePosition);//поменять на тач
        if (_plane.Raycast(ray, out float dist))
        {
            var result = transform.position - ray.GetPoint(dist);
            result.Normalize();
            this.gameObject.transform.forward = new Vector3(result.x, 0, result.z);
        }
    }

    public Projectile CreateProjectile()
    {
        _projectile = Instantiate(_projectilePrefab, _projectileSpawn.transform.position, Quaternion.identity);
        return _projectile;
    }

    public void Shot()
    {
        _projectile.Shot(transform.forward * -5);//поменять множитель
        _projectile = null;
    }

    public void Decrease()
    {
        if (_projectile == null)
            _projectile = CreateProjectile();

        float delta = decreaseSpeed * 0.5f;

        _projectile.gameObject.transform.position = _projectileSpawn.transform.position;
            //new Vector3(_projectileSpawn.transform.position.x + decreaseSpeed, _projectile.increaseSpeed + decreaseSpeed,
            //_projectileSpawn.transform.position.z + decreaseSpeed);
            //проверку на размер
        StartCoroutine(DecreaseCorutine());
        _projectile.Increase();        
    }

    private IEnumerator DecreaseCorutine()
    {
        _projectileSpawn.transform.position +=
    new Vector3(0, decreaseSpeed, decreaseSpeed) * Time.deltaTime;
        this.transform.localScale -= new Vector3(decreaseSpeed, decreaseSpeed, decreaseSpeed) * Time.deltaTime;
        this.transform.position -= new Vector3(0, decreaseSpeed * 0.5f, 0) * Time.deltaTime;


        yield return null;
    }
}
