using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FallingObjectInteraction : MonoBehaviour
{
    [Header("FallingPrefabs")]
    public GameObject[] FallingPrefabs;
    public Transform FallingObjectTransform;

    Text warningText;
    private Rigidbody faliingRigid;

    int gimmickTurnCnt = 2; // 기믹 작동에 필요한 턴 개수

    int remainTurnCnt = -1;
    int currentTurnCnt = -1; // 현재 총 턴 횟수

    // Start is called before the first frame update
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
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player")) 
        {
            Debug.Log("Enter the Gimmick!!");

            warningText = Managers.BattleUI.warningUI.GetText();
            currentTurnCnt = Managers.Battle.totalTurnCnt;
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")) 
        {
            remainTurnCnt = Managers.Battle.totalTurnCnt - currentTurnCnt;

            if(remainTurnCnt >= gimmickTurnCnt) 
            {
                FallingObjectTransform.gameObject.SetActive(true);
                faliingRigid.isKinematic = false;
            }
            else {
                if(Managers.Battle.isPlayerTurn)
                {
                    StartCoroutine(ShowWarningCoroutine());
                    Managers.BattleUI.warningUI.ShowWarningUI();   
                }
            }
        }
    }
    private IEnumerator ShowWarningCoroutine()
    {
        warningText.text = $"{gimmickTurnCnt - remainTurnCnt}턴 이후 낙석 주의!!";
        Managers.BattleUI.warningUI.SetText(warningText.text);
        yield return new WaitForSeconds(2f);
        Managers.Battle.isPlayerTurn = false;
    }
}
