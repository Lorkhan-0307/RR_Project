using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelection : MonoBehaviour
{
    [SerializeField]
    private bool unlocked;
    public Image unlockImage;
    public bool selected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStageImage();
    }

    //unlock 여부에 따라 스테이지 이미지가 바뀜
    private void UpdateStageImage()
    {
        if (!unlocked)
        {
            unlockImage.gameObject.SetActive(true);
        }
        else
        {
            unlockImage.gameObject.SetActive(false);
        }
    }

    //스테이지 클릭 시 stageName에 해당하는 stage로 씬 전환
    public void OnClickStage(int stageName)
    {
        if (unlocked)
        {
            FindObjectOfType<Door>().stageNum = stageName;
            selected = true;
        }
    }
}
