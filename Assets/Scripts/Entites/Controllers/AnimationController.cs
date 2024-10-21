using UnityEngine;

public class AnimationController : MonoBehaviour
{
    protected Animator animator;
    protected TopDownController controller;

    protected virtual void Awake() // ����� �Ŷ� virtual 
    {
        animator = GetComponentInChildren<Animator>(); // ���ڱ� �ִϸ��̼ǿ��� ����ϱ� �����ϵ���
        controller = GetComponent<TopDownController>();
    }
}
