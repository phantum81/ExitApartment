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

    public EElevatorWork eleWork = EElevatorWork.Closing;

    private Coroutine curCoroutine;
    public Coroutine CurCoroutine { get; set; }
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
        
        float elapsedTime = 0f;
        float duration = 2f;        
        yield return new WaitForSeconds(0.5f);
        while (elapsedTime < duration)
        {
            doors[0].transform.position = Vector3.Lerp(doors[0].transform.position, origin[0] + Vector3.left * 0.8f, (elapsedTime / duration) * Time.fixedDeltaTime * speed);
            doors[1].transform.position = Vector3.Lerp(doors[1].transform.position, origin[1] + Vector3.right * 0.8f, (elapsedTime / duration) * Time.fixedDeltaTime * speed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }        
    }

    public IEnumerator CloseDoor()
    {
        float elapsedTime = 0f;
        float duration = 2f;
        yield return new WaitForSeconds(0.5f);
        while (elapsedTime < duration)
        {

            doors[0].transform.position = Vector3.Lerp(doors[0].transform.position, origin[0], (elapsedTime / duration) * Time.fixedDeltaTime * speed);
            doors[1].transform.position = Vector3.Lerp(doors[1].transform.position, origin[1], (elapsedTime / duration) * Time.fixedDeltaTime * speed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
    }
    private void Init()
    {
        for(int i =0; i < doors.Length;i++)
        {
            origin[i] = doors[i].transform.position;
        }
    }
}
