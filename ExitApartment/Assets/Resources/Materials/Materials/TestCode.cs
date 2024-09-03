
using UnityEngine;

public class TestCode : MonoBehaviour
{
    public Transform land1,land2;
    public Transform playerRoot, playerCam;
    public Transform portalCam;
    public RenderTexture renderTex;
    // Start is called before the first frame update
    void Start()
    {
        renderTex.width= Screen.width;
        renderTex.height= Screen.height;
    }

    // Update is called once per frame
    void Update()
    {

        //Calculate the offset from land1 to playerCam in local space
        Vector3 localOffset = land1.InverseTransformPoint(playerCam.position);

        // Apply this offset to land2 to position the portalCam
        Vector3 globalOffset = land2.TransformPoint(localOffset);
        portalCam.position = globalOffset;


        float angle = Quaternion.Angle(land1.rotation, land2.rotation);

        Quaternion angleToQuaternion = Quaternion.AngleAxis(angle, Vector3.up);

        Vector3 dir = angleToQuaternion * -playerCam.forward;

        portalCam.rotation = Quaternion.LookRotation(new Vector3(dir.x, -dir.y, dir.z), Vector3.up);


        //// Calculate the rotation offset
        //Quaternion localRotation = Quaternion.Inverse(land1.rotation) * playerCam.rotation;

        //// Apply the rotation to portalCam, considering land2's rotation
        //portalCam.rotation = land2.rotation * localRotation;
    }
}
