using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Transform _bottomTrunk;
    [SerializeField] private Transform _log;

    private List<Transform> _logs = new List<Transform>();
    public int _points { get; private set; }
    private GameState CurrentGameState;
    public enum GameState
    {
        Playing,
        Menu,
    }

    private void Start()
    {
        Instantiate(_bottomTrunk);
        CreateTree();
        _points = 0;
    }

    private void Update()
    {
        if (CurrentGameState == GameState.Playing && Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (IsDeathMove(mouseWorldPosition))
            {
                CurrentGameState = GameState.Menu;
                DeleteLog(mouseWorldPosition);
                MoveTree();
                TimerBar.Instance.Hide();
                Menu.Instance.Show();
                return;
            }

            DeleteLog(mouseWorldPosition);
            MoveTree();
            _points++;
            UIController.Instance.UpdatePoints(_points);
            TimerBar.Instance.IncreaseTime();
            AddLog();
        }
        if (TimerBar.Instance.IsTimeOut())
        {
            CurrentGameState = GameState.Menu;
            TimerBar.Instance.Hide();
            Menu.Instance.Show();
        }
    }

    private void CreateTree()
    {
        Transform log = Instantiate(_log);
        _logs.Add(log);
        for (int i = 0; i < 10; i++)
        {
            AddLog();
        }
    }

    private void AddLog()
    {
        float yLogPosition = -6.25f + 3 * _logs.Count;
        Transform log = Instantiate(_log, new Vector3(0, yLogPosition, 0), Quaternion.identity);
        _logs.Add(log);
        log.GetComponent<Log>().GenerateBranch(_points);
    }

    private void DeleteLog(Vector3 mousePosition)
    {
        if (mousePosition.x < 0)
        {
            _logs[0].GetComponent<Log>().MoveRight();
            Timberman.Instance.AnimationTrigger("Hit left");
        }
        else
        {
            _logs[0].GetComponent<Log>().MoveLeft();
            Timberman.Instance.AnimationTrigger("Hit right");
        }
        _logs.Remove(_logs[0]);
    }

    private void MoveTree()
    {
        foreach (Transform log in _logs)
        {
            log.GetComponent<Log>().MoveDown();
        }
    }

    private void DestroyTree()
    {
        foreach (Transform log in _logs)
        {
            Destroy(log.gameObject);
        }
        _logs.Clear();
    }

    private bool IsDeathMove(Vector3 mouseWorldPosition)
    {
        if (_logs[1].GetComponent<Log>().BranchOnTheLeft && mouseWorldPosition.x < 0)
        {
            return true;
        }
        else if (_logs[1].GetComponent<Log>().BranchOnTheRight && mouseWorldPosition.x > 0)
        {
            return true;
        }
        return false;
    }

    public void RestartGame()
    {
        _points = 0;
        UIController.Instance.UpdatePoints(_points);
        TimerBar.Instance.Show();
        TimerBar.Instance.Restart();
        DestroyTree();
        CreateTree();
        Menu.Instance.Hide();
        CurrentGameState = GameState.Playing;
    }
}
