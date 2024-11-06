// File Path: QuizMaster.ConsoleApp/Program.cs
using System;
using System.IO;
using System.Collections.Generic;

namespace QuizMaster.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {   
            Console.WriteLine("\n-----------------------\nWelcome to QuizzMaster!\n-----------------------\n");
            Console.WriteLine("Do you want to start the game? ");
            Console.WriteLine("\nY for YES\nN for NO\n");
 
            var input = Console.ReadLine().ToUpper();
            if (input == "Y")
            {
                Message.StartInfo();
            }
            else if (input == "N")
            {
                Message.EndGameInfo();
                return;
            }   
            
            // Läs frågor från en textfil
            var questions = LoadQuestionsFromFile("questions.txt");

            int score = 0;
            int questionNumber = 1;
            int timeLimit = 10; // Tidsgräns per fråga i sekunder
            var results = new List<string>();

            foreach (var question in questions)
            {
                Console.WriteLine($"\nFråga {questionNumber}: {question.QuestionText}\n");
                for (int i = 0; i < question.Options.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {question.Options[i]}");
                }

                Message.AnswerMessage();
                var startTime = DateTime.Now;
                string userAnswer = Console.ReadLine() ?? string.Empty;
                var elapsedTime = (DateTime.Now - startTime).TotalSeconds;

                if (elapsedTime > timeLimit)
                {
                    Message.TimesUpMessage();
                    results.Add($"Fråga {questionNumber}: Tiden är slut!");
                }
                else if (int.TryParse(userAnswer, out int answerIndex) &&
                         answerIndex > 0 && answerIndex <= question.Options.Count &&
                         question.Options[answerIndex - 1] == question.CorrectAnswer)
                {
                    Message.RightAnswer();
                    score++;
                    results.Add($"Fråga {questionNumber}: Rätt svar!");
                }
                else
                {
                    Console.WriteLine($"Fel svar! Rätt svar var: {question.CorrectAnswer}");
                    results.Add($"Fråga {questionNumber}: Fel svar! Rätt svar var: {question.CorrectAnswer}");
                }

                questionNumber++;
            }
            
            // Message.Score();
            Console.WriteLine($"Ditt slutresultat: {score} poäng.");
            // Console.WriteLine("Resultat per fråga:");
            Message.ResultPerQ();
            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
        }

        static List<Question> LoadQuestionsFromFile(string filePath)
        {
            var questions = new List<Question>();

            try
            {
                foreach (var line in File.ReadAllLines(filePath))
                {
                    var parts = line.Split('|');
                    if (parts.Length == 5)
                    {
                        var questionText = parts[0];
                        var options = new List<string> { parts[1], parts[2], parts[3] };
                        var correctAnswer = parts[4]; // Rätt svar är den femte delen
                        questions.Add(new Question(questionText, options, correctAnswer));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ett fel inträffade: {ex.Message}");
            }

            return questions;
        }
    }

    class Question
    {
        public string QuestionText { get; }
        public List<string> Options { get; }
        public string CorrectAnswer { get; }

        public Question(string questionText, List<string> options, string correctAnswer)
        {
            QuestionText = questionText;
            Options = options;
            CorrectAnswer = correctAnswer;
        }
    }

    class Message
    {
        
        public static void StartInfo()
        {
            Console.WriteLine("\nThe game will now begin."); 
            Console.WriteLine("Rules: ");
            Console.WriteLine("- Theere will be 5 questions.");
            Console.WriteLine("- Every question has 3 alternativ answers. You choose alternative by entering number 1, 2 or 3");
            Console.WriteLine("- You have 10 seconds to answer each question.\n");     
        }
        public static void EndGameInfo() => Console.WriteLine($"\nThe game ended.");
        public static void WrongMessage() => Console.WriteLine("Please enter a valid sign.");
        public static void AnswerMessage() => Console.Write("\nVälj ditt svar (1-3): ");
        public static void TimesUpMessage() => Console.WriteLine("Tiden är slut! Gå vidare till nästa fråga.");
        public static void RightAnswer() => Console.WriteLine("Rätt svar!");
        
        // public static void Score() => Console.WriteLine($"Ditt slutresultat: {score} poäng.");
        public static void ResultPerQ() => Console.WriteLine("Resultat per fråga:");
        
    }
}