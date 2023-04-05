using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontrol : MonoBehaviour
{
    public AudioClip deathClip;

    private int jump_count = 0;
    private bool isGrounded = false;
    private bool isDead = false;
    [SerializeField] private float jump_force = 700f;
    
    private Rigidbody2D playerRigid;
    private Animator animator;
    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        playerRigid = transform.GetComponent<Rigidbody2D>(); //이 transform에 있는 Rigidbody2D를 가져온다
        animator = transform.GetComponent<Animator>();
        audio = transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //1. 사용자의 입력(마우스 왼쪽 버튼)을 감지하고 점프처리
        //2. 점프카운트 2번 확인
        //3. player가 죽으면 더 이상 실행되지 않도록 만들기
        if (isDead)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && jump_count < 2) //마우스 왼쪽은 0, 오른쪽은 1
        {
            jump_count++;
            playerRigid.velocity = Vector2.zero; //점프 직전에 속도를 순간적으로 제로로 변경
            playerRigid.AddForce(new Vector2(0, jump_force)); //플레이어 리지드바디에 위쪽으로 힘주기
            audio.Play(); //점프할 때 소리
        }
        //마우스 왼쪽 버튼을 떼고, 속도의 y값이 양수면 (양수라는 뜻 -> 위로 상승중이라면)
        else if(Input.GetMouseButtonUp(0) && playerRigid.velocity.y > 0)
        {
            playerRigid.velocity *= 0.5f; //현재 속도의 절반으로 줄여주세요
        }
        animator.SetBool("Grounded", isGrounded);
    }

    private void Die() 
    {
        //사망처리
        animator.SetTrigger("Die");

        //오디오 클립을 deathClip으로 변경
        audio.clip = deathClip;
        audio.Play();

        playerRigid.velocity = Vector2.zero;

        isDead = true;

        GameManager.Instance.PlayerDead();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ollision.transform.tag.Equals("Dead") 은 스트링을 비교하는 것으로 조금 느리다
        //연산자보다 빌트인 매서드를 사용하는 것이 더 빠르다. 
        if (collision.transform.CompareTag("Dead") && !isDead)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //플레이어가 바닥에 닿았음 감지 처리
        //어떠한 콜라이더가 닿았으며 충돌 표면이 위쪽을 보고 있다면?
        ContactPoint2D temp = collision.GetContact(0);
        if (temp.normal.y > 0.7f) //닿는 물체를 저장하는 contacts
        {
            isGrounded = true; //땅에 닿았음을 표시하는 bool 값
            jump_count = 0; //땅에 닿았으니 점프 카운트 초기화
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
    
}
