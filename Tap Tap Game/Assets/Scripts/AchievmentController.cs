using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievmentController : MonoBehaviour
{
    private static AchievmentController _instance = null;
    public static AchievmentController Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<AchievmentController>();
            }
            return _instance;
        }
    }

    [SerializeField] private Transform _popUpTransform;
    [SerializeField] private Text _popUpText;
    [SerializeField] private float _popUpShowDuration;
    [SerializeField] private List<AchievmentData> _achievmentList;

    private float _popUpShowDurationControl;

    private void Update()
    {
        if(_popUpShowDurationControl > 0)
        {
            _popUpShowDurationControl -= Time.unscaledDeltaTime;
            _popUpTransform.localScale = Vector3.LerpUnclamped(_popUpTransform.localScale, Vector3.one, 0.5f);
        }
        else
        {
            _popUpTransform.localScale = Vector3.LerpUnclamped(_popUpTransform.localScale, Vector3.right, 0.3f);
        }
    }

    public void UnlockAchievment(AchievmentType type, string value)
    {
        AchievmentData achievment = _achievmentList.Find(a => a.Type == type && a.Value == value);

        if (achievment != null && !achievment.isUnlocked)
        {
            achievment.isUnlocked = true;
            ShowAchievmentPopUp(achievment);
        }
        Debug.Log("istrigger");
    }

    private void ShowAchievmentPopUp(AchievmentData achievment)
    {
        _popUpText.text = achievment.Title;
        _popUpShowDurationControl = _popUpShowDuration;
        //_popUpTransform.localScale = Vector2.right;
    }

    [System.Serializable]

    public class AchievmentData
    {
        public string Title;
        public AchievmentType Type;
        public string Value;
        public bool isUnlocked;
    }

    public enum AchievmentType
    {
        UnlockResource
    }
}
