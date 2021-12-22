using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Singletone { get; set; }
    public Player player;
    [SerializeField] private Plane _plane;
    [SerializeField] private FollowCamera _camera;
    [SerializeField] private Target _target;

    [SerializeField] private Player _playerPrefab;
    [SerializeField] private Target _targetPrefab;

    public void Restart()
    {
        var target_ = Instantiate(_targetPrefab);
        var player_ = Instantiate(_playerPrefab);
        player_.SetTarget = target_;
        ObstacleController.Singletone.CreateNewObstacles();
        _camera.SetPlayer = player_;

        Destroy(player.gameObject);
        player = player_;
        Destroy(_target.gameObject);
        _target = target_;
    }

    private void Awake()
        => Singletone = this;

    private void OnMouseDrag()
        => player.Decrease();

    private void OnMouseUp()
        => player.Shot();
}
