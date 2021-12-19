using UnityEngine;

public class CheckedArea : MonoBehaviour
{
    public bool isFreeArea;
    [SerializeField] private LayerMask _LayerMask;
    private bool _started;

    void Start() => _started = true;

    void Update() => MyCollisions();

    void MyCollisions()
    {
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity, _LayerMask);
        if (hitColliders.Length > 0) isFreeArea = false;
        else isFreeArea = true;
    }
}