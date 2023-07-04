using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTheGuide : MonoBehaviour
{
    public Transform guide;
    public float speed = 1.0f;
    public float accuracy = 0.01f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = guide.position - this.transform.position;
        Debug.DrawRay(this.transform.position, direction, Color.red);
        if (direction.magnitude > accuracy) {
            this.transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        }
    }
}
