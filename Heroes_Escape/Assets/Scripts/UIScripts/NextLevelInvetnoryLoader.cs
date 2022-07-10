using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class NextLevelInvetnoryLoader : MonoBehaviour
{
    [SerializeField] private SavingSystem savingManager;
    [SerializeField] private bool traningScene = false;
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject InventoryPanel;
    private bool loaded = false;

    void OnEnable()
    {
        RenderPipelineManager.endCameraRendering += RenderPipelineManager_endCameraRendering;
    }
    void OnDisable()
    {
        RenderPipelineManager.endCameraRendering -= RenderPipelineManager_endCameraRendering;
    }
    private void RenderPipelineManager_endCameraRendering(ScriptableRenderContext context, Camera camera)
    {
        OnPostRender();
    }
    private void OnPostRender()
    {
        if(!traningScene)
        {
            if (!loaded)
            {
                PausePanel.SetActive(true);
                InventoryPanel.SetActive(true);
                savingManager.LoadInventory();
                InventoryPanel.SetActive(false);
                PausePanel.SetActive(false);
                loaded = true;
            }

        }
    }
            
}
