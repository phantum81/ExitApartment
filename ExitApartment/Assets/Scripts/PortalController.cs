
using UnityEngine;


public class PortalController : MonoBehaviour
{
    private Transform playerCamera;  // 플레이어의 카메라
    public Transform portal;
    public Transform otherPortal;
    private Transform portalCamera;  // 포탈 너머를 바라보는 카메라 트랜스폼
    public Transform translateTarget;
    private Transform player;
    public RenderTexture renderTexture;

    

    public float viewValue = 1.2f;



    private void Start()
    {
        playerCamera = Camera.main.transform;
        portalCamera = this.transform;
        player = GameManager.Instance.unitMgr.PlayerCtr.Player;
        renderTexture.width = Screen.width;
        renderTexture.height = Screen.height;
        portal.parent.gameObject.SetActive(false);

    }
    void Update()
    {

        // Calculate the rotation offset
        Quaternion localRotation = Quaternion.Inverse(portal.rotation) * playerCamera.rotation;

        // Apply the rotation to portalCam, considering land2's rotation
        portalCamera.rotation = otherPortal.rotation * localRotation;



        //Calculate the offset from land1 to playerCam in local space
        Vector3 localOffset = portal.InverseTransformPoint(playerCamera.position);

        // Apply this offset to land2 to position the portalCam
        Vector3 globalOffset = otherPortal.TransformPoint(localOffset);

        portalCamera.position = globalOffset;

    }
    public void Teleport()
    {
        player.position = new Vector3(translateTarget.position.x, player.position.y, translateTarget.position.z);
        GameManager.Instance.unitMgr.PlayerCtr.RotateModify(180f);
        player.rotation = Quaternion.Euler (new Vector3(translateTarget.rotation.x, 180f, translateTarget.rotation.z));
        playerCamera.position = new Vector3(translateTarget.position.x, playerCamera.position.y, translateTarget.position.z);
        playerCamera.rotation = Quaternion.Euler(new Vector3(translateTarget.rotation.x, 180f, translateTarget.rotation.z));
    }

    public void ShowThePortal()
    {
        portal.parent.gameObject.SetActive(true);
    }
}
