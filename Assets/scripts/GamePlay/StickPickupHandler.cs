using UnityEngine;

public class StickPickupHandler : MonoBehaviour
{
    public Transform mouthPoint; 
    private GameObject currentStick = null;
    private bool isHoldingStick = false;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetInteger("AnimationID", 5);

            if (!isHoldingStick)
            {
                TryHoldStick();
            }
            else
            {
                ReleaseStick();
            }
        }
    }

    private void TryHoldStick()
    {
        Collider[] hits = Physics.OverlapSphere(mouthPoint.position, 0.3f);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Stick"))
            {
                Transform endPoint = hit.transform;
                GameObject stick = endPoint.parent.gameObject;

                Transform endA = stick.transform.Find("EndA");
                Transform endB = stick.transform.Find("EndB");

                if (endA == null || endB == null) continue;

                Vector3 stickDirection = (endB.position - endA.position).normalized;
                Vector3 playerForward = transform.forward;

                float dot = Mathf.Abs(Vector3.Dot(stickDirection, playerForward));
                if (dot > 0.3f) 
                {
                    Debug.Log("Stick is not perpendicular to player, cannot pick up.");
                    continue;
                }

                currentStick = stick;

                Vector3 offset = stick.transform.position - endPoint.position;
                stick.transform.SetParent(mouthPoint);
                stick.transform.position = mouthPoint.position + offset;

                var rb = stick.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.isKinematic = true;

                isHoldingStick = true;

                var player = Object.FindFirstObjectByType<PlayerMovement>();
                player.isHoldingStick = true;
                player.currentStick = currentStick;

                break; 
            }
        }
    }

    private void ReleaseStick()
    {
        if (currentStick != null)
        {
            Vector3 originalPosition = currentStick.transform.position;
            Quaternion originalRotation = currentStick.transform.rotation;

            originalPosition += Vector3.down * 0.1f;

            currentStick.transform.SetParent(null);

            var rb = currentStick.GetComponent<Rigidbody>();
            if (rb != null)
                rb.isKinematic = false;

            currentStick.transform.position = originalPosition;
            currentStick.transform.rotation = originalRotation;

            Object.FindFirstObjectByType<PlayerMovement>().isHoldingStick = true;
            Object.FindFirstObjectByType<PlayerMovement>().currentStick = currentStick;

            currentStick = null;
            isHoldingStick = false;
        }
    }

}
