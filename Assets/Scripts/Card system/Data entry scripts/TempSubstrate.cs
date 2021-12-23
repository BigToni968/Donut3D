using UnityEngine;

[System.Serializable]
public class TempSubstrate
{
    [Header("Спрайт для замены обложки.")]
    [SerializeField] private Sprite _substrate = null;
    [Header("Спрайт для замены иконки.")]
    [SerializeField] private Sprite _icon = null;
    [Header("Название строения.")]
    [SerializeField] private string _name = "Повар";
    [Header("Мини описание.")]
    [SerializeField] private string _toolTip = "Короткая информация.";
    [Header("Строковой id строения. Кирилицу не применять!")]
    [SerializeField] private string _id = "Povar";
    [Header("Аббревиатура. Каждая ячейка,это 3 нуля,кроме первой. Цена: ")]
    [SerializeField] private Abbreviations _abbreviationsPrice;
    [Range(1, 999)]
    [SerializeField] private int _price = 0;
    [Header("Аббревиатура. Каждая ячейка,это 3 нуля,кроме первой. Доход: ")]
    [SerializeField] private Abbreviations _abbreviationsIncome;
    [Range(1, 999)]
    [SerializeField] private int _income = 0;
    [Header("Полное описание.")]
    [TextArea(5, 10)]
    [SerializeField] private string _description = "Более полное описание";
}

