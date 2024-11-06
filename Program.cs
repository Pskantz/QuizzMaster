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
            // Läs frågor från en textfil
            var questions = LoadQuestionsFromFile("questions.txt");

            int score = 0;
            int questionNumber = 1;
            int timeLimit = 10; // Tidsgräns per fråga i sekunder
            var results = new List<string>();

            foreach (var question in questions)
            {
                Console.WriteLine($"Fråga {questionNumber}: {question.QuestionText}");
                for (int i = 0; i < question.Options.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {question.Options[i]}");
                }

                Console.Write("Välj ditt svar (1-3): ");
                var startTime = DateTime.Now;
                string userAnswer = Console.ReadLine() ?? string.Empty;
                var elapsedTime = (DateTime.Now - startTime).TotalSeconds;

                if (elapsedTime > timeLimit)
                {
                    Console.WriteLine("Tiden är slut! Gå vidare till nästa fråga.");
                    results.Add($"Fråga {questionNumber}: Tiden är slut!");
                }
                else if (int.TryParse(userAnswer, out int answerIndex) &&
                         answerIndex > 0 && answerIndex <= question.Options.Count &&
                         question.Options[answerIndex - 1] == question.CorrectAnswer)
                {
                    Console.WriteLine("Rätt svar!");
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

            Console.WriteLine($"Ditt slutresultat: {score} poäng.");
            Console.WriteLine("Resultat per fråga:");
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
}