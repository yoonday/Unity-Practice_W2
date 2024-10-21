using UnityEngine;

public class AnimationController : MonoBehaviour
{
    protected Animator animator;
    protected TopDownController controller;

    protected virtual void Awake() // 상속할 거라서 virtual 
    {
        animator = GetComponentInChildren<Animator>(); // 발자국 애니메이션에서 사용하기 용이하도록
        controller = GetComponent<TopDownController>();
    }
}
