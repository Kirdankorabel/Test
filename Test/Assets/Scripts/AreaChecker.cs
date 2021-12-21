using System.Collections.Generic;
using UnityEngine;

public class AreaChecker : MonoBehaviour
{
    [SerializeField] private LayerMask _LayerMask;
    private List<Obstacle> _obstacles = new List<Obstacle>();
    public bool GetStatus()
    {
        Collider[] hitColliders = Physics.OverlapCapsule(GameController.Singletone.player.transform.position, 
            this.transform.position, GameController.Singletone.player.GetRadius() + 1, _LayerMask);
        if (hitColliders.Length > 0)
        {
            foreach (var item in hitColliders)
                if (item.GetComponent<Obstacle>() != null)
                {
                    Obstacle obstacle = item.GetComponent<Obstacle>();
                    obstacle.Markerd();
                    _obstacles.Add(obstacle);
                }
            return false;
        }
        else
        {
            if (_obstacles.Count > 0)
            {
                foreach (var item in _obstacles)
                    if(item.gameObject != null && !item.isInfected)
                    item.Unmarkerd();
                _obstacles.Clear();
            }
            return true;
        }
    }
}