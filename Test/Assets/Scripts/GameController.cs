using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Singletone { get; set; }
    [SerializeField] private Plane _plane;
    [SerializeField] private Player _plyerSphere;

    public Player GetPlayer => _plyerSphere;

    private void Awake()
    {
        Singletone = this;
    }

    private void OnMouseDrag()
    {
        _plyerSphere.Decrease();
    }

    private void OnMouseUp()
    {
        _plyerSphere.Shot();        
    }
}
