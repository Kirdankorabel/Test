using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector3 decreaseSpeed = new Vector3(0.1f, 0.1f, 0.1f);
    public float force = 10;
    public float startShotForce = 3;
    public float minScale = 3;
    public static event Action lose;

    [SerializeField] private Camera _mailnCamera;
    [SerializeField] private SphereCollider _playerSphere;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private GameObject _projectileSpawn;
    [SerializeField] private Target _target;
    [SerializeField] private float _nextShotTime = 2f;
    private float _shotForce;

    private Projectile _projectile;
    private bool _onGround = false;
    private float _lastShotTime = 0;
        
    private void Update()
    {
        _target.SetTargetComponentsTransform(_playerSphere.gameObject);

        if (Input.touchCount > 0)
            WatchOut();

        if (_target.GetAreaStatus() && _onGround && _projectile == null) 
            Move();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
            _onGround = true;
    }

    public float GetRadius()
        => _playerSphere.transform.localScale.x / 2;

    public void Decrease()
    {        
        if (_onGround && !_target.GetAreaStatus() && Time.time - _lastShotTime > _nextShotTime)
        {
            if (_projectile == null)
                CreateProjectile();

            _target.SetTargetComponentsTransform(_playerSphere.gameObject);

            if (_playerSphere.transform.localScale.x < minScale)
                lose?.Invoke();
            else
            {
                StartCoroutine(DecreaseCorutine());
                _projectile.Increase();
            }
        }
    }

    private void Move()
    {
        this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(-1, 2) * force);
        _onGround = false;
    }

    private void WatchOut()
    {
        RaycastHit hit;
        int layerMask = (1 << 8);
        Touch touch = Input.GetTouch(0);
        var ray = Camera.main.ScreenPointToRay(touch.position);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            var result = transform.position - hit.point;
            result.Normalize();
            this.gameObject.transform.forward = new Vector3(result.x, 0, result.z);
        }
    }

    public void Shot()
    {
        if (_projectile != null)
        {
            _projectile.Shot(transform.forward * -_shotForce);
            _projectile = null;
            _lastShotTime = Time.time;
            _projectileSpawn.transform.localPosition = new Vector3(0, 0, -10);
        }
    }

    private void CreateProjectile()
    {
        _shotForce = startShotForce;
        _projectile = Instantiate(_projectilePrefab, _projectileSpawn.transform.position, Quaternion.identity);
        _projectile.gameObject.transform.localScale = _projectile.increaseSpeed;
        _playerSphere.transform.localScale -= decreaseSpeed;
        _projectile.SpawnPoint = _projectileSpawn;
    }

    private IEnumerator DecreaseCorutine()
    {
        _shotForce += decreaseSpeed.x * 10 * Time.deltaTime;
        _playerSphere.transform.localScale -= decreaseSpeed * Time.deltaTime;
        _playerSphere.transform.position -= new Vector3(0, decreaseSpeed.y * 0.5f, 0) * Time.deltaTime;
        _projectileSpawn.transform.localPosition += new Vector3(0, 0, - (decreaseSpeed.z * 0.5f)) * Time.deltaTime;

        yield return null;
    }
}
