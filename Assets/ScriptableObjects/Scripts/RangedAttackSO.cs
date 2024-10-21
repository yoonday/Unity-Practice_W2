using UnityEngine;

[CreateAssetMenu(fileName = "RangedAttackSO", menuName = "TopDownController/Attacks/Ranged", order = 1)]
public class RangedAttackSO : AttackSO // 원거리 공격용. 발사체 정보를 저장하기 위함
{
    [Header("Ranged Attack Info")]
    public string bulletNameTag;
    public float duration; // 공격이 나가는 시간
    public float spread; // 랜덤으로 얼마나 퍼질지
    public int numberOfProjectilePerShot; // 한번에 발사되는 투사체 수
    public float multipleProjectilesAngle; // 발사체끼리 몇 도만큼 떨어져있는지
    public Color projectileColor; // 투사체 색깔
}