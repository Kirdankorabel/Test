using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float decreaseSpeed = 0.1f;
    public float force = 10;
    public float shotForce = 10;
    public float minScale = 3;
    [SerializeField] private Camera _mailnCamera;
    [SerializeField] private SphereCollider _playerSphere;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private GameObject _projectileSpawn;
    [SerializeField] private Target _target;
    [SerializeField] private float _nextShotTime = 1f;
    private Projectile _projectile;
    private bool _onGround = false;
    private float _lastShotTime = 0;//стрелять только если в области есть цели
    

    private void Update()
    {
        _target.SetQuadScale(_playerSphere.gameObject);

        WatchOut();

        if (_target.GetAreaStatus() && _onGround) 
            Move();
    }

    private void Move()
    {
        this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(-1, 2) * force);
        _onGround = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
            _onGround = true;
    }

    private void WatchOut()
    {
        RaycastHit hit;
        int layerMask = (1 << 8);
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            Debug.Log(hit.point);
            var result = transform.position - hit.point;
            result.Normalize();
            this.gameObject.transform.forward = new Vector3(result.x, 0, result.z);
        }
    }

    public void Shot()
    {
        if (_onGround && _projectile != null)
        {
            _projectile.Shot(transform.forward * -shotForce);
            _projectile = null;
            _lastShotTime = Time.time;
        }
    }

    public void Decrease()
    {
        if (_onGround && Time.time - _lastShotTime > _nextShotTime)
        {
            if (_projectile == null)
            {
                CreateProjectile();
            }

            _target.SetQuadScale(_playerSphere.gameObject);
            StartCoroutine(DecreaseCorutine());
            _projectile.Increase();
        }
    }
    private void CreateProjectile()
    {
        _projectile = Instantiate(_projectilePrefab, _projectileSpawn.transform.position, Quaternion.identity);
        _projectile.SpawnPoint = _projectileSpawn;
    }

    private IEnumerator DecreaseCorutine()
    {
        if (this.gameObject.transform.localScale.x <= minScale)
        {
            //заканчивать игру
            yield return null;
        }
        _playerSphere.transform.localScale -= new Vector3(decreaseSpeed, decreaseSpeed, decreaseSpeed) * Time.deltaTime;
        _playerSphere.transform.position -= new Vector3(0, decreaseSpeed * 0.5f, 0) * Time.deltaTime;

        yield return null;
    }
}
