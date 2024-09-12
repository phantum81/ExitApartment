using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepHeight : MonoBehaviour
{
    public float stepHeight = 0.1f;   // �ö� �� �ִ� �ִ� ���� ����
    public float stepSmoothSpeed = 5f;  // ������ �ö� ���� �ӵ�
    
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
        // �� ���̿��� ���� ����
        if (Physics.Raycast(transform.position, _inputdir, out hitLower, 0.2f, ~expectLayer))
        {
            if(Physics.Raycast(limitHeight, _inputdir, 0.3f, ~expectLayer))
            {
                return;
            }
            

            // ������ ���̸� �������� ��ǥ ���� ���
            float targetHeight = hitLower.point.y + stepHeight;

            // �ڿ������� ��ǥ ���̷� �̵�
            Vector3 targetPosition = new Vector3(_rigd.position.x, Mathf.Lerp(_rigd.position.y, targetHeight, Time.fixedDeltaTime * stepSmoothSpeed), _rigd.position.z);

            // Rigidbody�� ��ġ ������Ʈ
            _rigd.MovePosition(targetPosition);

        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Gizmo ���� ����

        // RayOrigin���� Forward �������� Raycast�� �׸�
        Gizmos.DrawRay(transform.position, transform.forward * 0.2f);

        Gizmos.color = Color.red; // Gizmo ���� ����

        // RayOrigin���� Forward �������� Raycast�� �׸�
        Gizmos.DrawRay(transform.position + Vector3.up * limitHeightMultiply, transform.forward * 0.3f);

        // Ray�� �浹�� ��ġ�� ���� �׸�
        //if (hitLower.collider != null)
        //{
        //    Gizmos.color = Color.green;  // �浹�� ��ġ�� ������� ǥ��
        //    Gizmos.DrawSphere(hitLower.point, 0.05f);  // �浹�� ������ ���� �� �׸���
        //}
    }
}
