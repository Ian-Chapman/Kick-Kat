using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;

public class CameraController : EventTrigger
{
    private CinemachineFreeLook freeCam;
    public float verticalSens = 0.002f;
    public float horizontalSens = 0.002f;

    private void Awake()
    {
        if (!(Application.platform == RuntimePlatform.Android ||
            Application.platform == RuntimePlatform.IPhonePlayer))
        {
            Destroy(gameObject);
            return;
        }

        freeCam = FindObjectOfType<CinemachineFreeLook>();

        freeCam.m_XAxis.m_InputAxisName = null;
        freeCam.m_YAxis.m_InputAxisName = null;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);

        freeCam.m_XAxis.Value += eventData.delta.x * horizontalSens;
        freeCam.m_YAxis.Value -= eventData.delta.y * verticalSens;
    }
}
