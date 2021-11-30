/* Задачи
1. Создаем колоду карт.
2, Для игры нам необходима перемешанная колода, поэтому надо в случайном порядке 
перетасовать карты (или сделать какой-то механизм, чтобы менять порядок карт)
*/
int numberOfCards = 52;
string[] cardSuit = new string[] { "треф", "пик", "буби", "червы" };
string[] cardRank = new string[]
{"туз", "двойка", "тройка", "четвёрка", "пятерка", "шестёрка", "семёрка",
 "восьмёрка", "девятка", "десятка", "валет", "дама", "король"};

string[,] FillCardDesk(string[] array1, string[] array2) // создание колоды
{
    string[,] doubleArray = new string[array1.Length, array2.Length]; // новая колода
    int indxArray1 = 0; // масть
    for (int i = 0; i < array1.Length; i++)
    {
        int indxArray2 = 0; // ранг карты
        for (int j = 0; j < array2.Length; j++)
        {
            doubleArray[i, j] = $"{array2[indxArray2]} {array1[indxArray1]}, ";
            indxArray2++;
        }
        indxArray1++;
    }
    return doubleArray;
}
void PrintImage(string[,] doubleArray) // вывод колоды на консоль
{
    for (int i = 0; i < doubleArray.GetLength(0); i++)
    {
        for (int j = 0; j < doubleArray.GetLength(1); j++)
        {
            Console.Write($"{doubleArray[i, j]}");
        }
        Console.WriteLine();
    }
}
string[,] cardDesk = FillCardDesk(cardSuit, cardRank);
PrintImage(cardDesk);
