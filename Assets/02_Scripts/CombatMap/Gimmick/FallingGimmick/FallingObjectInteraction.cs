using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FallingObjectInteraction : GimmickInteraction
{
    [Header("FallingPrefabs")]
    public GameObject[] FallingPrefabs;
    public Transform FallingObjectTransform;

    Text warningText;
    private Rigidbody faliingRigid;
    SpriteRenderer spriteRenderer;
    
    public Sprite[] TurnNumSprite;

    // int gimmickTurnCnt = 2; // 기믹 작동에 필요한 턴 개수

    int remainTurnCnt = -1;
    int currentTurnCnt = -1; // 현재 총 턴 횟수

    bool isFalling = false;

    private void Awake() {
        warningColor = new Color32(180, 75, 75, 255);
        TurnCnt = 2;
    }

    void Start()
    {
        int randomIndex = Random.Range(0, FallingPrefabs.Length);
        
        // 초기화 작업
        GameObject fallObject = Instantiate(FallingPrefabs[randomIndex], FallingObjectTransform);
        fallObject.transform.localPosition = Vector3.zero;

        FallingObjectTransform.gameObject.SetActive(false);
        
        faliingRigid = FallingObjectTransform.GetComponent<Rigidbody>();
        if (faliingRigid == null)
            faliingRigid = fallObject.AddComponent<Rigidbody>();

        // 물리 작용 끔
        faliingRigid.isKinematic = true;

        spriteRenderer = gameObject.transform.parent.GetComponentInChildren<SpriteRenderer>();
        if(spriteRenderer != null)
        {
            spriteRenderer.sprite = TurnNumSprite[TurnCnt];
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player")) 
        {
            Debug.Log("Enter the Gimmick!!");

            // warningText = Managers.BattleUI.warningUI.GetText();
            currentTurnCnt = Managers.Battle.totalTurnCnt;
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")) 
        {
            remainTurnCnt = Managers.Battle.totalTurnCnt - currentTurnCnt;

            if(remainTurnCnt >= TurnCnt) 
            {
                StartCoroutine(FallStone());
                if(isFalling)
                    FallingObjectTransform.gameObject.SetActive(false);
            }
            else {
                if(Managers.Battle.isPlayerTurn)
                {
                    spriteRenderer.sprite = TurnNumSprite[TurnCnt - remainTurnCnt];
                    // StartCoroutine(ShowWarningCoroutine());
                    // Managers.BattleUI.warningUI.ShowWarningUI();   
                }
            }
        }
    }

    public int getRemainedTurnCnt()
    {
        return remainTurnCnt;
    }
    private IEnumerator FallStone() 
    {
        FallingObjectTransform.gameObject.SetActive(true);
        faliingRigid.isKinematic = false;
        yield return new WaitForSeconds(2f);
        isFalling = true;
    }

    private IEnumerator ShowWarningCoroutine()
    {
        warningText.text = $"{TurnCnt - remainTurnCnt}턴 이후 낙석 주의!!";
        Managers.BattleUI.warningUI.SetText(warningText.text);
        yield return new WaitForSeconds(2f);
        Managers.Battle.isPlayerTurn = false;
    }
}
