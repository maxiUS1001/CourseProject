using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimationManager : MonoBehaviour, IPointerClickHandler
{
    Animator anim;

    Tasks tasksScript;

    public bool canPress;
    public bool canOpen;
    public bool canSwitch;
    public bool canStartHeating;
    public bool task2Done;
    public bool task3Done;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        tasksScript = GameObject.Find("TaskManager").GetComponent<Tasks>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        if (name == "Door" && canOpen)
        {
            var doorState = !anim.GetBool("IsOpen");
            anim.SetBool("IsOpen", doorState);
            canOpen = false;

            if (!task3Done)
            {
                task3Done = true;
                tasksScript.Task3();
            }

            if (tasksScript.task9Done)
            {
                tasksScript.Task10();
            }
        }
        if (name == "Box015" && canPress) // switch
        {
            var switchState = !anim.GetBool("IsPressed");
            anim.SetBool("IsPressed", switchState);
            canPress = false;
            
            if (task2Done)
            {
                tasksScript.Task9();
            }
            else
            {
                task2Done = true;
                tasksScript.Task2();
            }
            
        }
        if (name == "Sphere001" && canSwitch) // toggle
        {
            var toggleState = !anim.GetBool("IsSwitched");
            anim.SetBool("IsSwitched", toggleState);
            canSwitch = false;
            tasksScript.Task5();
        }
        if (name == "Line001" && canStartHeating)
        {
            var heatingState = !anim.GetBool("IsHeating");
            anim.SetBool("IsHeating", heatingState);
            canStartHeating = false;
        }
    }
}
