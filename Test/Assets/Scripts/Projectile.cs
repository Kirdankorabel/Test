using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damageRadius = 0;
    public float increaseSpeed = 0.2f;
    private GameObject _spawnPoint;
    private float _delta = 0;

    public void Shot(Vector3 route)//переименовать
    {
        GetComponent<Rigidbody>().AddForce(route, ForceMode.Impulse);
    }

    public GameObject SpawnPoint
    {
        set => _spawnPoint = value;
    }

    public void Increase()
    {
        StartCoroutine(IncreaseCorutine());
        transform.position = new Vector3(_spawnPoint.transform.position.x, _delta, _spawnPoint.transform.position.z);
        Debug.Log(transform.position);
    }

    void OnCollisionEnter(Collision collision)
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
        this.gameObject.transform.localScale += new Vector3(increaseSpeed, increaseSpeed, increaseSpeed) * Time.deltaTime;
        _delta += increaseSpeed * 0.5f * Time.deltaTime;
        damageRadius += 1f * Time.deltaTime;
        yield return null;
    }
}
