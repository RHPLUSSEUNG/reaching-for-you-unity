using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    bool isActive;
    Button button;

    [SerializeField]
    PlayerController player;
    void Start()
    {
        button = GetComponentInChildren<Button>();
        button.gameObject.SetActive(false);
        isActive = false;
        player = GameObject.Find("Player_Girl").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isActive)
        {
            button.gameObject.SetActive(false);
            isActive = false;
            player.Teleport(this.GetComponent<BoxCollider>());
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
