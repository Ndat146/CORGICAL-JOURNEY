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
        Collider[] hits = Physics.OverlapSphere(mouthPoint.position, 0.5f);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Stick")) 
            {
                Transform endPoint = hit.transform;
                GameObject stick = endPoint.parent.gameObject; 

                currentStick = stick;

                // Tính offset giữa đầu gậy và object cha
                Vector3 offset = stick.transform.position - endPoint.position;

                stick.transform.SetParent(mouthPoint);

                stick.transform.position = mouthPoint.position + offset;


                var rb = stick.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.isKinematic = true;

                isHoldingStick = true;

                Object.FindFirstObjectByType<PlayerMovement>().isHoldingStick = true;
                Object.FindFirstObjectByType<PlayerMovement>().currentStick = currentStick;


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
