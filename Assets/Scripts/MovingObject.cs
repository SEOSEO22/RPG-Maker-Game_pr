using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public string currentMapName;

    [SerializeField]
    private float moveSpeed;
    Vector3 vector;

    [SerializeField]
    float runSpeed;
    float applyRunSpeed;

    public int walkCount;
    private int currentWalkCount;

    private bool canMove = true;
    private Animator animator;

    private BoxCollider2D boxCollider;
    public LayerMask layerMask;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    IEnumerator MoveCoroutine()
    {
        while (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);
            bool runBool = false;

            if (vector.x != 0)
                vector.y = 0;

            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            RaycastHit2D hit;

            Vector2 start = transform.position;
            Vector2 end = start + new Vector2(vector.x * moveSpeed * walkCount, vector.y * moveSpeed * walkCount);

            boxCollider.enabled = false;
            hit = Physics2D.Linecast(start, end, layerMask);
            boxCollider.enabled = true;

            if (hit.transform != null)
                break;

            animator.SetBool("Walking", true);

            while (walkCount > currentWalkCount)
            {
                //shift키를 누를 시 달리기
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    runBool = true;
                    applyRunSpeed = runSpeed;
                }
                else
                {
                    applyRunSpeed = 0;
                }

                //방향키를 누를 시 캐릭터 이동
                if (vector.x != 0)
                {
                    transform.Translate(vector.x * (moveSpeed + applyRunSpeed), 0, 0);
                }
                else if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * (moveSpeed + applyRunSpeed), 0);
                }

                if (runBool == true)
                    currentWalkCount++;
                currentWalkCount++;
                yield return new WaitForSeconds(0.01f);
            }

            currentWalkCount = 0;
        }

        animator.SetBool("Walking", false);
        canMove = true;
    }

    private void Update()
    {
        if (canMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());
            }
        }
    }
}
