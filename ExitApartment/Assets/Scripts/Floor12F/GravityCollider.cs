using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityCollider : MonoBehaviour
{
    

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")|| other.CompareTag("Item"))
        {
            GameManager.Instance.ChangeStageState(1); // 중력변환
        }


    }

}
