//
// в игре используется колода обычных игральных карт - 36 карт
//
Console.WriteLine("Welcome to Black Jack");
Console.WriteLine("");
Console.WriteLine("Player should take cards from the deck one by one");
Console.WriteLine("than second player plays his game and finally result will be shown");

int[] playerSet1 = { 0 };
int[] playerSet2 = { 0 };
int[] suitNum = { 2, 3, 4, 6, 7, 8, 9, 10, 11 };

// создание колоды карт
int[] originalGameDeck = new int[36];
originalGameDeck = CreateDeck(suitNum, originalGameDeck);

//создание текущей колоды карт
int[] curGameDeck = new int[36];
curGameDeck = CreateDeck(suitNum, curGameDeck);
curGameDeck = shuffleDeck(curGameDeck);
//игра игрока 1
playerSet1 = MakeMove(1, curGameDeck, originalGameDeck);

//удаление карт вытащенных первым игроком
int[] newcurGameDeck = new int[originalGameDeck.Length - playerSet1.Length];
newcurGameDeck = RemoveCards(playerSet1, curGameDeck);

//игра игрока 2
playerSet2 = MakeMove(2, newcurGameDeck, originalGameDeck);

// подведение итогов игры
ResultDec(playerSet1, playerSet2);


// метод подведения итогов
void ResultDec(int[] playerSet1, int[] playerSet2)
{
  int points1 = 0;
  int points2 = 0;
  int bjack = 21;

  for (int i = 0; i < playerSet1.Length; i++)
  {
    points1 = points1 + CardPoint(playerSet1[i]);
  }
  for (int i = 0; i < playerSet2.Length; i++)
  {
    points2 = points2 + CardPoint(playerSet2[i]);
  }
  Console.WriteLine();
  Console.WriteLine($"Player 1 has {points1}");
  Console.WriteLine();
  Console.WriteLine($"Player 2 has {points2}");
  Console.WriteLine();
  if (points1 > points2 && points1 <= bjack || points2 > bjack) Console.WriteLine("Player 1 wins!");
  else
    if (points2 > points1 && points2 <= bjack || points1 > bjack) Console.WriteLine("Player 2 wins!");

  if (points1 == points2 && points1 <= bjack && points2 <= bjack)
  {
    Console.WriteLine("Nobody wins - equal points!");
  }
  if (points1 > bjack && points2 > bjack)
  {
    Console.WriteLine("Both failed - overflow!");
  }

}

// печать карты
void PrintCard(int card, int[] Deck)
{
  string[] suits = { "spade", "heart", "clubs", "diamonds" };
  string[] cardname = { "Vallet of ", "Dame of ", "King of ", "six of ", "seven of ", "eight of ", "nine of ", "ten of ", "Ace of " };
  Console.WriteLine($"{cardname[FindCard(card, Deck) % 9] + suits[FindCard(card, Deck) / 9] }");
}

// удаление карты из колоды присвоением соответствующему значению карты
int[] RemoveCards(int[] playSet, int[] Deck)
{

  int[] res = new int[Deck.Length - playSet.Length];
  int countDeck = 0;
  int countRes = 0;
  while (countRes < res.Length)
  {
    if (FindCard(Deck[countDeck], playSet) == 0)
    {
      res[countRes] = Deck[countDeck];
      countRes++;
    }
    countDeck++;
  }
  return res;
}

//найти карту в колоде
int FindCard(int card, int[] deck)
{
  int res = 0;
  for (int i = 0; i < deck.Length; i++)
  {
    if (card == deck[i])
    {
      res = i;
    }
  }
  return res;
}

// определение очков карты
int CardPoint(int card)
{
  return (card - (card / 11) * 10);
}

//
// сделать ход - взять карту
//
int[] MakeMove(int playerNum, int[] curDeck, int[] originalDeck)
{
  ConsoleKeyInfo inch;
  int[] playerSet = new int[curDeck.Length];
  FillArray(playerSet);


  int count = 0;

  //  89 - Y  78 - N Convert.ToInt32(inch.Key) == 78
  while (count < curDeck.Length)
  {
    Console.WriteLine();
    Console.WriteLine($"Игрок {playerNum} Press any key to pull your card, 'N' - exit");
    inch = Console.ReadKey();
    if (Convert.ToInt32(inch.Key) == 78) break;
    playerSet[count] = curDeck[count];
    Console.WriteLine();
    Console.Write($"Your card is : ");
    PrintCard(playerSet[count], originalDeck);
    count++;
  }
  int[] res = new int[count];
  for (int i = 0; i < playerSet.Length; i++)
  {
    if (playerSet[i] != 0) res[i] = playerSet[i];
  }

  return res;
}


int[] CreateDeck(int[] suit, int[] Deck)
{
  int card = 0;

  for (int j = 0; j < 4; j++)
  {
    for (int i = 0; i < suit.Length; i++)
    {
      Deck[card] = suit[i] + j * 10;
      card++;
    }
  }
  return Deck;
}

int[] shuffleDeck(int[] array)
{
  int temp;
  int repCell;
  for (int i = 0; i < array.Length; i++)
  {

    temp = array[i];
    repCell = new Random().Next(0, array.Length);
    array[i] = array[repCell];
    array[repCell] = temp;
  }
  return array;
}

void FillArray(int[] array)
{

  int count = array.Length;
  for (int i = 0; i < count; i++)
  {
    array[i] = 0;
  }
}