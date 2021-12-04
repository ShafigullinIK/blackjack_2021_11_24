//Игра - финальный вариант
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
void PlayerKart(string[] kartArray, out string[] player1, out string[] player2)
{
    player1 = new string[10];
    for (int i = 0; i < player1.Length; i++)
    {
        player1[i] = "0";
    }
    player2 = new string[10];
    for (int i = 0; i < player1.Length; i++)
    {
        player2[i] = "0";
    }
    player1[0] = kartArray[0];
    player2[0] = kartArray[1];
    player1[1] = kartArray[2];
    player2[1] = kartArray[3];
}
void Print(string[] array, int b)
{
    for (int i = 0; i < array.Length; i++)
    {
        if (array[i] != "0") Console.Write(array[i] + " ");
    }
    Console.WriteLine();
}
int AddKart(string[] kartArray, string[] player, int b, int j)
{
    string a = String.Empty;
    Console.Write($"Игрок {b}, ещё карту надо? Y/N ");

    for (int i = 2; a != "N"; i++)
    {
        a = Console.ReadLine();
        if (a == "y" || a == "Y")
        {
            player[i] = kartArray[j++];
            Console.Write($"Выпала карта: {player[i]}. Ещё? ");
        }
        if (a == "n" || a == "N") break;
    }
    return j;
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
            else if (array[i] == "Туз" || array[i] == "туз")
            {
                sum += 11;
            }
            else sum += 10;
        }
    }
    return sum;
}
string FindWinner(int result1, int result2)
{
    string winner = String.Empty;
    if (result1 == result2) winner = "Пуш";
    if (result1 == 21) winner = "Блек-джек. Игрок 1 победил";
    if (result1 > 21 && result2 < 21) winner = "Перебор, Игрок 2 победил";
    if (result2 == 21) winner = "Блек-джек. Игрок 2 победил";
    if (result2 > 21 && result1 < 21) winner = "Перебор, Игрок 1 победил";
    if (result1 > result2 && result1 != 21 && result1 < 21) winner = "Игрок 1 победил";
    if (result2 > result1 && result2 != 21 && result2 < 21) winner = "Игрок 2 победил";
    return winner;
}
int Answer()
{
    Console.WriteLine("Сколько раундов будем играть?");
    int round = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine($"Играем {round} раунд{Ending(round)}");
    return round;
}
string Ending(int number)
{
    string ending = String.Empty;
    if(number == 1)
    {
        ending = " ";
    }
    else if (number == 2 || number == 3)
    {
        ending = "а";
    }
    else 
    {
        ending = "ов";
    }
    return ending;
}

Console.WriteLine("Сыграем в Black Jack?");
Thread.Sleep(1000);
int round = Answer();
while (round >= 0)
{
    if (round == 0)
    {
        Console.WriteLine("Ещё играем? Y/N ");
        string answer = Console.ReadLine();
        if (answer == "y" || answer == "Y")
        {
            round = Answer();
        }
        if (answer == "n" || answer == "N") break;
    }
    string[] cardDesk = FillCardDesk(cardRank, cardSuit);
    string[] shuffledCardDesk = DeskShuffle(cardDesk, numberOfShuffle);
    string[] playersHand;
    string[] croupierHand;
    int player1 = 1;
    int player2 = 2;
    int j = 4;
    PlayerKart(shuffledCardDesk, out playersHand, out croupierHand);
    Console.Write("Игрок 1 ваши карты: ");
    Print(playersHand, player1);
    Console.Write("Игрок 2 ваши карты: ");
    Print(croupierHand, player2);
    Console.WriteLine();
    j = AddKart(shuffledCardDesk, playersHand, player1, j);
    Console.Write("Итого у игрока 1: ");
    Print(playersHand, player1);
    AddKart(shuffledCardDesk, croupierHand, player2, j);
    Console.Write("Итого у игрока 2:");
    Print(croupierHand, player2);
    int playerResult = GetSumCard(playersHand);
    int croupierResult = GetSumCard(croupierHand);
    Console.WriteLine(playerResult);
    Console.WriteLine(croupierResult);

    string gameResult = FindWinner(playerResult, croupierResult);
    Console.WriteLine(gameResult);
    round--;
    Console.WriteLine($"Осталось {round} раунд{Ending(round)}!");
    Thread.Sleep(2000);
}
