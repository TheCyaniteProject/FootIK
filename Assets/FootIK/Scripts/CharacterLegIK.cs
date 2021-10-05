using UnityEngine;

public class CharacterLegIK : MonoBehaviour
{
    public Animator animator;

    public LayerMask groundMask;
    public Vector3 offset;

    public float distanceToGround;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Walking", !animator.GetBool("Walking"));
        }
    }


    private void OnAnimatorIK(int layerIndex)
    {
        animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);
        animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);
        animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1f);

        // Left foot
        RaycastHit hit;
        Ray ray = new Ray(animator.GetIKPosition(AvatarIKGoal.LeftFoot) + offset, Vector3.down);
        if (Physics.Raycast(ray, out hit, distanceToGround + offset.y, groundMask))
        {
            Vector3 footPosition = hit.point;
            footPosition.y += distanceToGround;
            animator.SetIKPosition(AvatarIKGoal.LeftFoot, footPosition);
            animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(transform.forward, hit.normal));

        }

        // Right foot
        ray = new Ray(animator.GetIKPosition(AvatarIKGoal.RightFoot) + offset, Vector3.down);
        if (Physics.Raycast(ray, out hit, distanceToGround + offset.y, groundMask))
        {
            Vector3 footPosition = hit.point;
            footPosition.y += distanceToGround;
            animator.SetIKPosition(AvatarIKGoal.RightFoot, footPosition);
            animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(transform.forward, hit.normal));

        }
    }
}
