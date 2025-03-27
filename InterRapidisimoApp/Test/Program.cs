string word = "Andres";
char[] reverseWord = new char[word.Length];



for (int i = 0, j = word.Length - 1; i<word.Length; i++, j--)
{
    reverseWord[i] = word[j];
}

Console.WriteLine(new string(reverseWord));
