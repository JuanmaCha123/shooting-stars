using System.Collections.Generic;
using UnityEngine;

public class EventQueue : MonoBehaviour
{
    private Queue<ICommand> eventQueue = new Queue<ICommand>();

    void Update()
    {
        while (eventQueue.Count > 0)
        {
            ICommand command = eventQueue.Dequeue();
            command.Execute();
        }
    }

    public void EnqueueEvent(ICommand command)
    {
        eventQueue.Enqueue(command);
    }
}