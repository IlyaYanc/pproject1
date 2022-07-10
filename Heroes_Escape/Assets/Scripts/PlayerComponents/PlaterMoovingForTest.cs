using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlaterMoovingForTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    [Button]
    public void Up()
    {
        Vector3 pos = transform.position + Vector3.up;
        transform.position = pos;
    }
    [Button]
    public void Right()
    {
        Vector3 pos = transform.position + Vector3.right;
        transform.position = pos;
    }
    [Button]
    public void Down()
    {
        Vector3 pos = transform.position + -1 * Vector3.up;
        transform.position = pos;
    }
    [Button]
    public void Left()
    {
        Vector3 pos = transform.position + -1 * Vector3.right;
        transform.position = pos;
    }
}
