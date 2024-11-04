using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class WaveDialogueManager : MonoBehaviour
{
    public GameObject background;
    public GameObject bossBackGround;

    public TextMeshProUGUI fairyText;
    public TextMeshProUGUI playertText;
    public TextMeshProUGUI narrationText;
    public TextMeshProUGUI bossText;

    private string[][] waveDialogues; 

    private int currentWave = 0;

    void Start()
    {
        background.gameObject.SetActive(false);
        bossBackGround.gameObject.SetActive(false);

        waveDialogues = new string[][]
        {
            // Comment : 첫번째 웨이브 시작 전
            new string[] { "요정: 내가 힘을 빌려줄테니까, 왼쪽 트리거 버튼을 눌러 방어 역장을 펼쳐.",
                            "요정: 왼쪽 트리거를 클릭한 상태로 총에다 갖다대면 장전이 될거야",
                            "요정: 오른쪽 트리거 버튼을 눌러 총알을 발사해!" }, 
            // Comment : 첫번째 웨이브 종료 후
            new string[] { "요정: 조심성 없기는! 너 때문에 죽을 뻔 했잖아!", 
                            "주인공: 너, 넌 뭐야! 반딧불인가?!", 
                            "요정: 요정이야! 너 나침반 아직 가지고 있는거 맞지?!", 
                            "요정이 당신에게서 나침반을 뺏어 들었다.", "요정: 이게 뭐야, 다 망가졌잖아!", 
                            "요정: 너 때문에 내 나침반이 엉망이 됐어!" }, 
            // Comment : 두번째 웨이브 시작 전
            new string[] { "요정: 우측 그립 버튼을 눌러 무기를 교체할 수 있어",
                            "요정: 총알이 부족하면 좌측 그립 버튼을 눌러, 내가 마법으로 만들어줄게!", }, 
            // Comment : 두번째 웨이브 종료 후
            new string[] { "보스: 하하-! 허둥대는 모습이 우스꽝스럽기 그지없군!", 
                            "보스: 요리조리 잘도 도망 다니시던데! ","보스: 결국 꼬맹이한테 붙다니, 이젠 더 갈 곳도 없는 모양이지?",
                            "요정: 너희 때문에 나침반을 잃어버리지만 않았어도 이런 일은…!",
                            "보스: 미리 내놓았으면 잃어버릴 일도 없었을 것 아냐!", "보스: 지금이라도 너가 옳은 선택을 할 기회를 주지.",
                            "요정: 누가 너희에게 넘길 줄 알고?!",
                            "보스: 흠…뭐 그럴 줄 알았어.",
                            "보스: 영역전개",
                            "보스: 사록멸쇄진"},
            // Comment : 세번째 웨이브 시작 전
            new string[] { "보스: 주위를 한 번 둘러봐라! 이제 추격전도 끝이다!", "보스: 나침반이 부숴져 힘을 잃은 너가 과연 날 이겨낼 수 있을까!",
                            "보스: 보아라! 나의 전투의 춤을!",
                            "보스: 이 춤의 힘으로 내 부하들은 한 단계 진화한다!",
                            "요정: 이전이랑 별 차이 없는 것 같은데",
                            "보스: 힘아 솟아라!",
                            "보스: 더 빠르게 달려라!",
                            "보스: 더 오래 맞아라!"},
            // Comment : 세번째 웨이브 종료 후 멘트 없음
            // Comment : 네번째 웨이브 시작 전
            new string[] {"보스: 많이 기다렸지?", "보스: 이제 내가 직접 상대해주마!" },
            // Comment : 보스 처치 후
            new string[] { "보스: 내 영혼의…울림을… 느껴봐…" }
        };
    }


    private IEnumerator DisplayDialogue(string[] dialogues)
    {
        foreach (var line in dialogues)
        {
            if (line.StartsWith("요정:"))
            {
                fairyText.text = line;
                yield return new WaitForSeconds(2f);
                fairyText.text = ""; 
            }
            else if (line.StartsWith("주인공:"))
            {
                background.gameObject.SetActive(true);
                playertText.text = line;
                yield return new WaitForSeconds(2f);
                background.gameObject.SetActive(false);
                playertText.text = ""; 
            }
            else if (line.StartsWith("보스:"))
            {
                background.gameObject.SetActive(true);
                playertText.text = line;
                yield return new WaitForSeconds(2f);
                background.gameObject.SetActive(false);
                playertText.text = "";
            }
            else
            {
                background.gameObject.SetActive(true);
                narrationText.text = line; 
                yield return new WaitForSeconds(2f);
                background.gameObject.SetActive(false);
                narrationText.text = ""; 
            }
        }
    }

    public void StartWave()
    {
        StartCoroutine(DisplayDialogue(waveDialogues[currentWave]));
        currentWave++;
    }
}
