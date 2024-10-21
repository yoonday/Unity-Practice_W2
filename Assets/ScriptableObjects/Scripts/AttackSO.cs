using UnityEngine;

[CreateAssetMenu(fileName = "DefaultAttackSO", menuName = "TopDownController/Attacks/Default", order = 0)]
public class AttackSO : ScriptableObject
{
    [Header("Attack Info")] // Header 인스펙터 창에서 변수를 그룹화하고 가독성을 높이기 위해 사용되는 시각적인 요소
    public float size; // 공격의 사이즈
    public float delay; // 공격의 딜레이
    public float power; // 공격의 파워
    public float speed; // 공격의 스피드
    public LayerMask target; // 공격이 어떤 레이어에 맞는지 정의

    [Header("knock Back Info")]
    public bool isOnKnockBack; // 넉백을 주는지
    public float knockbackPower; // 넉백의 세기
    public float knockbackTime; // 넉백의 지속 시간
}
