using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    private const float speed=1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
        
    }
    private void FixedUpdate()
    {
        Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z), speed * Time.deltaTime * Mathf.Max(Vector3.Distance(new Vector3(transform.position.x, transform.position.y, 0), new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0)), 1));
    }
}
