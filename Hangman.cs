using System;
using System.Collections.Generic;

public class Template
{
    public static void Main(string[] args)
    {
        // pick how many players menu, n players, n games
        Console.Write("How Many People are Playing?: ");
        int players = Convert.ToInt32(Console.ReadLine());

        // create a dictionary to store player scores
        Dictionary<string, int> scores = new Dictionary<string, int>();

        Console.Clear();

        // each iteration of loop is a new game
        Game g = null;
        string playerName;
        for(int i=1; i<=players; i++){
            // get player name
            Console.Write("Enter player name: ");
            playerName = Console.ReadLine();

            // add player to scores dictionary
            scores[playerName] = 0;

            switch(wordChoiceMenu(playerName)){
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

            string guess;
            bool FalseGuess;
            char guessChar;
            
            // each iteration of loop is a new guess
            while (!g.IsGameOver()){
                Console.WriteLine("Word to guess: " + g.GetWordGuessing());
                Console.WriteLine("Lives remaining: " + g.GetLives());
                Console.Write("Enter your guess: ");
                guess = Console.ReadLine();

                FalseGuess = false;
                guessChar = guess[0];

                // checks if guess is a single char or a word; uses respective overload
                if (guess.Length == 1){
                    FalseGuess = g.makeGuess(guessChar);
                }
                else if (guess.Length > 1){
                    FalseGuess = g.makeGuess(guess);
                }
                else{
                    Console.WriteLine("Invalid guess! Please enter a single character or a word.");
                }

                if (!FalseGuess){
                    Console.WriteLine("Incorrect guess!");
                }

                Console.WriteLine();
            }

            // checks if player won or lost
            if (g.IsWordGuessed()){
                Console.WriteLine("Congratulations! You guessed the word: " + g.GetWord());
                
                // update player score
                scores[playerName] += 1;
            }
            else{
                Console.WriteLine("Game over! The word was: " + g.GetWord());
            }

            Console.WriteLine();    
        }

        // currently, each player has a stored count of of their losses. we need to count their wins
        // we do this by creating a new dictionary, scoresPrint
        // here, each player's score' is the sum of all ther players' losses 

        Dictionary<string, int> scoresPrint = new Dictionary<string, int>();
        int temp=0;
        foreach (KeyValuePair<string, int> entry1 in scores){
            foreach (KeyValuePair<string, int> entry2 in scores){
                if(entry1.Key!=entry2.Key){
                    temp += entry2.Value;
                }
            }
            scoresPrint[entry1.Key]=temp;
            temp=0;
        }

        // print everyone's 'win' scores
        foreach (KeyValuePair<string, int> entry in scoresPrint)
        {
            Console.WriteLine($"{entry.Key}: {entry.Value}");
        }
    }
    
    // returns 1 if player chooses word, 2 if computer chooses word, prompts player to pick6
    public static int wordChoiceMenu(string i){
        // pick who chooses word menu
        Console.WriteLine("Who Chooses a Word for Player " + i + "?");
        Console.WriteLine("[1] The Player ");
        Console.WriteLine("[2] The Computer");
        Console.Write("Your Choice: ");
        return Convert.ToInt32(Console.ReadLine());
    }    
}

public class Game{
    private string wordChosen;
    private string[] wordBank = new string[] {"apple", "banana", "carrot", "dog", "elephant", "flower", "giraffe", "house", "island", "jacket"};
    private char[] wordGuessing; // the slot for the word the players are guessing. (e.g: _ _ _ _ _ _)
    private bool GameOver;
    private bool wordGuessed;

    private int lives=5;
    // null constructor
    public Game(){
        // initializes game with word from bank
        wordChosen=pickWordFromBank();
        initWordGuessing(wordChosen);
        GameOver=false;
        wordGuessed=false;
    }

    // other constructor
    public Game(string word){
        // initializes game with player chosen word
        wordChosen=word;
        initWordGuessing(wordChosen);
        GameOver=false;
        wordGuessed=false;
    }

    // picks a random element from wordBank
    private string pickWordFromBank(){
        Random q = new Random();
        int index = q.Next(0, wordBank.Length);
        return wordBank[index];
    }

    // turns a string into its "_ _ _" form
    public void initWordGuessing(string word, int index = 0)
    {
        if (index == 0){
            wordGuessing = new char[word.Length];
        }

        if (index < word.Length){
            wordGuessing[index] = '_';
            initWordGuessing(word, index + 1);
        }
    }
    // returns true if guess is correct, works with chars
    public bool makeGuess(char guess){
        bool isCorrectGuess = false;

        for(int i = 0; i < wordChosen.Length; i++)
        {
            if (wordChosen[i] == guess)
            {
                wordGuessing[i]=guess;
                isCorrectGuess = true;
                if(convertWord(wordGuessing) == wordChosen){
                    wordGuessed = true;
                }
            }
        }

        if (!isCorrectGuess){
            lives--;
        }

        return isCorrectGuess;
    }
    
    // returns true if guess is correct, works with strings
    public bool makeGuess(string guess)
    {
        bool isCorrectGuess = false;
        if (guess == wordChosen){
            wordGuessed = true;
            isCorrectGuess = true;
        }
        else{
            isCorrectGuess = false;
        }

        if (!isCorrectGuess){
            lives--;
        }
        return wordChosen == guess;
    }
    // returns gameOver
    public bool IsGameOver(){
        if (lives==0 || wordGuessed){
            GameOver=true;
        }
        
        return GameOver;
    }
    // returns wordGuessing
    public string GetWordGuessing(){
        return convertWord(wordGuessing); 
    }
    // returns number of lives remaining
    public int GetLives(){
        return lives; 
    }

    // returns true if word is guessed
    public bool IsWordGuessed(){
        return wordGuessed;
    }
    public string GetWord(){
        return wordChosen;
    }
    //converts char array to string
    public string convertWord(char[] word){
        return new string(word);
    }
}
