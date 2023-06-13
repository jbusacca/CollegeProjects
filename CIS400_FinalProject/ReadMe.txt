

Ballon D'or Outcome Predictor
CIS 400 Final project - Spring 2023


GROUP MEMBERS:
    Joseph Busacca
    Jonathan Fuhrman
    Evan Garvey
    Talal Hakki
    Austin Johnson


DESCRIPTION:
    The Ballon D'or Outcome Predictor is a python-based application that utilizes the
    Twitter API, NLTK, and MatPlotLib to search Twitter for selected potential candidates
    of this year's Ballon D'or, particularly Lionel Messi, Erling Haaland, Karim Benzema,
    Kylian Mbappe, and Vinicius Jr. Sentiment analysis is run on the selection of tweets,
    and graphs are created for the client.

HOW TO USE:
    While there is a provided Twitter API code in this file, due to current limitations
    regarding Twitter's API service, it is strongly suggested to switch this out with your
    own personal API code. The API codes are located at lines 41 through 44. Aditionally,
    there exists a commented out line of code at line 26 (nltk.download('all')). Uncomment
    this line the first time you run this code. First, run the main function in main.py. After
    this is completed, there should be a newly created "playerTweets.txt" file in the directory.

    Sentiment Analysis and Data Visualization occurs in analysis.py. Running the main there
    will cause the tweets to be analyzed and then graphed, with the graphs showing up [not sure
    where they show up, u can replace this portion of text with where the graphs are saved, also
    this shit is in the share folder for some reason cuz I think I did it wrong when making it lol
    so feel free to put this in the main directory since I'm not sure I have those permissions
    as a nonhost]