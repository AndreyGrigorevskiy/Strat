using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Image barImage;
    public Text barText;
    public Gradient barColor;

    private Transform _anchor;
    private Camera _camera;
    private EntityEvents _entity;

    private void Awake()
    {
        _camera = Camera.main;
    }

    public void Initialize(Transform anchor, EntityEvents entity)
    {
        _anchor = anchor;
        transform.position = _camera.WorldToScreenPoint(_anchor.position);

        _entity = entity;

        barText.text = $"{_entity.health} /{ _entity.maxHealth}";
        barImage.color = barColor.Evaluate(1);
    }

    public void UpdateBar()
    {
        barText.text = $"{_entity.health} /{ _entity.maxHealth}";

        var persentHp = ((_entity.health * 100) / _entity.maxHealth);

        barImage.fillAmount = persentHp;
        barImage.color = barColor.Evaluate(persentHp);
    }

}
