using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScoreUIManager : MonoBehaviour
{
    public static ScoreUIManager Instance { get; private set; }
    public GameObject scoreUIobj;
    public float score = 0;
    public float scoreline = 0;
    public float levelScore;
    public float eliteEnemyCount;
    public float normalEnemyCount;
    public float remainBulletCount;
    public float remainProgress;
    public float remainHP;
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
    //public WHS_DollyProgress whs_DollyProgress;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            ResetScore();
        }
    }
    private void Start()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        scoreUIobj.SetActive(false);
        ResetScore();
    }
    private void Update()
    {
        Debug.Log(score);
        UpdateScoreUI();
    }
    // ScoreUIManager.Instance.AddScore(점수); 로 점수 추가 할 수 있게 함
    public void AddScore(float monsterScore)
    {
        if (monsterScore == 100) normalEnemyCount++;
        if (monsterScore == 500) eliteEnemyCount++;
        Debug.Log("ADDSCORE");
        score += monsterScore;
        UpdateScoreUI();
    }
    private void UpdateScoreUI()
    {
        if (scoreTextUI == null) return;
        scoreTextUI.text = score.ToString();
    }
    // Comment : 게임클리어시 나오는 UI
    public void WinScoreLine()
    {
        Debug.Log("게임 클리어 점수판");
        if (PlayerSpecialBullet.Instance == null || LJH_UIManager.Instance == null)
        {
            return;
        }
        remainBulletCount = PlayerSpecialBullet.Instance.SpecialBullet.Length;
        remainHP = LJH_UIManager.Instance.ljh_curHp / 10000;
        if (WHS_StageIndex.curStage == 1)
        {
            levelScore = 1;
        }
        else if (WHS_StageIndex.curStage == 2)
        {
            levelScore = 2;
        }
        normalEnemyText.text = normalEnemyCount.ToString();
        eliteEnemyText.text = eliteEnemyCount.ToString();
        levelScoreText.text = levelScore.ToString();
        remainBulletText.text = remainBulletCount.ToString();
        //최종점수 = 20000 * 난이도 * 남은체력 + score + 남은 특수 탄환 * 100
        scoreline = 20000 * levelScore * remainHP + score + remainBulletCount * 100;
        scorelineText.text = scoreline.ToString();
        StartCoroutine(ScoreDisplayRoutine());
    }
    // Comment : 게임 중간에 플레이어가 사망시 나오는 UI
    public void LoseScoreLine()
    {
        Debug.Log("게임 오버 점수판");
        if (normalEnemyText == null) return;
        if (PlayerSpecialBullet.Instance == null)
        {
            return;
        }
        remainBulletCount = PlayerSpecialBullet.Instance.SpecialBullet.Length;
        if (WHS_StageIndex.curStage == 1)
        {
            levelScore = 1;
            remainProgress = LSY_WaveBar.instance?.wavePercent ?? 0;
        }
        else if (WHS_StageIndex.curStage == 2)
        {
            levelScore = 2;
            remainProgress = WHS_DollyProgress.Instance?.progress ?? 0;
        }
        normalEnemyText.text = normalEnemyCount.ToString();
        eliteEnemyText.text = eliteEnemyCount.ToString();
        levelScoreText.text = levelScore.ToString();
        remainBulletText.text = remainBulletCount.ToString();
        scoreline = 20000 * levelScore * remainProgress + score + remainBulletCount * 100;
        scorelineText.text = scoreline.ToString();
        StartCoroutine(ScoreDisplayRoutine());
    }
    IEnumerator ScoreDisplayRoutine()
    {
        WaitForSeconds delay = new WaitForSeconds(0.3f);
        scoreUIobj.SetActive(true);
        yield return new WaitForSeconds(2f);
        if (normalEnemyText != null) normalEnemyText.gameObject.SetActive(true);
        if (normalEnemyUI != null) normalEnemyUI.gameObject.SetActive(true);
        yield return delay;
        if (eliteEnemyText != null) eliteEnemyText.gameObject.SetActive(true);
        if (eliteEnemyUI != null) eliteEnemyUI.gameObject.SetActive(true);
        yield return delay;
        if (levelScoreText != null) levelScoreText.gameObject.SetActive(true);
        if (levelScoreUI != null) levelScoreUI.gameObject.SetActive(true);
        yield return delay;
        if (remainBulletText != null) remainBulletText.gameObject.SetActive(true);
        if (remainBulletUI != null) remainBulletUI.gameObject.SetActive(true);
        yield return delay;
        if (scorelineText != null) scorelineText.gameObject.SetActive(true);
        if (scorelineUI != null) scorelineUI.gameObject.SetActive(true);
        yield return delay;
        if (noticeWordText != null) noticeWordText.gameObject.SetActive(true);
    }
    public void ResetScore()
    {
        scoreUIobj.SetActive(false);
        score = 0;
        scoreline = 0;
        levelScore = 0;
        eliteEnemyCount = 0;
        normalEnemyCount = 0;
        remainBulletCount = 0;
        remainProgress = 0;
        remainHP = 0;
        UpdateScoreUI();
    }
}