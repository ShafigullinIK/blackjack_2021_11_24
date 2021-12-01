/* Задачи
1. Создаем колоду карт.
2, Для игры нам необходима перемешанная колода, поэтому надо в случайном порядке 
перетасовать карты (или сделать какой-то механизм, чтобы менять порядок карт)
*/
int numberOfShuffle = 100;
string[] cardSuit = new string[] { "треф", "пик", "буби", "червы" };
string[] cardRank = new string[]
{"туз", "2", "3", "4", "5", "6", "7",
 "8", "9", "10", "валет", "дама", "король"};

string[] FillCardDesk(string[] rank, string[] suit)
{
    string[] shuffledDesk = new string[rank.Length * suit.Length];
    int index = 0;
    for (int j = 0; j < suit.Length; j++)
    {
        for (int i = 0; i < rank.Length; i++)
        {
            if (index < shuffledDesk.Length)
            {
                shuffledDesk[index] = $"{rank[i]}";
                index++;
            }
        }
    }
    return shuffledDesk;
}

string[] DeskShuffle(string[] array, int number)
{
    string temp = String.Empty;
    string temp2 = String.Empty;
    while (number != 0)
    {
        int i = new Random().Next(0, array.Length);
        temp = array[i];
        int l = new Random().Next(0, array.Length);
        temp2 = array[l];
        if (temp != temp2)
        {
            array[i] = temp2;
            array[l] = temp;
            number = number - 2;
        }
    }
    return array;
}

void PrintCards(string[] array) // вывод колоды на консоль
{
    for (int j = 0; j < array.Length; j++)
    {
        Console.Write($"{array[j]} ");
    }
    Console.WriteLine();
}

int GetSumCard(string[] array)
{
    int sum = 0;
    for (int i = 0; i < array.Length; i++)
    {
        for (int j = 0; j < 1; j++)
        {
            if (Char.IsDigit(array[i][j]) == true)
            {
                sum += Convert.ToInt32(array[i]);
            }
            else if (array[i][j] != 'т')
            {
                sum += 10;
            }
            else sum += 11;
        }
    }
    return sum;
}

string[] CardDrawUser(string[] deck)
{
    string[] cardUser = new string[2];
    for (int i = 0; i < cardUser.Length; i++) 
    {
        cardUser[i] = deck[i];
    }
    return cardUser;
}
string[] CardDrawСroupier(string[] deck)
{
    string[] cardСroupier = new string[2];
    int j = 2;
    for (int i = 0; i < cardСroupier.Length; i++)
    {
        cardСroupier[i] = deck[j++];
    }
    return cardСroupier;
}

string[] cardDesk = FillCardDesk(cardRank, cardSuit);
//PrintImage(cardDesk);
//Console.WriteLine();

string[] shuffledCardDesk = DeskShuffle(cardDesk, numberOfShuffle);
PrintCards(shuffledCardDesk);

string[] playersHand = CardDrawUser(shuffledCardDesk);
string[] croupierHand = CardDrawСroupier(shuffledCardDesk);

int playerResult = GetSumCard(playersHand);
int croupierResult = GetSumCard(croupierHand);

Console.WriteLine(playerResult);
Console.WriteLine(croupierResult);


