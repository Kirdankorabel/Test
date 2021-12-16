using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damageRadius = 0;
    public float increaseSpeed = 0.2f;

    public void Shot(Vector3 route)//переименовать
    {
        GetComponent<Rigidbody>().AddForce(route, ForceMode.Impulse);
    }    

    public void Increase()
    {
        StartCoroutine(IncreaseCorutine());
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
        damageRadius += 1f * Time.deltaTime;
        yield return null;
    }
}
