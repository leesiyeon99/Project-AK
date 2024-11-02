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

    // Commet : Text : ����Ǵ� ���� /  UI : �ش� ������ �ȳ���Ʈ (������ �ʴ� �۾�)
    public TextMeshProUGUI normalEnemyUI;
    public TextMeshProUGUI normalEnemyText;
    public TextMeshProUGUI eliteEnemyText;
    public TextMeshProUGUI eliteEnemyUI;
    public TextMeshProUGUI levelScoreText;
    public TextMeshProUGUI levelScoreUI;
    public TextMeshProUGUI remainBulletText;
    public TextMeshProUGUI remainBulletUI;

    public TextMeshProUGUI noticeWordText;


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

        
            //WinScoreLine(score);
        
        
        
            //LoseScoreLine(score);
        
    }

    // ScoreUIManager.Instance.AddScore(����); �� ���� �߰� �� �� �ְ� ��
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
        scoreTextUI.text = "����: " + score;
    }

    // Comment : ����Ŭ����� ������ UI
    public void WinScoreLine(int score)
    {
        //remainBulletCount = PlayerSpecialBullet.Instance.SpecialBullet.Length;
        //float remainHP = ljh_UIManager.ljh_curHp / 10000; // �ڷ�ƾ �����ϰ� 0���� �ʱ�ȭ ����� �ҵ�??

        normalEnemyText.text = normalEnemyCount.ToString();
        eliteEnemyText.text = eliteEnemyCount.ToString();
        levelScoreText.text = levelScore.ToString();
        remainBulletText.text = remainBulletCount.ToString();

        //�������� = 20000 * ���̵� * ����ü�� + score + ���� Ư�� źȯ * 100
        scoreline = 20000 * levelScore * /*remainHP*/ + score + remainBulletCount * 100;

        scorelineText.text = scoreline.ToString();

        StartCoroutine(ScoreDisplayRoutine());
    }

    // Comment : ���� �߰��� �÷��̾ ����� ������ UI
    public void LoseScoreLine(int score)
    {
        //remainBulletCount = PlayerSpecialBullet.Instance.SpecialBullet.Length;
        //float remainProgress = whs_DollyProgress.progress;// �ڷ�ƾ �����ϰ� 0���� �ʱ�ȭ ����� �ҵ�??

        normalEnemyText.text = normalEnemyCount.ToString();
        eliteEnemyText.text = eliteEnemyCount.ToString();
        levelScoreText.text = levelScore.ToString();
        remainBulletText.text = remainBulletCount.ToString();

        //�������� = 20000 * ���̵� * �������� + score + ���� Ư�� źȯ * 100
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
