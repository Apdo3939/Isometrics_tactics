using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelAdvanceJob : MonoBehaviour
{
    PanelPositioner positioner;
    public List<PanelAdvanceJobInfo> jobInfos;
    public RectTransform selector;
    int index;

    void Awake()
    {
        positioner = GetComponent<PanelPositioner>();
    }

    public void Show()
    {
        index = 0;
        ChangeSelector();
        UpdatePanelInfo();
        positioner.MoveTo("Show");
    }

    public void Hide()
    {
        positioner.MoveTo("Hide");
    }

    public void SelectNext()
    {
        index++;
        if (index >= jobInfos.Count)
        {
            index = 0;
        }
        ChangeSelector();
    }

    public void SelectPrevious()
    {
        index--;
        if (index < 0)
        {
            index = jobInfos.Count - 1;
        }
        ChangeSelector();
    }

    void ChangeSelector()
    {
        selector.position = jobInfos[index].panel.transform.position;
    }

    public void DuplicatePanel()
    {
        Vector3 newPos = jobInfos[0].panel.transform.position;
        newPos.x += 200 * jobInfos.Count;
        GameObject instantiated = Instantiate(jobInfos[0].panel, newPos, Quaternion.identity, transform);
        PanelAdvanceJobInfo jobInfo = instantiated.GetComponent<PanelAdvanceJobInfo>();
        jobInfos.Add(jobInfo);
    }

    public void JobChange()
    {
        Job.Employ
        (
            Turn.unitCharacter,
            Turn.unitCharacter.job.advancesTo[index],
            Turn.unitCharacter.GetStat(StatEnum.LVL)
        );
    }

    void UpdatePanelInfo()
    {
        Clean();
        for (int i = 0; i < Turn.unitCharacter.job.advancesTo.Count; i++)
        {
            Job job = Turn.unitCharacter.job.advancesTo[i];
            jobInfos[i].jobName.text = job.name;
            jobInfos[i].portrait.sprite = job.portrait;
            jobInfos[i].jobDescription.text = job.description;

            if (Turn.unitCharacter.job.advancesTo.Count > jobInfos.Count)
            {
                DuplicatePanel();
            }
        }
    }

    void Clean()
    {
        for (int i = jobInfos.Count - 1; i > 0; i--)
        {
            PanelAdvanceJobInfo info = jobInfos[i];
            jobInfos.Remove(info);
            Destroy(info.panel);
        }
    }

}
