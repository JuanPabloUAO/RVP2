using UnityEngine;

[CreateAssetMenu(fileName = "QuizData", menuName = "Quiz/MovieQuiz")]
public class MovieQuizData : ScriptableObject
{
    public MovieID movieID;
    public Question[] questions;
}