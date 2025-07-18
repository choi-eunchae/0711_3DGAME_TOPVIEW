using UnityEngine;

//스킬 효과를 받을 수 있는 타입들이 구현할 인터페이스
public interface ISkullable
{
    //스킬 효과를 적용하는 메서드(데미지, 상태이상 등)
    void OnSkillEffect(float damage, Vector3 hitPoint);
}

