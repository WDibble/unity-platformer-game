using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CinemachineController : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;
    private float originalOrthoSize;
    [SerializeField] private float invisibleOrthoSize;

    // Start is called before the first frame update
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        originalOrthoSize = vcam.m_Lens.OrthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "Level 3")
        {
            return;
        }

        else if (currentSceneName == "Level 4")
        {
            return;
        }

        bool isInvisible = PlayerVisibility.IsInvisible;

        if (isInvisible && vcam.m_Lens.OrthographicSize != invisibleOrthoSize)
        {
            vcam.m_Lens.OrthographicSize = Mathf.Lerp(vcam.m_Lens.OrthographicSize, invisibleOrthoSize, Time.deltaTime * 2);
        }
        else if (!isInvisible && vcam.m_Lens.OrthographicSize != originalOrthoSize)
        {
            vcam.m_Lens.OrthographicSize = Mathf.Lerp(vcam.m_Lens.OrthographicSize, originalOrthoSize, Time.deltaTime * 2);
        }
    }
}