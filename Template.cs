// Martin Pelteshki 11/3
using System;

public class Template
{
    public static void Main(string[] args)
    {
        Console.Write("How Many People are Playing?: ");
        int players = Convert.ToInt32(Console.ReadLine());

        Console.Clear();

        // each iteration of loop is a new game; doesn't work for now.
        for(int i=1; i<=players; i++){

            Game g;
            switch(wordChoiceMenu(i)){
                case 1:
                    Console.Write("Write Word: ");
                    g = new Game(Console.ReadLine());
                    Console.Clear();
                    break;

                case 2:
                    g = new Game();
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("Invalid Option");
                    //kills the program with exit code 1
                    Environment.Exit(1);
                    break;
            }


            // play game


            // keep results
        }
            
    }
    public static int wordChoiceMenu(int i){
        // pick who chooses word menu
        Console.WriteLine("Who Chooses a Word for Player " + i + "?");
        Console.WriteLine("[1] The Player ");
        Console.WriteLine("[2] The Computer");
        Console.Write("Your Choice: ");
        return Convert.ToInt32(Console.ReadLine());
    }
}

class Game{
    private string word_chosen;
    private string[] wordBank = new string[] {"apple", "banana", "carrot", "dog", "elephant", "flower", "giraffe", "house", "island", "jacket"};
    private string wordGuessing; // the slot for the word the players are guessing. (e.g: _ _ _ _ _ _)

    private int lives=5;
    // null constructor
    public Game(){
        // initializes game with word from bank
        word_chosen=pickWordFromBank();
        initWordGuessing(word_chosen);
    }

    // other constructor
    public Game(string word){
        // initializes game with player chosen word
        word_chosen=word;
        initWordGuessing(word_chosen);
    }

    private string pickWordFromBank(){
        // picks a random element from wordBank

        Random q = new Random();
        int index = q.Next(0, wordBank.Length);
        return wordBank[index];
    }

    private void initWordGuessing(string word){
        int wgl = word.Length;
        char[] wordGuessing = new char[wgl];
        for (int i=0; i<wgl; i++){
            wordGuessing[i]='_';
        }
    }
}