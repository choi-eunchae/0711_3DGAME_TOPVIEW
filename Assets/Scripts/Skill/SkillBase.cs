using UnityEngine;
using UnityEngine.Playables;

public abstract class SkillBase : ScriptableObject
{
    public string skillName;
    public Sprite icon;
    public PlayableDirector sillCutscene; //각 스킬별 컷씬
    public abstract float cooldown { get; } //스킬의 쿨타임 (초 단위)

    //스킬 발동 시 호출
    public abstract void Activate(GameObject user);
}
