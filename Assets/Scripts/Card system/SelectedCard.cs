using UnityEngine;

public class SelectedCard
{
    //������ � �������.
    private Data _data = Data.GetInstance();
    private DataTemp _json = null;

    //������ �� �������� �����.
    private Substrate _card = null;

    public SelectedCard(Substrate card, DataTemp json)
    {
        _json = json;
        _card = card;
    }

    //������ � ��������� ������.
    public void CardItem(TempItem item)
    {

        switch (item.TypeCard)
        {
            default:
                Debug.Log("��� ����� �� ��� ������� � �� ������ ������ ����� ���� �������.");
                _data.Items.cards.Remove(item);
                _card.Destroy();
                break;

            case typeCard.item:
                foreach (TempItem temp in _data.Items.cards)
                    if (temp.Id.Equals(item.Id)) item = temp;

                _json.Balance -= item.AutoPrice;
                _json.PerSecond += item.AutoIncome * item.Bonus;
                item.AutoPrice *= item.PriceUp;
                item.Count++;
                item.IsBay = true;
                _card.Filling(item);
                break;
        }
    }

    //������ � ��������� ������.
    public void CardUpgrade(TempUpgrade upgrade)
    {
        switch (upgrade.TypeCard)
        {
            default:
                Debug.Log("��� ����� �� ��� ������� � �� ������ ������ ����� ���� �������.");
                _data.Upgrades.cards.Remove(upgrade);
                _card.Destroy();
                break;

            case typeCard.upItem:
                if (_data.Items != null)
                {
                    foreach (TempItem temp in _data.Items.cards)
                        if (temp.Id.Equals(upgrade.Id))
                            if (temp.IsBay)
                            {
                                _json.Balance -= upgrade.AutoPrice;
                                double income = temp.AutoIncome * temp.Count;
                                _json.PerSecond -= income;
                                _json.PerSecond += income * upgrade.Percent;
                                temp.Bonus = upgrade.Percent;
                                _data.Upgrades.cards.Remove(upgrade);
                                _card.Destroy();
                            }
                            else return;
                }
                else
                    Debug.Log("�� ������� ���� ������� ������ ���� ����������!");

                break;

            case typeCard.upClick:
                _json.Balance -= upgrade.AutoPrice;
                _json.Click *= upgrade.Percent;
                _card.Destroy();
                break;
        }
    }

    //������ � ��������� ������.
    public void CardAchivment(TempAchivments achivment)
    {
        foreach (TempAchivments temp in _data.Achivments.cards)
            if (temp.Id.Equals(achivment.Id)) achivment = temp;

        if (achivment.IsReceived == false)
        {
            switch (achivment.Income)
            {
                case typeAchivments.balance:
                    _json.Balance += achivment.IncomeUnit;
                    break;
                case typeAchivments.click:
                    _json.Click += achivment.IncomeUnit;
                    break;
                case typeAchivments.perSecond:
                    _json.PerSecond += achivment.IncomeUnit;
                    break;
                case typeAchivments.emerald:
                    _json.Emerald += (int)achivment.IncomeUnit;
                    break;
            }
            achivment.IsReceived = true;
            _card.Filling(achivment);
        }
    }
}
