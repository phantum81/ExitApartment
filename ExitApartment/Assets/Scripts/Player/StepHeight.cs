using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepHeight : MonoBehaviour
{
    public float stepHeight = 0.1f;   // 올라갈 수 있는 최대 단차 높이
    public float stepSmoothSpeed = 5f;  // 단차를 올라갈 때의 속도
    
    private LayerMask expectLayer = (1 << 7) | (1 << 8)| (1<<9);
    public float limitHeightMultiply = 0.3f;
    private RaycastHit hitLower;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StepHeightMove(Rigidbody _rigd, Vector3 _inputdir)
    {
        //RaycastHit limitHitted;
        Vector3 limitHeight = transform.position + Vector3.up * limitHeightMultiply;
        // 발 높이에서 단차 감지
        if (Physics.Raycast(transform.position, _inputdir, out hitLower, 0.2f, ~expectLayer))
        {
            if(Physics.Raycast(limitHeight, _inputdir, 0.3f, ~expectLayer))
            {
                return;
            }
            

            // 단차의 높이를 기준으로 목표 높이 계산
            float targetHeight = hitLower.point.y + stepHeight;

            // 자연스럽게 목표 높이로 이동
            Vector3 targetPosition = new Vector3(_rigd.position.x, Mathf.Lerp(_rigd.position.y, targetHeight, Time.fixedDeltaTime * stepSmoothSpeed), _rigd.position.z);

            // Rigidbody의 위치 업데이트
            _rigd.MovePosition(targetPosition);

        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Gizmo 색상 설정

        // RayOrigin에서 Forward 방향으로 Raycast를 그림
        Gizmos.DrawRay(transform.position, transform.forward * 0.2f);

        Gizmos.color = Color.red; // Gizmo 색상 설정

        // RayOrigin에서 Forward 방향으로 Raycast를 그림
        Gizmos.DrawRay(transform.position + Vector3.up * limitHeightMultiply, transform.forward * 0.3f);

        // Ray가 충돌한 위치에 구를 그림
        //if (hitLower.collider != null)
        //{
        //    Gizmos.color = Color.green;  // 충돌된 위치는 녹색으로 표시
        //    Gizmos.DrawSphere(hitLower.point, 0.05f);  // 충돌된 지점에 작은 구 그리기
        //}
    }
}
