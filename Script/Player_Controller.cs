using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] private Transform topCube;
    public int score;
    public Text scoreText;
    public float limitX;
    public float lerpTime;


    [SerializeField]
    public float runningSpeed;

    [SerializeField]
    public float xSpeed;

    private float _currentRunningSpeed;

    [SerializeField]
    private float moveSens;

    [SerializeField]
    private Animator anim;

    private const string coinStr = "Coin";
    private const string enemyCoinStr = "EnemyCoin";
    private const string trapStr = "Trap";
    private const string finishStr = "Finish";

    private bool canMove;
    void Start()
    {
        canMove = true;
        _currentRunningSpeed = runningSpeed;
    }
    void Update()
    {
        
        PlayerMove();
    }

    
    private void PlayerMove()
    {
        if (!canMove)
            return;

        float targetX = Mathf.Clamp(transform.position.x + SwerveInput.I.MoveFactorX * moveSens, -4f, 4f);
        Vector3 newPosition = new Vector3(targetX, transform.position.y, transform.position.z + _currentRunningSpeed * Time.deltaTime);
        
        transform.position = newPosition;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag(coinStr))
        {
            score++;
            Destroy(other.gameObject);
        }
        else if(other.CompareTag (enemyCoinStr))
        {
            if (score >= 1)
            {
                score--;
                Destroy(other.gameObject);
            }
        }
        else if (other.CompareTag (trapStr))
        {
            enabled = false;
        }
        else if (other.CompareTag (finishStr))
        {
            other.tag = "Untagged";
            StartCoroutine(FinishCoroutine());
        }
        scoreText.text = score.ToString();
    }
    IEnumerator FinishCoroutine()
    {
        canMove = false;
        transform.position = new Vector3(0,transform.position.y,transform.position.z);
        Camera.main.transform.DOLocalRotate(Vector3.right * 15f,0.8f);
        transform.DOMoveZ(253, 1.2f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(1.2f);
        transform.rotation = Quaternion.Euler(0, 180, 0);

        anim.SetBool("Finish", true);
        topCube.SetParent(transform);
        Vector3 targetVec = new Vector3(transform.position.x, transform.position.y + 4.5f, transform.position.z);
        transform.DOMoveY(transform.position.y + 10f,1.8f).SetEase(Ease.InCubic);
    
        
        
    }
}
