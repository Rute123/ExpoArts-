﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Scripts;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PortalListController : MonoBehaviour
{
    [SerializeField] private GameObject modelExpoPrefab;
    [SerializeField] private GameObject emptyExpoPrefab;
   
    [SerializeField] private RectTransform portalSelectorParent;

    [SerializeField] private PortalSelector portalSelectorPrefab;

    [SerializeField] private Text selectedPortalNameText;

    [SerializeField] private Button portalSelectorButton;

    public PortalSelector SelectedPortal;

    [SerializeField] private float baseWidth = 5f;
    [SerializeField] private float baseHeight = 5f;

    private VisualizerArController visualizer;

    [SerializeField] private Text debugText;

    private int qtdPortals;

    private IEnumerable<PortalSelector> portals;
    // Start is called before the first frame update
    private void OnEnable()
    {
        debugText.text = GameDataManager.gameData.portals.Count.ToString();
        qtdPortals = GameDataManager.gameData.portals.Count;
        visualizer = GetComponent<VisualizerArController>();        
        LoadPortals();
    }

    private void LoadPortals()
    {
        portals = GetPortalList();
        portals.First().Select();

        var localPosition = portalSelectorParent.localPosition;
        localPosition = new Vector3(
            qtdPortals * 0.5f * portalSelectorPrefab.gameObject.GetComponent<RectTransform>().rect.width, 
            localPosition.y,
            localPosition.z);
        portalSelectorParent.localPosition = localPosition;
    }

    private IEnumerable<PortalSelector> GetPortalList()
    {
        var portals = new List<PortalSelector>();
        var firstInstance = Instantiate(portalSelectorPrefab, portalSelectorParent);
        portals.Add(firstInstance);
        firstInstance.name = "Exposição modelo";
        firstInstance.PortalName = selectedPortalNameText;
        firstInstance.PortalListController = this;
        firstInstance.portalData = new Portal(baseWidth, baseHeight);
        firstInstance.portalPrefab = modelExpoPrefab;
        
        for (var i = 0; i < qtdPortals; i++)
        {
            var instantiatedObject = Instantiate(portalSelectorPrefab, portalSelectorParent);
            portals.Add(instantiatedObject);
            instantiatedObject.name = $"Portal: {i + 1}";
            instantiatedObject.PortalName = selectedPortalNameText;
            instantiatedObject.PortalListController = this;
            instantiatedObject.portalData = GameDataManager.gameData.portals[i];
            instantiatedObject.portalPrefab = emptyExpoPrefab;
        }
        
        return portals;
    }

    public void SelectPortal()
    {
        visualizer.SetPrefab(SelectedPortal.portalPrefab);
        visualizer.selectedPortal = SelectedPortal.portalData;
    }

    public void Reset()
    {
        foreach (var portal in portals)
        {
            Destroy(portal);
        }
    }

}
