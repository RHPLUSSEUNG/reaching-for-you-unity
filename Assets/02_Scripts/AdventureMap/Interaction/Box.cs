using UnityEngine;
using UnityEngine.UI;

public class Box : MonoBehaviour
{
    bool isActive;
    Button button;

    [SerializeField]
    PlayerController player;
    GameObject boxObject;

    // 인터랙션 후 상자 효과
    [SerializeField]
    Animator BoxOpenAnim;
    [SerializeField]
    ParticleSystem BoxOpenEffect;
    
    void Start()
    {
        button = GetComponentInChildren<Button>();
        button.gameObject.SetActive(false);
        isActive = false;
        player = GameObject.Find("Player_Girl").GetComponent<PlayerController>();
        boxObject = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isActive)
        {
            button.gameObject.SetActive(false);
            isActive = false;
            
            BoxOpenAnim.SetTrigger("BoxOpenTrigger");
            BoxOpenEffect.Play();
            Destroy(boxObject, 2f);
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.parent.CompareTag("Player"))
        {
            isActive = true;
            button.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.transform.parent.CompareTag("Player"))
        {
            isActive = false;
            button.gameObject.SetActive(false);
        }
    }
}
