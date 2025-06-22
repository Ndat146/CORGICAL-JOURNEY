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

    public bool isHoldingStick = false;
    public GameObject currentStick;

    private Transform endA;
    private Transform endB;

    private Vector3 lastPlayerPosition;
    private Vector3 lastStickPosition;

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

        if (!IsBlockNormalAt(targetPosition))
            return false;

        if (!isHoldingStick || currentStick == null)
            return true;

        endA = currentStick.transform.Find("EndA");
        endB = currentStick.transform.Find("EndB");

        if (endA == null || endB == null)
            return true;

        Vector3Int playerGridPos = Vector3Int.RoundToInt(transform.position);
        Vector3Int endAGridPos = Vector3Int.RoundToInt(endA.position);
        Vector3Int endBGridPos = Vector3Int.RoundToInt(endB.position);

        bool isEndAInMouth = playerGridPos == endAGridPos;
        bool isEndBInMouth = playerGridPos == endBGridPos;

        if (!isEndAInMouth && !isEndBInMouth)
            return true;

        Transform mouthEnd = isEndAInMouth ? endA : endB;
        Transform otherEnd = isEndAInMouth ? endB : endA;

        Vector3 stickOffset = otherEnd.position - mouthEnd.position;

        Vector3 predictedMouthEnd = transform.position + direction * moveDistance;
        Vector3 predictedOtherEnd = predictedMouthEnd + stickOffset;

        if (IsBlockTreeAt(predictedOtherEnd))
            return false;

        return true;
    }
    private bool IsBlockTreeAt(Vector3 position)
    {
        Collider[] hitColliders = Physics.OverlapSphere(position, overlapRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Tree")) // Tag bạn dùng cho block chứa cây
            {
                return true;
            }
        }
        return false;
    }
    public void ReleaseStick()
    {
        currentStick = null;
        isHoldingStick = false;
        endA = null;
        endB = null;
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
        // Lưu trạng thái trước khi di chuyển
        lastPlayerPosition = transform.position;

        if (isHoldingStick && currentStick != null)
            lastStickPosition = currentStick.transform.position;

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

        // Sau khi di chuyển xong, kiểm tra va chạm với cây
        if (isHoldingStick && currentStick != null)
        {
            endA = currentStick.transform.Find("EndA");
            endB = currentStick.transform.Find("EndB");

            if (endA != null && endB != null)
            {
                bool isEndAInMouth = Vector3.Distance(endA.position, transform.position) < 0.3f;
                bool isEndBInMouth = Vector3.Distance(endB.position, transform.position) < 0.3f;

                Transform otherEnd = isEndAInMouth ? endB : endA;
                if (IsBlockTreeAt(otherEnd.position))
                {
                    // Quay lại vị trí cũ
                    transform.position = lastPlayerPosition;
                    currentStick.transform.position = lastStickPosition;
                }
            }
        }

        animator.SetInteger("AnimationID", 0);
        isMoving = false;
    }
    private void OnDrawGizmos()
    {
        if (!isHoldingStick || currentStick == null)
            return;

        Transform endA = currentStick.transform.Find("EndA");
        Transform endB = currentStick.transform.Find("EndB");

        if (endA == null || endB == null)
            return;

        Vector3 playerPos = transform.position;
        Vector3Int playerGrid = Vector3Int.RoundToInt(playerPos);
        Vector3Int endAGrid = Vector3Int.RoundToInt(endA.position);
        Vector3Int endBGrid = Vector3Int.RoundToInt(endB.position);

        bool isEndAInMouth = playerGrid == endAGrid;
        bool isEndBInMouth = playerGrid == endBGrid;

        Transform otherEnd = isEndAInMouth ? endB : endA;

        // Vẽ hình cầu đại diện vùng kiểm tra va chạm
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(otherEnd.position, overlapRadius);
    }
    public void BowCheck()
    {
    }
}
