using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Singletone { get; set; }
    public Player plyer;
    [SerializeField] private Plane _plane;
    [SerializeField] private Plane _target;
    
    private void Awake() => 
        Singletone = this;

    private void OnMouseDrag() =>
        plyer.Decrease();

    private void OnMouseUp()
        => plyer.Shot();        
}
