using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    [SerializeField] private List<Transform> _leftBranch;
    [SerializeField] private List<Transform> _rightBranch;
    private bool _moving;
    private Vector3 _target;
    private float _speed = 70f;
    private BranchSide _branchSide = BranchSide.Clear;
    public enum BranchSide
    {
        Left,
        Right,
        Clear
    }
    public bool BranchOnTheRight => _branchSide == BranchSide.Right;
    public bool BranchOnTheLeft => _branchSide == BranchSide.Left;

    public void GenerateBranch(int points)
    {
        int i = Random.Range(0, 500 - points);
        if (i < 100 && i >= 1)
        {
            Instantiate(_leftBranch[0], transform);
            _branchSide = BranchSide.Left;
        } 
        else if (i >= 100 && i < 200)
        {
            Instantiate(_rightBranch[0], transform);
            _branchSide = BranchSide.Right;
        }
        
    }

    public void MoveDown()
    {
        if (_moving)
        {
            transform.position = _target;
        }
        _target = transform.position + new Vector3(0, -3, 0);
        _moving = true;
    }

    public void MoveRight()
    {
        Rigidbody2D rigidBody2D = transform.GetComponent<Rigidbody2D>();
        rigidBody2D.simulated = true;
        rigidBody2D.velocity = new Vector2(25f, 30f);
        rigidBody2D.angularVelocity = 540f;
        Destroy(gameObject, 2f);

        ChangeSpriteRendererLayer(1);
    }

    public void MoveLeft()
    {
        Rigidbody2D rigidBody2D = transform.GetComponent<Rigidbody2D>();
        rigidBody2D.simulated = true;
        rigidBody2D.velocity = new Vector2(-25f, 30f);
        rigidBody2D.angularVelocity = -540f;
        Destroy(gameObject, 2f);

        ChangeSpriteRendererLayer(1);
    }

    private void Start()
    {
        _moving = false;
    }

    private void Update()
    {
        if (_moving)
        {
            Move(_target);
        }
    }

    private void Move(Vector3 target)
    {
        Vector3 translate = (target - transform.position).normalized * _speed * Time.deltaTime;
        if (translate.magnitude > (target - transform.position).magnitude)
        {
            translate = target - transform.position;
            _moving = false;
        }
        transform.Translate(translate, Space.World);
    }

    private void ChangeSpriteRendererLayer(int orderInLayer)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer;
        }
    }
}
