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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStageImage();
    }

    //unlock ���ο� ���� �������� �̹����� �ٲ�
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

    //�������� Ŭ�� �� stageName�� �ش��ϴ� stage�� �� ��ȯ
    public void OnClickStage(string stageName)
    {
        if (unlocked)
        {
            SceneManager.LoadScene(stageName);
        }
    }
}
