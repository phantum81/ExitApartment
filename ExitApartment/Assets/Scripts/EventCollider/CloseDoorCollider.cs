using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CloseDoorCollider : MonoBehaviour, ISOEventContect
{
    public UnityEvent onClose;
    [SerializeField]
    private ElevatorController elevatorCtr;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void OnContect(ESOEventType _type)
    {
        elevatorCtr.StopCoroutine(elevatorCtr.CurCoroutine);
        elevatorCtr.eleWork = EElevatorWork.Closing;
        onClose.Invoke();
        transform.gameObject.SetActive(false);
        

    }
}
