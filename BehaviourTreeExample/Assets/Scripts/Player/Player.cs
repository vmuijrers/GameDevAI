using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform Camera;
    [SerializeField] private float rotationSpeed = 180f;
    [SerializeField] private float moveSpeed = 3;
    [SerializeField] private float deathForce = 1000;
    [SerializeField] private GameObject ragdoll;
    private Rigidbody rb;
    private Animator animator;
    private float vert = 0;
    private float hor = 0;
    private Vector3 moveDirection;
    private Collider mainCollider;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        mainCollider = GetComponent<Collider>();
        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rib in rigidBodies)
        {
            rib.isKinematic = true;
            rib.useGravity = false;
        }

        var cols = GetComponentsInChildren<Collider>();
        foreach (Collider col in cols)
        {
            if (col.isTrigger) { continue; }
            col.enabled = false;
        }
        mainCollider.enabled = true;
        rb.isKinematic = false;
    }

    // Update is called once per frame
    void Update()
    {
        vert = Input.GetAxis("Vertical");
        hor = Input.GetAxis("Horizontal");
        Vector3 forwardDirection = Vector3.Scale(new Vector3(1, 0, 1), Camera.transform.forward);
        Vector3 rightDirection = Vector3.Cross(Vector3.up, forwardDirection.normalized);
        moveDirection = forwardDirection.normalized * vert + rightDirection.normalized * hor;
        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(moveDirection.normalized, Vector3.up), rotationSpeed * Time.deltaTime);
        }
        transform.position += moveDirection.normalized * moveSpeed * Time.deltaTime;

        bool isMoving = hor != 0 || vert != 0;
        ChangeAnimation(isMoving ? "Walk Crouch" : "Crouch Idle", isMoving ? 0.05f : 0.15f);
    }

    private void FixedUpdate()
    {
        
    }

    public void TakeDamage(GameObject attacker, int damage)
    {
        animator.enabled = false;
        var cols = GetComponentsInChildren<Collider>();
        foreach (Collider col in cols)
        {
            col.enabled = true;
        }
        mainCollider.enabled = false;

        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rib in rigidBodies)
        {
            rib.isKinematic = false;
            rib.useGravity = true;
            rib.AddForce(Vector3.Scale(new Vector3(1,0.5f,1),(transform.position - attacker.transform.position).normalized * deathForce));
        }
        ragdoll.transform.SetParent(null);

        gameObject.SetActive(false);
    }

    private void GetComponentsRecursively<T>(GameObject obj, ref List<T> components)
    {
        T component = obj.GetComponent<T>();
        if(component != null)
        {
            components.Add(component);
        }
        foreach(Transform t in obj.transform)
        {
            if(t.gameObject == obj) { continue; }
            GetComponentsRecursively<T>(t.gameObject, ref components);
        }
    }

    private void ChangeAnimation(string animationName, float fadeTime)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName(animationName) && !animator.IsInTransition(0))
        {
            animator.CrossFade(animationName, fadeTime);
        }
    }
}
