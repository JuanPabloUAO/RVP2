using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public Text questionText;
    public Button[] answerButtons;
    public Text progressText;

    public MovieQuizData[] allQuizzes;

    private Question[] questions;
    private int currentQuestionIndex = 0;
    private int score = 0;
    private MovieID currentMovie;

    void Start()
    {
        int movieIndex = PlayerPrefs.GetInt("SelectedMovie", 0);
        currentMovie = (MovieID)movieIndex;

        StartQuizByMovie(currentMovie);
    }

    public void StartQuizByMovie(MovieID movie)
    {
        MovieQuizData data = GetQuiz(movie);
        questions = data.questions;

        currentQuestionIndex = 0;
        score = 0;

        ShowQuestion();
    }

    MovieQuizData GetQuiz(MovieID id)
    {
        foreach (var quiz in allQuizzes)
        {
            if (quiz.movieID == id)
                return quiz;
        }
        return null;
    }

    void ShowQuestion()
    {
        Question q = questions[currentQuestionIndex];

        questionText.text = q.questionText;
        progressText.text = (currentQuestionIndex + 1) + "/10";

        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i;

            answerButtons[i].GetComponentInChildren<Text>().text = q.answers[i];

            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => Answer(index));
        }
    }

    void Answer(int index)
    {
        if (currentQuestionIndex >= questions.Length)
            return;

        if (index == questions[currentQuestionIndex].correctIndex)
        {
            score++;
        }

        currentQuestionIndex++;

        if (currentQuestionIndex < questions.Length)
        {
            ShowQuestion();
        }
        else
        {
            FinishQuiz();
        }
    }

    void FinishQuiz()
    {
        int finalScore = Mathf.RoundToInt((score / (float)questions.Length) * 10f);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.SaveScore(currentMovie, finalScore);
        }

        questionText.text = "Puntaje final: " + finalScore + "/10";

        foreach (var btn in answerButtons)
        {
            btn.gameObject.SetActive(false);
        }

        Invoke("GoToMenu", 3f);
    }

    void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}