using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveDialogue2Manager : MonoBehaviour
{
    public GameObject background;
    //public GameObject bossBackGround;

    public TextMeshProUGUI fairyText;
    public TextMeshProUGUI playertText;
    public TextMeshProUGUI narrationText;
    public TextMeshProUGUI bossText;

    private string[][] waveDialogues;

    private int currentWave = 0;

    void Start()
    {
        background.gameObject.SetActive(false);
        //bossBackGround.gameObject.SetActive(false);

        waveDialogues = new string[][]
        {
            // Comment : 스테이지 2 시작 
            new string[] { "주인공: 여기가 중세인가? 뭔가 분위기가 이상한데?",
                            "요정: 뭔가 위험한 느낌이야 빨리 이 숲을 벗어나자", 
                            "벤시의 웃음소리가 들려온다."}, 
            // Comment : 첫번째 웨이브 종료 후 경로 이동 중 대사 출력
            new string[] { "요정: 시작부터 너무한데? 우리가 벤시한테 잘못한 것도 없잖아!",
                            "주인공: 벤시들은 우리가 마음에 들지 않는가봐",
                            "요정: 이 시대도 뭔가 문제가 있는건가?"}, 
            // Comment : 두번째 웨이브 종료 후 경로 이동을 하고 대사 출력
            new string[] { "요정: 이거 우리 잘못 도망친거 같지..?",
                            "요정: 어... 그건 맞는거...거 같네 빨리 도망치자!", 
                            "약탈자: 크하하하!!! 잡아라 잡아!!!"}, 
            // Comment : 세번째 웨이브를 통과 후 경로 이동을 하고 대사 출력
            new string[] { "요정: 우리 언제까지 도망쳐야 되는거야!?",
                            "주인공: 어! 저기 성이 보여! 저기까지 도망쳐보자!",
                            "요정: 그래 그래! 저기서 도움을 요청하면 되겠다!"},
            // Comment : 네번째 웨이브를 통과 후 경로 이동을 하고 대사 출력
            new string[] { "요정: 성에 다왔어! 조금만 힘을 내자!",
                            "주인공: 헉헉.. 이제 좀 안전하겠지?",
                            "요정: 어.. 그건 아닌거 같아",
                            "성 중앙에 모여있는 수 많은 기사들"},
            // Comment : 다섯번째 웨이브를 통화 후 경로 이동을 하고 대사 출력
            new string[] {"요정: 잠깐만 이 성 위에서 익숙한 힘이 느껴져",
                            "주인공: 혹시 나침반이야!? 난 빨리 나침반을 고치고 싶어",
                            "요정: 맞는거 같아! 얼른 올라가보자!"},
            // Comment : 보스와 마주 후
            new string[] {"보스: 어서와라! 너희 죽고싶지 않다면 나침반을 내놔라!",
                            "주인공: 안돼! 이건 나한테 중요한거야!"}
            // TODO: 보스 전 시작 시 보스 멘트는 용진님 쪽에서 할지 이쪽에서 할지 정해야 함
        };

        StartWave();
    }


    private IEnumerator DisplayDialogue(string[] dialogues)
    {
        foreach (var line in dialogues)
        {
            if (line.StartsWith("요정:"))
            {
                fairyText.text = line;
                yield return new WaitForSeconds(3f);
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
                //bossBackGround.gameObject.SetActive(true);
                playertText.text = line;
                yield return new WaitForSeconds(2f);
                //bossBackGround.gameObject.SetActive(false);
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
