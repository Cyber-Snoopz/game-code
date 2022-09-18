using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Text;
using MoralisUnity;
public class QuizManager : MonoBehaviour
{
    [SerializeField] private string quizGetUri = "http://172.31.23.234:5001/quiz";
    private string quizPostUri;
    string jsonResponse;
    [SerializeField] TextMeshProUGUI question;
    [SerializeField] GameObject[] options;
    [SerializeField] TextAsset jsonData;
    [SerializeField] int count = 0;
    int currentQuestionLevel;
    List<SelectedAnswer> selectedAnswers;
    string ethAddress;
    int prevCount;
    int currentActiveQuestion;
    bool isCompletedQuiz = false;
    [SerializeField] TextMeshProUGUI connectionText;
    [SerializeField] bool isConnectionActive = true;
    [SerializeField] TextMeshProUGUI Loader;
    [SerializeField] GameObject quizContainer;
    private async void Start()
    {
        if (!question || options.Length == 0)
        {
            Debug.LogError("Assign the options and questions");
        }
        selectedAnswers = new List<SelectedAnswer>();
        prevCount = -1;
        // var user= await Moralis.GetUserAsync();
        // ethAddress = "";
        // if(user != null)
        //     ethAddress = user.ethAddress;
        // else
        //     Debug.Log("Failed to retrieve user");
        // quizPostUri = quizGetUri + "/"+ethAddress;
        StartCoroutine(GetRequest(quizGetUri));
    }
    private void ReadJson()
    {
        Data json;
        if (isConnectionActive == true)
        {
            json = JsonUtility.FromJson<Data>(jsonResponse);
            connectionText.gameObject.SetActive(false);
        }
        else
        {
            json = JsonUtility.FromJson<Data>(jsonData.text);
            connectionText.gameObject.SetActive(true);
        }
        if (count < json.questions.Length)
        {
            SetUpQuiz(json);
        }
        else
        {
            if(isConnectionActive)
                HandlePostRequest();
            else
            {
                int score = 0;
                int i = 0;
                foreach(SelectedAnswer answer in selectedAnswers)
                {
                    if(answer.answer == json.questions[i].correct)
                        score++;
                    i++;
                }
                question.text = "You got " + score + " out of " + count + " correct.\n Unfortunately, it's only a mock test hence no NFT will be provided!";
                foreach(GameObject option in options)
                    option.SetActive(false);
            }
        }
    }

    private void SetUpQuiz(Data json)
    {
        var currentQnA = json.questions[count];
        question.text = currentQnA.questionText;
        QuestionIdHolder questionHolder = question.GetComponent<QuestionIdHolder>();
        if (questionHolder)
            questionHolder.setQuestionId(currentQnA.id);
        currentActiveQuestion = currentQnA.id;
        options[0].GetComponentInChildren<TextMeshProUGUI>().text = currentQnA.option1;
        options[1].GetComponentInChildren<TextMeshProUGUI>().text = currentQnA.option2;
        options[2].GetComponentInChildren<TextMeshProUGUI>().text = currentQnA.option3;
        options[3].GetComponentInChildren<TextMeshProUGUI>().text = currentQnA.option4;
        currentQuestionLevel = currentQnA.level;
    }

    private void HandlePostRequest()
    {
        isCompletedQuiz = true;
        foreach (GameObject option in options)
        {
            option.SetActive(false);
        }
        DataToReturn dataToReturn = new DataToReturn(selectedAnswers.ToArray());
        if (isConnectionActive)
        {
            var jsonResponseData = JsonUtility.ToJson(dataToReturn);
            quizPostUri = quizGetUri + "/" + ethAddress;
            StartCoroutine(Post(quizPostUri, jsonResponseData));
        }
    }

    public void OnAnswerClick(int option)
    {
        Debug.Log(option);
        SelectedAnswer selectedAnswer = new SelectedAnswer(currentActiveQuestion, option);
        if (selectedAnswer != null)
            selectedAnswers.Add(selectedAnswer);
        else
            Debug.Log("Selected Answer not set");
        count++;
    }


    [System.Serializable]
    class Data
    {
        public QnA[] questions;
    }

    [System.Serializable]
    public class QnA
    {
        public string questionText;
        public int id;
        public string option1;
        public string option2;
        public string option3;
        public string option4;
        public int correct;
        public int level;
    }
    [System.Serializable]
    class DataToReturn
    {
        public SelectedAnswer[] answers;
        public DataToReturn(SelectedAnswer[] answerPicked)
        {
            answers = answerPicked;
        }
    }
    [System.Serializable]
    public class SelectedAnswer
    {
        public int questionId;
        public int answer;
        public SelectedAnswer(int question, int answer)
        {
            this.questionId = question;
            this.answer = answer;
        }
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(uri))
        {
            quizContainer.SetActive(false);
            Loader.gameObject.SetActive(true);
            var handler = request.SendWebRequest();
            float requestTime = 0f;
            while (!handler.isDone)
            {
                requestTime += Time.deltaTime;
                int loaderDot = (int)requestTime%3;
                if(loaderDot == 0)
                    Loader.text = "Loading.";
                if(loaderDot == 1)
                    Loader.text = "Loading..";
                if(loaderDot == 2)
                    Loader.text = "Loading...";
                if (requestTime > 10f)
                {
                    break;
                }
                yield return null;
            }
            if (request.result == UnityWebRequest.Result.Success)
            {
                isConnectionActive = true;
                jsonResponse = request.downloadHandler.text;
            }
            else
            {
                isConnectionActive = false;
            }
            Loader.gameObject.SetActive(false);
            quizContainer.SetActive(true);
            StartCoroutine(StartQuiz());
            request.Abort();
        }
    }
    IEnumerator StartQuiz()
    {
        while (!isCompletedQuiz)
        {
            if (prevCount != count)
            {
                Debug.Log("Reached here");
                prevCount = count;
                ReadJson();
            }
            yield return null;
        }
    }
    IEnumerator Post(string url, string bodyJsonString)
    {
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        quizContainer.SetActive(false);
        Loader.gameObject.SetActive(true);
        yield return request.SendWebRequest();
        var data = request.downloadHandler.text;
        Debug.Log(data);
        Debug.Log("Status Code: " + request.responseCode);
        Loader.gameObject.SetActive(false);
        quizContainer.SetActive(true);
        request.Abort();
    }
}

