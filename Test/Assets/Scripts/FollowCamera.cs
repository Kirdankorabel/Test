using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Player _player;
    private Vector3 _offset;
    private Vector3 _startPosition;

    public Player SetPlayer
    {
        set
        {
            _player = value;
            transform.position = _startPosition;
        }
    }

    void Start()
    {
        _offset = transform.position - _player.transform.position;
        _startPosition = transform.position;
    }

    void Update()
        => transform.position = new Vector3(_player.transform.position.x + _offset.x, transform.position.y, transform.position.z);
}