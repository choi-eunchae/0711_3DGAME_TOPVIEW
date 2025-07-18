using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리자 관련 코드
using UnityEngine.UI; // UI 관련 코드
using System.Collections;


// 필요한 UI에 즉시 접근하고 변경할 수 있도록 허용하는 UI 매니저
public class UIManager : MonoBehaviour
{
    // 싱글톤 접근용 프로퍼티
    public static UIManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<UIManager>();
            }

            return m_instance;
        }
    }

    private static UIManager m_instance; // 싱글톤이 할당될 변수

    public Text ammoText; // 탄약 표시용 텍스트
    public Text scoreText; // 점수 표시용 텍스트
    public Text waveText; // 적 웨이브 표시용 텍스트
    public GameObject gameoverUI; // 게임 오버시 활성화할 UI 
    public GameObject waveStartUI; // 웨이브 시작시 보여줄 UI
    public Text waveStartText; // 웨이브 시작 텍스트
    private int previousWave=0; //이전 웨이브 저장

    // 탄약 텍스트 갱신
    public void UpdateAmmoText(int magAmmo, int remainAmmo)
    {
        ammoText.text = magAmmo + "/" + remainAmmo;
    }

    // 점수 텍스트 갱신
    public void UpdateScoreText(int newScore)
    {
        scoreText.text = "Score : " + newScore;
    }

    // 적 웨이브 텍스트 갱신
    public void UpdateWaveText(int waves, int count)
    {
        waveText.text = "Wave : " + waves + "\nEnemy Left : " + count;
        if (waves > previousWave)
        {
            previousWave++;            
            ShowWaveStartUI(waves);
        }
    }

    //중앙에 웨이브 텍스트 보여주기
    public void ShowWaveStartUI(int waveNumber, float duration = 0.7f)
    {
        if (waveStartUI == null)
    {
        Debug.LogError("waveStartUI가 Inspector에 연결되어 있지 않습니다!");
        return;
    }

        waveStartUI.SetActive(true);
        waveStartText.text = "Wave " + waveNumber + " Start!";
        StartCoroutine(HideWaveStartUIAfterDelay(duration));
    }

    private IEnumerator HideWaveStartUIAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        waveStartUI.SetActive(false);
    }


    // 게임 오버 UI 활성화
    public void SetActiveGameoverUI(bool active)
    {
        gameoverUI.SetActive(active);
    }


    // 게임 재시작
    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}