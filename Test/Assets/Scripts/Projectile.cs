using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damageRadius = 0;
    public float lifetime = 3;
    public Vector3 increaseSpeed = new Vector3(0.2f, 0.2f, 0.2f);
    public float damageRadiusncIreaseSpeed = 0.8f;
    private GameObject _spawnPoint;
    private float _delta = 0;
    private float _shotTime = 0;

    private void Update()
    {
        if (_shotTime != 0 && Time.time - _shotTime > lifetime)
            Destroy(this.gameObject);
    }

    public void Shot(Vector3 route)
    { 
        GetComponent<Rigidbody>().AddForce(route, ForceMode.Impulse);
        _shotTime = Time.time;
    }

    public GameObject SpawnPoint
    {
        set => _spawnPoint = value;
    }

    public void Increase()
    {
        StartCoroutine(IncreaseCorutine());
        transform.position = new Vector3(_spawnPoint.transform.position.x, _delta, _spawnPoint.transform.position.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            var obstacle = collision.gameObject.GetComponent<Obstacle>();
            obstacle.Infect(damageRadius);
            Destroy(this.gameObject);
        }
    }

    private IEnumerator IncreaseCorutine()
    {
        this.gameObject.transform.localScale += increaseSpeed * Time.deltaTime;
        _delta += increaseSpeed.x * 0.5f * Time.deltaTime;
        damageRadius += damageRadiusncIreaseSpeed * Time.deltaTime;
        
        yield return null;
    }
}
