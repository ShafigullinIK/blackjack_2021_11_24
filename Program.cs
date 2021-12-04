using static System.Console;
using static System.Convert;
using System;

int NumInput()
{
    int number = 0;
    if ((int.TryParse(ReadLine(), out int input) && input > 0))
    number = input;
    else NumInput();
return number;
}

int[,] deckPoints = new int[4, 13]
{
{2,     3,  4,  5,  6,  7,  8,  9,  10,     10,     10,     10,     1},
{2,     3,  4,  5,  6,  7,  8,  9,  10,     10,     10,     10,     1},
{2,     3,  4,  5,  6,  7,  8,  9,  10,     10,     10,     10,     1},
{2,     3,  4,  5,  6,  7,  8,  9,  10,     10,     10,     10,     1},
};

string[,] deckNames = new string[,]
{
{"Трефы 2", "Трефы 3",  "Трефы 4",  "Трефы 5",  "Трефы 6",  "Трефы 7",  "Трефы 8",  "Трефы 9",  "Трефы 10", "Трефы Валет",  "Трефы Дама",   "Трефы Король", "Трефы Туз"},
{"Пики 2",  "Пики 3",   "Пики 4",   "Пики 5",   "Пики 6",   "Пики 7",   "Пики 8",   "Пики 9",   "Пики 10",  "Пики Валет",   "Пики Дама",    "Пики Король",  "Пики Туз"},
{"Червы 2", "Червы 3",  "Червы 4",  "Червы 5",  "Червы 6",  "Червы 7",  "Червы 8",  "Червы 9",  "Червы 10", "Червы Валет",  "Червы Дама",   "Червы Король", "Червы Туз"},
{"Бубны 2", "Бубны 3",  "Бубны 4",  "Бубны 5",  "Бубны 6",  "Бубны 7",  "Бубны 8",  "Бубны 9",  "Бубны 10", "Бубны Валет",  "Бубны Дама",   "Бубны Король", "Бубны Туз"},
};


int[,] MakeShoe(int[,] deckPoints, int deckQuan) //Ботинок - это общая база, к которой будут обращаться другие методы. Ботинок представляет собой ёмкость (реальный предмет обуви или машинка в казино или что-то другое того же типа), из которой раздаются карты игрокам.
{
    int[,] shoe = new int[(deckPoints.GetLength(0) * deckQuan), deckPoints.GetLength(1)];

    for (int i = 0; i < deckQuan; i++)
    {
        Array.Copy(deckPoints, 0, shoe, (deckPoints.Length * i), 52);
    }
    return shoe;
}

WriteLine();
WriteLine("Добро пожаловать в BlackJack!");
WriteLine("Сколько колод будет в игре? Введите число не меньше 1");

WriteLine();
WriteLine("Каков ваш счет на начало игры? Введите число не меньше 10");

int credit = NumInput();
if (credit < 10) credit = NumInput();

PlayAgain:
WriteLine();
WriteLine("Каковы ставки в текущем раунде?");

int bet = NumInput();
if (bet > credit) bet = NumInput();

int deckQuan = NumInput();

int[,] shoe = MakeShoe(deckPoints, deckQuan);

void Card(int[,] shoe, int[] hand, int suitIn, int valueIn)
{
    hand[suitIn] = new Random().Next(0, shoe.GetLength(0)); //в элемент массива hand под индексом suitIn кладется рандомное значение масти.
    hand[valueIn] = new Random().Next(0, shoe.GetLength(1)); //в элемент массива hand под индексом valueIn кладется рандомное значение достоинства.
}

string CardName(int suitIn, int valueIn)
{
    string cardName = String.Empty;
    if (suitIn > 3) cardName = deckNames[(suitIn % 4), valueIn];
    else cardName = deckNames[suitIn, valueIn];
    return cardName;
}

int[] GiveCard(int[] hand, int[,] shoe)
{
    Array.Resize(ref hand, hand.Length + 2);
    Card(shoe, hand, hand.Length - 2, hand.Length - 1); //сначала (слева направо) кладем в метод масть, потом достоинство.
    return hand;
}


WriteLine();
WriteLine("Игра начинается. Карта дилера:");
int[] dealerHand = new int[0];
int dealerPoints = 0;

dealerHand = GiveCard(dealerHand, shoe);
WriteLine(CardName(dealerHand[dealerHand.Length - 2], dealerHand[dealerHand.Length - 1]));
dealerPoints = dealerPoints + shoe[dealerHand[dealerHand.Length - 2], dealerHand[dealerHand.Length - 1]];
WriteLine($"Общее количество очков: {dealerPoints}");

WriteLine();


WriteLine("Игрок, ваши карты:");
int[] playerHand = new int[0];
int playerPoints = 0;

playerHand = GiveCard(playerHand, shoe);
WriteLine(CardName(playerHand[playerHand.Length - 2], playerHand[playerHand.Length - 1]));
playerPoints = playerPoints + shoe[playerHand[playerHand.Length - 2], playerHand[playerHand.Length - 1]];

playerHand = GiveCard(playerHand, shoe);
WriteLine(CardName(playerHand[playerHand.Length - 2], playerHand[playerHand.Length - 1]));


int bj = 21;

