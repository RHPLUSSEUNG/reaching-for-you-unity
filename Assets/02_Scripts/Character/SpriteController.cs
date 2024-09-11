using System.Collections;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class SpriteController : MonoBehaviour
{
    private Transform sprite;
    private Transform character;
    private Transform char_base;
    [SerializeField]
    float rotateSpeed = 5.0f;
    [SerializeField]
    Direction direction = Direction.Right;
    GameObject mainCamera;

    protected Animator anim;
    float targetAngleX;
    float targetAngleY;
    float targetAngleZ;
    float angleX;
    float angleY;
    bool isIdle;
    public bool cameraChanged;

    void Awake()
    {
        sprite = this.transform.GetChild(0);
        character = sprite.GetChild(0);
        char_base = character.GetChild(0);
        anim = character.GetComponent<Animator>();
        isIdle = true;
        mainCamera = GameObject.Find("Main Camera");
        SetAnimState(AnimState.Idle);
    }
    private void LateUpdate()
    {
        SetRotation(mainCamera.transform.eulerAngles.x, mainCamera.transform.eulerAngles.y);
        
        if (direction == Direction.Left)
        {
            StartCoroutine(RotateCharacter(Direction.Left));
        }
        else
        {
            StartCoroutine(RotateCharacter(Direction.Right));
        }
        StartCoroutine(RotateSprite(mainCamera.transform.eulerAngles.y));
    }
    public void SetAnimState(AnimState state)
    {
        if (!anim)
        {
            return;
        }
        switch (state)
        {
            case (AnimState.Idle):
                anim.SetInteger("State", (int)AnimState.Idle);
                break;
            case (AnimState.Move):
                anim.SetInteger("State", (int)AnimState.Move);
                break;
            case (AnimState.Hit):
                anim.SetTrigger("Hit");
                break;
            case (AnimState.Attack):
                anim.SetTrigger("Attack");
                break;
            case (AnimState.Trigger1):
                anim.SetTrigger("Trigger1");
                break;
            case (AnimState.Trigger2):
                anim.SetTrigger("Trigger2");
                break;
            case (AnimState.State1):
                anim.SetInteger("State", (int)AnimState.State1);
                break;
            case (AnimState.State2):
                anim.SetInteger("State", (int)AnimState.State2);
                break;
        }
    }
    public void Flip(Direction _direction)  //�Է� ���⿡ �°� ��������Ʈ�� ȸ��
    {
        direction = _direction;

        if (direction == Direction.Left)
        {
            StartCoroutine(RotateCharacter(Direction.Left));
        }
        else if (direction == Direction.Right)
        {
            StartCoroutine(RotateCharacter(Direction.Right));
        }
    }
    public void SetRotation(float _angleX, float _angleY)   //mainī�޶� �°� ��������Ʈ�� ȸ��
    {
        angleX = _angleX;
        angleY = _angleY;
    }
    IEnumerator RotateCharacter(Direction direction)
    {
        isIdle = false;
        float startAngleX = character.eulerAngles.x;
        float startAngleY = character.eulerAngles.y;
        float t = 0f;
        float speed = 0f;

        if (direction == Direction.Left)
        {
            targetAngleX = Mathf.Abs(angleX);
            targetAngleY = Mathf.Abs(angleY) % 360;
        }
        else
        {
            targetAngleX = -Mathf.Abs(angleX);
            targetAngleY = 180 + Mathf.Abs(angleY);
        }

        while (speed < 1.0f)
        {
            t += Time.deltaTime;
            speed = rotateSpeed * t;

            float xRotation = Mathf.LerpAngle(startAngleX, targetAngleX, speed);
            float yRotation = Mathf.LerpAngle(startAngleY, targetAngleY, speed);
            character.eulerAngles = new Vector3(xRotation, yRotation, character.eulerAngles.z);
            yield return null;
        }
        isIdle = true;
    }
    IEnumerator RotateSprite(float cameraAngleY)
    {
        isIdle = false;
        float startAngleY = sprite.eulerAngles.y;
        float t = 0f;
        float speed = 0f;

        while (speed < 1.0f)
        {
            t += Time.deltaTime;
            speed = rotateSpeed * t;

            float yRotation = Mathf.LerpAngle(startAngleY, cameraAngleY, speed);
            sprite.eulerAngles = new Vector3(sprite.eulerAngles.x, yRotation, sprite.eulerAngles.z);

            yield return null;
        }
    }
}
