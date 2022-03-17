using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private string interactableTag = "Interactable";
    [SerializeField] private float interactDistance = 1f;
    [SerializeField] private Vector3 interactDirection = new Vector3(0, 1, 0);
    public void TryToInteract()
    {
       
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(interactDirection), interactDistance); //бьем луч
        //Debug.DrawRay(transform.position, TransformDirection(interactDirection));
        Debug.Log(hit.collider);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag(interactableTag)) //если коснулся объекта
            {
                Interactable obj = hit.collider.GetComponent<Interactable>();
                obj.Interact();

            }
        }
    }
}