if (( //player BlackJack
    (playerPoints == 10
    && shoe[playerHand[playerHand.Length - 2], playerHand[playerHand.Length - 1]] == 1)
|| (playerPoints == 1 
    && shoe[playerHand[playerHand.Length - 2], playerHand[playerHand.Length - 1]] == 10))
//dealer BlackJack (supposition)
&& !((dealerPoints == 10 || dealerPoints == 1) 
))
{
    WriteLine();
    WriteLine("Blackjack!");
    playerPoints = bj;
    WriteLine($"Общее количество очков: {playerPoints}");
    WriteLine($"Поздравляем с победой и тройным выигрышем! Ваш счет: {credit = credit + bet * 3}");
    WriteLine();
    goto Question;
}

if (((playerPoints == 10 //player BlackJack
    && shoe[playerHand[playerHand.Length - 2], playerHand[playerHand.Length - 1]] == 1)
 || (playerPoints == 1 
    && shoe[playerHand[playerHand.Length - 2], playerHand[playerHand.Length - 1]] == 10) 

&& (dealerPoints == 10 || dealerPoints == 1)
))
{
    WriteLine();
    WriteLine("Blackjack!");
    playerPoints = bj;
    WriteLine($"Общее количество очков: {playerPoints}");

    WriteLine("Если у дилера будет 21 очко, вы потеряете ставку. Продолжить игру? Введите да или нет");
    Request:
    string? answer = ReadLine();

    switch (answer)
    {
        case "да":
            {
                WriteLine();
                WriteLine("Вторая карта дилера");
                dealerHand = GiveCard(dealerHand, shoe);
                WriteLine(CardName(dealerHand[dealerHand.Length - 2], dealerHand[dealerHand.Length - 1]));

                if (dealerPoints == 10 && shoe[dealerHand[dealerHand.Length - 2], dealerHand[dealerHand.Length - 1]] == 1
                 || dealerPoints == 1 && shoe[dealerHand[dealerHand.Length - 2], dealerHand[dealerHand.Length - 1]] == 10)
                {
                    WriteLine();
                    WriteLine("К сожалению, Blackjack.");
                    dealerPoints = bj;
                    WriteLine($"Общее количество очков: {dealerPoints}");
                    WriteLine($"Вы проиграли. Ваш счет: {credit = credit - bet}");
                    goto Question;
                }
                else
                {
                    dealerPoints = dealerPoints + shoe[dealerHand[dealerHand.Length - 2], dealerHand[dealerHand.Length - 1]];
                    if (dealerPoints < 17) goto DealerCards;
                    else goto Results;
                }

            }
        case "нет":
            {
                    WriteLine($"Поздравляем с победой! Лучше синица в руках, чем журавль в небе, не так ли? Ваш счет: {credit = credit + bet}");
                    goto Question;
            }
        default: goto Request;
    }
}
else
{
    playerPoints = playerPoints + shoe[playerHand[playerHand.Length - 2], playerHand[playerHand.Length - 1]];
    WriteLine($"Общее количество очков: {playerPoints}");
}


HitorStand:
WriteLine();
WriteLine("Карту? Введите да или нет");
string? answer1 = ReadLine();
switch (answer1)
{
    case "да":
        {
            WriteLine();
            playerHand = GiveCard(playerHand, shoe);
            WriteLine(CardName(playerHand[playerHand.Length - 2], playerHand[playerHand.Length - 1]));
            playerPoints = playerPoints + shoe[playerHand[playerHand.Length - 2], playerHand[playerHand.Length - 1]];
            WriteLine($"Общее количество очков: {playerPoints}");
            if (playerPoints > 21) 
            {
                WriteLine("Перебор.");
                goto DealerCards;
            }
            else goto HitorStand;
        }
    case "нет": goto DealerCards;

    default: goto HitorStand;
}

DealerCards:
WriteLine();
WriteLine("Дилер раздает себе, пока у него не будет 17 очков и более");
do
{
    dealerHand = GiveCard(dealerHand, shoe);
    WriteLine(CardName(dealerHand[dealerHand.Length - 2], dealerHand[dealerHand.Length - 1]));
    dealerPoints = dealerPoints + shoe[dealerHand[dealerHand.Length - 2], dealerHand[dealerHand.Length - 1]];
} while (dealerPoints <= 17);
WriteLine($"Общее количество очков: {dealerPoints}");


Results:
WriteLine();
WriteLine("Подводим итоги:");
if (playerPoints == bj && (dealerPoints > 21 || dealerPoints < 21)) WriteLine($"Поздравляем с победой и тройным выигрышем! Ваш счет: {credit = credit + bet * 3}");
if (playerPoints > 21 && dealerPoints > 21 || playerPoints == dealerPoints) WriteLine($"Ничья. Ваш счет: {credit}");
if (playerPoints <= 21 && dealerPoints > 21) WriteLine($"Поздравляем с победой! Ваш счет: {credit = credit + bet}");
if (playerPoints <= 21
    && dealerPoints <= 21
    && dealerPoints < playerPoints) WriteLine($"Поздравляем с победой! Ваш счет: {credit = credit + bet}");
if (playerPoints <= 21
    && dealerPoints <= 21
    && dealerPoints > playerPoints) WriteLine($"К сожалению, вы проиграли. Ваш счет: {credit = credit - bet}");

Question:
WriteLine("Сыграем ещё раз? Введите да или нет");
string? answer2 = ReadLine();
switch (answer2)
{
    case "да": goto PlayAgain;
    case "нет": break;
    default: goto Question;
}