using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Singletone { get; set; }
    public Player player;
    [SerializeField] private Plane _plane;
    [SerializeField] private Plane _target;
    
    private void Awake() 
        => Singletone = this;

    private void OnMouseDrag()
    { 
        //if (!player.enabled)
            player.Decrease();
    }

    private void OnMouseUp()
    {
        //if (!player.enabled)   
            player.Shot();
    }
}
