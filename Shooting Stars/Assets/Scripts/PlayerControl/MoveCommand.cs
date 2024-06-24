using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ICommand
{
    private Transform playerTransform;
    private Vector3 direction;
    private float moveSpeed;
    private Camera mainCamera;

    public MoveCommand(Transform playerTransform, Vector3 direction, float moveSpeed, Camera mainCamera)
    {
        this.playerTransform = playerTransform;
        this.direction = direction;
        this.moveSpeed = moveSpeed;
        this.mainCamera = mainCamera;
    }

    public void Execute()
    {
        Vector3 newPosition = playerTransform.position + direction * moveSpeed * Time.deltaTime;
        newPosition = ClampPositionToCameraBounds(newPosition);
        playerTransform.position = newPosition;
    }

    private Vector3 ClampPositionToCameraBounds(Vector3 targetPosition)
    {
        Vector3 minBounds = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 maxBounds = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        targetPosition.x = Mathf.Clamp(targetPosition.x, minBounds.x, maxBounds.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minBounds.y, maxBounds.y);

        return targetPosition;
    }
}
