using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum Event
{
    Seven_Clock, BreakFast_Finish, Class_Finish,
    Lunch_Finish, Still_Early, Too_Late
};
public class Player : MonoBehaviour
{
    public StateBase state;
    public GameObject statesKeeper;
    public Dropdown dropdown;
    public bool someEventTriggered = false;
    public void SomeEventTriggered() {
        someEventTriggered = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        state = GetState<SleepingState>();
        state.Enter(this);

        //initialize the dropdown menu
        dropdown.ClearOptions();
        List<string> opt = new List<string>();
        for(int i = 0; i < 6; i++)
        {
            opt.Add(((Event)i).ToString());
        }
        dropdown.AddOptions(opt);
    }

    // Update is called once per frame
    void Update()
    {
        state.HandleInput(this);
    }
    public T GetState<T>() { return statesKeeper.GetComponent<T>(); }
}
