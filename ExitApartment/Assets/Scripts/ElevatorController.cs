using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    /// <summary>
    /// 0 = 왼쪽 1= 오른쪽
    /// </summary>
    [Header("엘리베이터 문"), SerializeField]
    private GameObject[] doors;
    [Header("문 속도"), SerializeField]
    private float speed = 3f;
    private Vector3[] origin = new Vector3[2];
    private bool isOpen = false;
    public bool IsOpen => isOpen;
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public IEnumerator OpenDoor()
    {
        Vector3 lDoor = doors[0].transform.position;
        Vector3 rDoor = doors[1].transform.position;
        while (doors[0].transform.position.x > -0.5f)
        {
            doors[0].transform.position = Vector3.Lerp(lDoor, new Vector3(lDoor.x - 0.1f, lDoor.y, lDoor.z), Time.deltaTime * speed);
            doors[1].transform.position = Vector3.Lerp(rDoor, new Vector3(rDoor.x + 0.1f, rDoor.y, rDoor.z), Time.deltaTime * speed);
            yield return null;
        }
        isOpen = true;
    }


    public IEnumerator CloseDoor()
    {
        while (Vector3.Distance(doors[0].transform.position, origin[0])<0.02f)
        {
            doors[0].transform.position = Vector3.Lerp(doors[0].transform.position, origin[0], Time.deltaTime * speed * 0.3f);
            doors[1].transform.position = Vector3.Lerp(doors[1].transform.position, origin[1], Time.deltaTime * speed * 0.3f);
            yield return null;
        }
        isOpen = false;
    }
    private void Init()
    {
        for(int i =0; i < doors.Length;i++)
        {
            origin[i] = doors[i].transform.position;
        }
    }
}
