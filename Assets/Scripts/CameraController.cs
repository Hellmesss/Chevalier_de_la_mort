using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera cameraSize;
    public Transform target;
    public float smoothing = 5f;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;
        cameraSize = GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetCamPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPosition, smoothing * Time.deltaTime);

    }
    void Update()
    {
        //reset
        if (Input.GetKeyDown(KeyCode.R)) comeback();

        //hide cursor in game
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.visible = true;
        }
    }
    void comeback()
    {
        cameraSize.orthographicSize = 5;
    }
}
