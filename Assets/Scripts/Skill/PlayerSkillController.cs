using UnityEngine;
using UnityEngine.Playables;

public class PlayerSkillController : MonoBehaviour
{
    public PlayableDirector skillCutsceneDirector;
    public PlayerInput playerInput;
    public Animator playerAnimator;
    public SkillBase[] skills;
    private float[] skillCooldownTimers;
    void Start()
    {
        skillCooldownTimers = new float[skills.Length];
    }

    
    void Update()
    {
        
    }
}
