using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public GameObject following;
    [Range(0.0f, 1.0f)]
    public float interested;

    private void Update()
    {
        Vector3 coolPos = following.transform.position + new Vector3(0, 0.25f, 0);

        transform.position = Vector3.MoveTowards(transform.position, coolPos, interested);
    }


}
