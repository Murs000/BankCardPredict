bool IsValid(string number)
{
    return number
        .Reverse()
        .Where(char.IsDigit)
        .Select(c => (int)char.GetNumericValue(c))
        .Select((n, i) => ((i % 2) == 0) ? n : n * 2)
        .Select(n => n > 9 ? n - 9 : n)
        .Sum() % 10 == 0;
}

int[,] XCoordinate(string number)
{
    int[,] xCoordinate = new int[2, number.Count(c => c == 'X')];
    int Count = 0;
    for (int i = 0; i < number.Length; i++)
    {
        if (number[i] == 'X')
        {
            xCoordinate[0, Count] = i;
            Count++;
        }
    }
    return xCoordinate;
}

string ChangeX(string number, int[,] xCoordinate)
{
    for (int i = 0; i < xCoordinate.GetLength(1); i++)
    {
        number = number.Remove(xCoordinate[0, i], 1).Insert(xCoordinate[0, i], xCoordinate[1, i].ToString());
    }
    return number;
}

int[,] ValueUp(int[,] xCoordinate)
{
    char[] values = new char[xCoordinate.GetLength(1)];
    for (int i = 0; i < values.Length; i++)
    {
        values[i] = Convert.ToChar(xCoordinate[1,i].ToString());
    }
    string value = string.Empty;
    for (int i = 0; i < values.Length; i++)
    {
        value = value.Insert(i, values[i].ToString());
    }
    long intValue = Convert.ToInt64(value);
    intValue++;
    value = intValue.ToString();
    char[] newValues = value.ToCharArray();
    for (int i = 0; i < newValues.Length; i++)
    {
        values[values.Length - i - 1] = newValues[newValues.Length - i - 1];
    }
    for (int i = 0; i < values.Length; i++)
    {
        xCoordinate[1,i] = (int)char.GetNumericValue(values[i]);
    }
    return xCoordinate;
}

List<string> GetValidCards(string cardNumber)
{
    int[,] xCoordinate = XCoordinate(cardNumber);
    List<string> cards = new List<string>();
    for (int i = 0; i < Math.Pow(10, xCoordinate.GetLength(1)) - 1; i++)
    {
        string newCardNumber = ChangeX(cardNumber, xCoordinate);
        if (IsValid(newCardNumber))
        {
            cards.Add(newCardNumber);
            Console.WriteLine(newCardNumber);
            File.AppendAllText(@"C:\Users\asus\Desktop\BankCards", $"{newCardNumber}\n");
        }
        ValueUp(xCoordinate);
    }
    return cards;
}

string cardNumber = Console.ReadLine();
cardNumber = cardNumber.Replace(" ", "");

List<string> validCards=  GetValidCards(cardNumber);
/*foreach(string valisCard in validCards)
{
    Console.WriteLine(valisCard);
}*/