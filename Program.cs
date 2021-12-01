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

string RandomSuit(string[] array)
{
    int i = new Random().Next(0, array.Length);
    string text = array[i];
    return text;
}

void PrintCards(string[] array) //string[] arraySuit) // вывод колоды на консоль
{
    for (int j = 0; j < array.Length; j++)
    {
        //string suit = RandomSuit(arraySuit);
        //Console.Write($"{array[j]} {suit}, ");
        Console.Write($"{array[j]}, ");
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
            else if (array[i] == "Туз" || array[i] == "туз")
            {
                sum += 11;
            }
            else sum += 10;
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

string FindWinner(int result1, int result2)
{
    string winner = String.Empty;
    if (result1 == result2) winner = "Пуш";
    if (result1 == 21) winner = "Блек-джек. Игрок победил";
    if (result1 > 21 && result2 < 21) winner = "Перебор, выйграло казино";
    if (result2 == 21) winner = "Игрок проиграл";
    if (result1 > result2 && result1 != 21) winner = "Игрок победил";
    if (result1 < result2) winner = "Игрок проиграл";
    return winner;
}

// Игра
//string[] cardDesk = FillCardDesk(cardRank, cardSuit);

//string[] shuffledCardDesk = DeskShuffle(cardDesk, numberOfShuffle);
//PrintCards(shuffledCardDesk, cardSuit);

//string[] playersHand = CardDrawUser(shuffledCardDesk);
//PrintCards(playersHand);

//string[] secondPlayersHand = PhaseGetCardUser(shuffledCardDesk, playersHand);

//string[] croupierHand = CardDrawСroupier(shuffledCardDesk);
//PrintCards(croupierHand);
/*
string[] secondCroupierHand = PhaseGetCardUser(shuffledCardDesk, croupierHand);

int playerResult = GetSumCard(playersHand);
int croupierResult = GetSumCard(croupierHand);

Console.WriteLine(playerResult);
Console.WriteLine(croupierResult);

//5. Определение победителя в этом раунде.

string gameResult = FindWinner(playerResult, croupierResult);
Console.WriteLine(gameResult);
*/
Console.WriteLine();
Console.WriteLine();
Console.WriteLine();




string[] PhaseGetCardUser(string[] deck, string[] cardUser)
{
    int indexCard = 4;
    PrintCards(cardUser);
    while (indexCard > 0)
    {
        System.Console.Write("Вам нужна еще карта? Введите Yes/No: ");
        string answer = Console.ReadLine();
        if (answer == "Yes" || answer == "yes")
        {
            Array.Resize(ref cardUser, cardUser.Length + 1);
            cardUser[cardUser.Length - 1] = deck[indexCard];
            PrintCards(cardUser);
            indexCard++;
        }
        else break;
    }
    return cardUser;
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

int AddKart(string[] kartArray, string[] player, int b, int j)
{
    string a = String.Empty;
    Console.Write($"Игрок {b}, ещё карту надо? Y/N ");

    for (int i = 2; a != "N"; i++)
    {
        a = Console.ReadLine();
        if (a == "y" || a == "Y")
        {
            //Array.Resize(ref player, player.Length + 1);
            player[i] = kartArray[j++];
            Console.Write($"Выпала карта: {player[i]}. Ещё? ");
        }
        if (a == "n" || a == "N") break;
    }
    return j;
}

void Print(string[] array, int b)
{
    for (int i = 0; i < array.Length; i++)
    {
        if (array[i] != "0") Console.Write(array[i] + " ");
    }
    Console.WriteLine();
}
int round = 2;
while (round > 0)
{
    string[] cardDesk = FillCardDesk(cardRank, cardSuit);
    string[] shuffledCardDesk = DeskShuffle(cardDesk, numberOfShuffle);
    string[] playersHand;
    string[] croupierHand;
    int b1 = 1;
    int b2 = 2;
    int j = 4;
    Console.WriteLine(j);
    PlayerKart(shuffledCardDesk, out playersHand, out croupierHand);
    Console.Write("Игрок 1 ваши карты: ");
    Print(playersHand, b1);
    Console.Write("Игрок 2 ваши карты: ");
    Print(croupierHand, b2);
    Console.WriteLine();
    j = AddKart(shuffledCardDesk, playersHand, b1, j);
    Console.Write("Итого у игрока 1: ");
    Print(playersHand, b1);
    AddKart(shuffledCardDesk, croupierHand, b2, j);
    Console.Write("Итого у игрока 2:");
    Print(croupierHand, b2);
    int playerResult = GetSumCard(playersHand);
    int croupierResult = GetSumCard(croupierHand);
    Console.WriteLine(playerResult);
    Console.WriteLine(croupierResult);

    string gameResult = FindWinner(playerResult, croupierResult);
    Console.WriteLine(gameResult);
    round--;
}





/*
int[] KA = { 5, 7, 3, 9, 4, 2, 10, 8, 1 };
int[] p1, p2;
int b1 = 1;
int b2 = 2;
PlayerKart(KA, out p1, out p2);
Console.Write("Игрок 1 ваши карты: ");
Print(p1, b1);
Console.Write("Игрок 2 ваши карты: ");
Print(p2, b2);
Console.WriteLine();
AddKart(KA, p1, b1);
Console.Write("Итого у игрока 1: ");
Print(p1, b1);
AddKart(KA, p2, b2);
Console.Write("Итого у игрока 2:");
Print(p2, b2);
*/