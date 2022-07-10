using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCanSee : MonoBehaviour
{
    public GameObject player;

    [SerializeField]
    private float watchDistance;

    [SerializeField]
    private float watchAngel;

    public bool hasDisquiet = false;

    public bool seing()
    {
        RaycastHit2D hit;
        Vector3 u = player.transform.position - transform.position;
        //Debug.DrawRay(transform.position, transform.up, Color.yellow);
        //Debug.DrawRay(transform.position, (player.transform.position - transform.position).normalized * watchDistance, Color.red);
        hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), new Vector2(player.transform.position.x, player.transform.position.y) - new Vector2(transform.position.x, transform.position.y), watchDistance);
        //Debug.Log(hit==null);
        if (Mathf.Abs(Vector3.Angle(u, transform.up)) <= watchAngel && hit)
        {
            if (hit.collider.gameObject != null)
            Debug.DrawRay(transform.position, (player.transform.position - transform.position).normalized * hit.distance, Color.red);
            else
                Debug.DrawRay(transform.position, (player.transform.position - transform.position).normalized * watchDistance, Color.yellow);
            if (hit.collider.gameObject == player)
            {
               // Debug.Log("see");
                return true;
            }
            else
            {
                //Debug.Log("not");
                return false;
            }
        }
        else
        {
            //Debug.Log("not");
            return false;
        }
    }
}
