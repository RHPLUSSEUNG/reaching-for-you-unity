using UnityEngine;
using UnityEngine.UI;

public class Box : MonoBehaviour
{
    bool isActive;
    Button button;

    [SerializeField]
    PlayerController player;
    GameObject boxObject;

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

            Destroy(boxObject);
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
