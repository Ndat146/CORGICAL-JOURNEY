using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveDistance = 1f;
    public float moveSpeed = 5f;
    public float overlapRadius = 0.2f;
    private Animator animator;
    private bool isMoving = false;

    private Quaternion targetRotation;
    private bool isRotating = false;
    public float rotateSpeed = 360f; 

    private void Start()
    {
        animator = GetComponent<Animator>();
        targetRotation = transform.rotation;

    }

    private void Update()
    {
        if (!isMoving && !isRotating)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                targetRotation *= Quaternion.Euler(0f, -90f, 0f);
                animator.SetInteger("AnimationID", 4);
                StartCoroutine(RotateSmoothly());
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                targetRotation *= Quaternion.Euler(0f, 90f, 0f);
                animator.SetInteger("AnimationID", 4);
                StartCoroutine(RotateSmoothly());
            }
        }


        if (isMoving) return;

        Vector3 direction = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            direction = Vector3.forward;
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            direction = Vector3.back;
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            direction = Vector3.left;
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            direction = Vector3.right;

        if (direction != Vector3.zero)
        {
            if (CanMoveInDirection(direction))
            {
                TryMove(direction);
            }
            else
            {
                isMoving = false; 
                animator.SetInteger("AnimationID", 0); 
            }
        }
    }

    private bool CanMoveInDirection(Vector3 direction)
    {
        Vector3 targetPosition = transform.position + direction * moveDistance;
        return IsBlockNormalAt(targetPosition);
    }
    private IEnumerator RotateSmoothly()
    {
        isRotating = true;
        Quaternion startRotation = transform.rotation;
        float elapsed = 0f;
        float duration = 90f / rotateSpeed; 

        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
        animator.SetInteger("AnimationID", 0);
        isRotating = false;
    }

    private void TryMove(Vector3 direction)
    {
        Vector3 targetPosition = transform.position + direction * moveDistance;

        if (IsBlockNormalAt(targetPosition))
        {
            StartCoroutine(MoveStep(targetPosition));
        }
        else
        {
            isMoving = false;
            animator.SetInteger("AnimationID", 0); 
        }
    }

    private bool IsBlockNormalAt(Vector3 position)
    {
        Collider[] hitColliders = Physics.OverlapSphere(position, overlapRadius);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("NormalBlock"))
            {
                return true;
            }
        }

        return false;
    }

    private IEnumerator MoveStep(Vector3 targetPosition)
    {
        isMoving = true;
        animator.SetInteger("AnimationID", 3); 

        float timeElapsed = 0f;
        float duration = 1f / moveSpeed;
        Vector3 startPosition = transform.position;

        while (timeElapsed < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        animator.SetInteger("AnimationID", 0); 
        isMoving = false;
    }
}
