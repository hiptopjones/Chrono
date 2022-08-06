using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    [SerializeField]
    float followDistance = 8f;

    [SerializeField]
    bool enableFollowHeight = false;
    [SerializeField]
    float followHeight = 2.5f;

    [SerializeField]
    bool enableFollowRotation = false;
    [SerializeField]
    float followRotationAngle = 0f;
    [SerializeField]
    float rotationDamping = 2.0f;

    private float rotationOffset = 0;
    private float heightOffset = 0;
    private float distanceOffset = 0;

    private Camera followCamera;
    private GameObject followTarget;

    void Start()
    {
        followCamera = Camera.main;
    }

    void LateUpdate()
    {
        if (followTarget == null)
        {
            return;
        }

        Quaternion currentRotation = followCamera.transform.rotation;
        if (enableFollowRotation)
        {
            float wantedRotationAngle = followTarget.transform.eulerAngles.y;
            float currentRotationAngle = currentRotation.eulerAngles.y;
            currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle + followRotationAngle + rotationOffset, rotationDamping * Time.deltaTime);
            currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);
        }

        float currentHeight = followHeight;
        if (enableFollowHeight)
        {
            float wantedHeight = followTarget.transform.position.y;
            currentHeight = wantedHeight + followHeight + heightOffset;
        }

        followCamera.transform.position = followTarget.transform.position;
        followCamera.transform.position -= currentRotation * (Vector3.forward * (followDistance + distanceOffset));
        followCamera.transform.position = new Vector3(followCamera.transform.position.x, currentHeight, followCamera.transform.position.z);

        followCamera.transform.LookAt(followTarget.transform);
    }

    public void SetTarget(GameObject target)
    {
        followTarget = target;
    }
}
