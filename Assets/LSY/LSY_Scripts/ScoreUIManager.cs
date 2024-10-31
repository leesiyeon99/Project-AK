using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreUIManager : MonoBehaviour
{
    public static ScoreUIManager Instance { get; private set; }

    public int score = 0;
    public int scoreline = 0;

    public int levelScore;
    public int eliteEnemyCount;
    public int normalEnemyCount;
    public int remainBulletCount;

    public TextMeshProUGUI scoreTextUI;
    public TextMeshProUGUI scorelineText;
    public TextMeshProUGUI scorelineUI;

    // Commet : Text : 변경되는 숫자 /  UI : 해당 숫자의 안내멘트 (변하지 않는 글씨)
    public TextMeshProUGUI normalEnemyUI;
    public TextMeshProUGUI normalEnemyText;
    public TextMeshProUGUI eliteEnemyText;
    public TextMeshProUGUI eliteEnemyUI;
    public TextMeshProUGUI levelScoreText;
    public TextMeshProUGUI levelScoreUI;
    public TextMeshProUGUI remainBulletText;
    public TextMeshProUGUI remainBulletUI;

    public TextMeshProUGUI noticeWordText;

    public bool isState;

    public LJH_UIManager ljh_UIManager;
    public WHS_DollyProgress whs_DollyProgress;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        score = 0; 
        UpdateScoreUI(); 
        normalEnemyText.gameObject.SetActive(false);
        eliteEnemyText.gameObject.SetActive(false);
        remainBulletText.gameObject.SetActive(false);
        levelScoreText.gameObject.SetActive(false);
        noticeWordText.gameObject.SetActive(false);
        scorelineText.gameObject.SetActive(false);
        scorelineUI.gameObject.SetActive(false);
        normalEnemyUI.gameObject.SetActive(false);
        eliteEnemyUI.gameObject.SetActive(false);
        levelScoreUI.gameObject.SetActive(false);
        remainBulletUI.gameObject.SetActive(false);
    }
    private void Update()
    {
        Debug.Log(score);
        UpdateScoreUI();

        if (isState)
        {
            WinScoreLine(score);
        }
        else
        {
            LoseScoreLine(score);
        }
    }

    // ScoreUIManager.Instance.AddScore(점수); 로 점수 추가 할 수 있게 함
    public void AddScore(int monsterScore)
    {
        if (monsterScore == 100) normalEnemyCount++;
        if (monsterScore == 500) eliteEnemyCount++;
        Debug.Log("ADDSCORE");
        score += monsterScore;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        scoreTextUI.text = "점수: " + score;
    }

    // Comment : 게임클리어시 나오는 UI
    public void WinScoreLine(int score)
    {
        remainBulletCount = PlayerSpecialBullet.Instance.SpecialBullet.Length;
        float remainHP = ljh_UIManager.ljh_curHp / 10000; // 코루틴 시작하고 0으로 초기화 해줘야 할듯??

        normalEnemyText.text = normalEnemyCount.ToString();
        eliteEnemyText.text = eliteEnemyCount.ToString();
        levelScoreText.text = levelScore.ToString();
        remainBulletText.text = remainBulletCount.ToString();

        //최종점수 = 20000 * 난이도 * 남은체력 + score + 남은 특수 탄환 * 100
        scoreline = 20000 * levelScore * /*remainHP*/ + score + remainBulletCount * 100;

        scorelineText.text = scoreline.ToString();

        StartCoroutine(ScoreDisplayRoutine());
    }

    // Comment : 게임 중간에 플레이어가 사망시 나오는 UI
    public void LoseScoreLine(int score)
    {
        //remainBulletCount = PlayerSpecialBullet.Instance.SpecialBullet.Length;
        //float remainProgress = whs_DollyProgress.progress; 머지하고 수정 // 코루틴 시작하고 0으로 초기화 해줘야 할듯??

        normalEnemyText.text = normalEnemyCount.ToString();
        eliteEnemyText.text = eliteEnemyCount.ToString();
        levelScoreText.text = levelScore.ToString();
        remainBulletText.text = remainBulletCount.ToString();

        //최종점수 = 20000 * 난이도 * 진행정도 + score + 남은 특수 탄환 * 100
        scoreline = 20000 * levelScore * /*remainProgress*/ + score + remainBulletCount * 100;

        scorelineText.text = scoreline.ToString();

        StartCoroutine(ScoreDisplayRoutine());
    }

    IEnumerator ScoreDisplayRoutine()
    {
        WaitForSeconds delay = new WaitForSeconds(0.3f);

        yield return delay;
        normalEnemyText.gameObject.SetActive(true);
        normalEnemyUI.gameObject.SetActive(true);
        yield return delay;
        eliteEnemyText.gameObject.SetActive(true);
        eliteEnemyUI.gameObject.SetActive(true);
        yield return delay;
        levelScoreText.gameObject.SetActive(true);
        levelScoreUI.gameObject.SetActive(true);
        yield return delay;
        remainBulletText.gameObject.SetActive(true);
        remainBulletUI.gameObject.SetActive(true);
        yield return delay;
        scorelineText.gameObject.SetActive(true);
        scorelineUI.gameObject.SetActive(true);
        yield return delay;
        noticeWordText.gameObject.SetActive(true);
    }

    public void ResetScore()
    {
        score = 0;
        scoreline = 0;
        levelScore = 0;
        eliteEnemyCount = 0;
        normalEnemyCount = 0;
        remainBulletCount = 0;  

        UpdateScoreUI();
    }
}
