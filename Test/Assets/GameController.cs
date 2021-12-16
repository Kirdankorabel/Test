using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Plane _plane;
    [SerializeField] private Player _plyerSphere;

    private void OnMouseDrag()
    {

        _plyerSphere.Decrease();
    }

    private void OnMouseUp()
    {
        _plyerSphere.Shot();        
    }
}
