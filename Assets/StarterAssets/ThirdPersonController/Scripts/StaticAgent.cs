using UnityEngine;
using UnityEngine.AI;

public class StaticAgent : MonoBehaviour
{
    NavMeshAgent agent; // navMesh 에이전트 컴포넌트
    Animator animator; // 애니메이터 컴포넌트
    [SerializeField]
    private GameObject destinationMarkerPrefab; // 목적지 마커 프리팹
    private GameObject currenMarker; // 현재 마커 인스턴스
    private LineRenderer pathLine;
    private Vector3 lastDestination;

    private bool isPerformingAction = false; // Q/W 애니메이션 중 여부

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        pathLine = GetComponent<LineRenderer>();
    }

    void Update()
    {
        // Q 눌렀을 때
        if (Input.GetKeyDown(KeyCode.Q) && !isPerformingAction)
        {
            StartAction("Action1");
        }

        // W 눌렀을 때
        if (Input.GetKeyDown(KeyCode.W) && !isPerformingAction)
        {
            StartAction("Action2");
        }

        // 마우스 클릭 시 NavMesh 이동
        if (Input.GetMouseButtonDown(0) && !isPerformingAction)
{
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit))
    {
        lastDestination = hit.point; // 목적지 저장
        agent.isStopped = false;
        animator.SetFloat("Speed", 2);
        animator.SetFloat("MotionSpeed", 1.0f);
        agent.SetDestination(lastDestination);

                // 기존 마커가 있으면 삭제
                if (currenMarker != null)
                    Destroy(currenMarker);

                // 목적지에 마커 생성
                if (destinationMarkerPrefab != null)
                    currenMarker = Instantiate(destinationMarkerPrefab, hit.point, Quaternion.identity);

                // 경로 시각화 활성화
                pathLine.enabled = true;
            }
        }

        // 경로 시각화 갱신 (애니메이션 중에는 갱신 안 함)
        if (agent.hasPath && pathLine.enabled && !isPerformingAction)
        {
            var path = agent.path;
            pathLine.positionCount = path.corners.Length;
            for (int i = 0; i < path.corners.Length; i++)
            {
                pathLine.SetPosition(i, path.corners[i]);
            }
        }
        else if (!isPerformingAction)
        {
            pathLine.positionCount = 0;
        }

        // 목적지 도달 체크
        if (!agent.pathPending && agent.remainingDistance < 0.1f && !isPerformingAction)
        {
            animator.SetFloat("Speed", 0);
            animator.SetFloat("MotionSpeed", 0);

            // 목적지에 도달하면 마커 삭제
            if (currenMarker != null)
            {
                Destroy(currenMarker);
                currenMarker = null;
            }
            // 경로 시각화 비활성화
            pathLine.enabled = false;
        }
    }

    private void StartAction(string triggerName)
    {
        isPerformingAction = true;         // 애니메이션 중임 표시
        agent.isStopped = true;            // NavMesh 멈춤
        animator.SetFloat("Speed", 0);     // Speed 0으로 고정
        animator.SetTrigger(triggerName);  // 애니메이션 실행
    }

    // 애니메이션 끝날 때 호출
    public void ResumeNavMeshAgent()
    {
        agent.isStopped = false;           // NavMesh 다시 활성화
        isPerformingAction = false;        // 애니메이션 끝남
    }
}
