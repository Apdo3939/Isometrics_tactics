using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateMachineController : MonoBehaviour
{
    public static StateMachineController instance;
    State _current;
    bool busy;
    public State current { get { return _current; } }
    public Transform selector;
    public TileLogic selectedTile;
    public List<UnitCharacter> units;

    [Header("ChooseActionState")]
    public List<Image> chooseActionButons;
    public Image chooseActionSelector;
    public PanelPositioner chooseActionPanel;

    [Header("SkillActionState")]
    public List<Image> skillActionButons;
    public Image skillActionSelector;
    public PanelPositioner skillActionPanel;
    public Sprite skillActionBlocked;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ChangeTo<LoadState>();
    }

    public void ChangeTo<T>() where T : State
    {
        State state = GetState<T>();
        if (_current != state) { ChangeState(state); }
    }

    public T GetState<T>() where T : State
    {
        T target = GetComponent<T>();
        if (target == null)
        {
            target = gameObject.AddComponent<T>();
        }
        return target;
    }

    protected virtual void ChangeState(State state)
    {
        if (busy) { return; }

        busy = true;

        if (_current != null) { _current.Exit(); }

        _current = state;
        if (_current != null) { _current.Enter(); }

        busy = false;
    }
}
