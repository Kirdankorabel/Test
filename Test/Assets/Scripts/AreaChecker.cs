using System.Collections.Generic;
using UnityEngine;

public class AreaChecker : MonoBehaviour
{
    [SerializeField] private LayerMask _LayerMask;

    public bool GetStatus()
    {
        Collider[] hitColliders = Physics.OverlapCapsule(GameController.Singletone.player.transform.position, 
            this.transform.position, GameController.Singletone.player.GetRadius() + 1, _LayerMask);
        if (hitColliders.Length > 0)
            return false;
        else return true;
    }
}