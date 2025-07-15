using UnityEngine;
using UnityEngine.AI;

public class StaticAgent : MonoBehaviour
{
    // [SerializeField]
    // Transform target;
    NavMeshAgent agent; //navMesh 에이전트 컴포넌트

    Animator animator; //애니메이터 컴포넌트
    [SerializeField]
    private GameObject destinationMarkerPrefab; //목적지 마커 프리팹
    private GameObject currenMarker; //현재 마커 인스턴스
    private LineRenderer pathLine;
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        pathLine = GetComponent<LineRenderer>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //마우스 왼쪽 버튼 클릭 시
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                animator.SetFloat("Speed", 2);
                animator.SetFloat("MotionSpeed", 1.0f);
                agent.SetDestination(hit.point);
                
                //기존 마커가 있으면 삭제
                if (currenMarker != null)
                    Destroy(currenMarker);

                //목적지에 마커 생성
                if (destinationMarkerPrefab != null)
                    currenMarker = Instantiate(destinationMarkerPrefab, hit.point, Quaternion.identity);

                //경로 시각화 활성화
                pathLine.enabled = true;
            }
        }
        //경로 시각화 갱신
        if (agent.hasPath && pathLine.enabled)
        {
            var path = agent.path;
            pathLine.positionCount = path.corners.Length;
            for (int i = 0; i < path.corners.Length; i++)
            {
                pathLine.SetPosition(i, path.corners[i]);
            }
        }
        else
        {
            pathLine.positionCount = 0;
        }
        //목적지 도달 체크
        if (!agent.pathPending && agent.remainingDistance < 0.1f)
        {
            animator.SetFloat("Speed", 0);
            animator.SetFloat("MotionSpeed", 0);
            
            //목적지에 도달하면 마커 삭제
            if (currenMarker != null)
            {
                Destroy(currenMarker);
                currenMarker = null;
            }
            //경로 시각화 비활성화
            pathLine.enabled = false;
        }
    }
}
