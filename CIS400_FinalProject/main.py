# CIS 400 Final Project - Spring 2023
# Johnathan Fuhrman, Austin Johnson, Evan Garvey, Joseph Busacca, Talal Hakki
# Imports

import twitter
from urllib.parse import unquote
from functools import partial
from sys import maxsize as maxint
import sys
import time
from urllib.error import URLError
from http.client import BadStatusLine
import networkx as nx
## import requests
import json
import numpy as np
import matplotlib
import matplotlib.pyplot as plt
import nltk
from nltk.tokenize import sent_tokenize, word_tokenize
from nltk.sentiment.vader import SentimentIntensityAnalyzer
from nltk.corpus import stopwords
from nltk.stem import WordNetLemmatizer

# One-time code. Comment this out, run once, then comment in again.
# nltk.download('all')

# DIVISION OF WORK ***


# Twitter Parsing ***
# John, Talal
# Tasks to complete:
# get 5 candidates (Lionel Messi, Erling Haaland, Kylian Mbappe, Karim Benzema, Vinicius Junior)
# get at least 300 tweets supporting each candidate for the chance of winning the ballon d'or in 2023
# output tweets give them to Evan and Austin.

## This method is from the Twitter Cookbook Chapter 1. Returns the twitter_api based on
## Twitter Developer account credentials with Elevated access
def oauth_login():
    CONSUMER_KEY = '4B21fng1De6ivIbzQ44ZyhtXm'
    CONSUMER_SECRET = 'T8wmdnY9TYNvihFaZduNlRoFOxT5is2B43O1va6k0XGiYfo8C4'
    OAUTH_TOKEN = '1359226761515593731-zJjwg8wZjFhvWgNWsOx7sBvbhBacGM'
    OAUTH_TOKEN_SECRET = 'RtmaAQoocQnV7v3KJetxlekdnJIGee7JYyr1doykNGzii'

    auth = twitter.oauth.OAuth(OAUTH_TOKEN, OAUTH_TOKEN_SECRET, CONSUMER_KEY, CONSUMER_SECRET)
    twitter_api = twitter.Twitter(auth=auth)
    return twitter_api


## NOTE: THIS FUNCTION STILL HAS NOT BEEN USED. (I think we might have to use it)
## This method is from the Twitter Cookbook Chapter 9. Creates a twitter request
## and provides exception handling to allow the program to run. I find this especially to be
## an important error handling case for when the rate limit is exceeded which allows my program to
## 'sleep'.
def make_twitter_request(twitter_api_func, max_errors=10, *args, **kw):
    # A nested helper function that handles common HTTPErrors. Return an updated
    # value for wait_period if the problem is a 500 level error. Block until the
    # rate limit is reset if it's a rate limiting issue (429 error). Returns None
    # for 401 and 404 errors, which requires special handling by the caller.
    def handle_twitter_http_error(e, wait_period=2, sleep_when_rate_limited=True):

        if wait_period > 3600:  # Seconds
            print('Too many retries. Quitting.', file=sys.stderr)
            raise e

        # See https://developer.twitter.com/en/docs/basics/response-codes
        # for common codes

        if e.e.code == 401:
            print('Encountered 401 Error (Not Authorized)', file=sys.stderr)
            return None
        elif e.e.code == 404:
            print('Encountered 404 Error (Not Found)', file=sys.stderr)
            return None
        elif e.e.code == 429:
            print('Encountered 429 Error (Rate Limit Exceeded)', file=sys.stderr)
            if sleep_when_rate_limited:
                print("Retrying in 15 minutes...ZzZ...", file=sys.stderr)
                sys.stderr.flush()
                time.sleep(60 * 15 + 5)
                print('...ZzZ...Awake now and trying again.', file=sys.stderr)
                return 2
            else:
                raise e  # Caller must handle the rate limiting issue
        elif e.e.code in (500, 502, 503, 504):
            print('Encountered {0} Error. Retrying in {1} seconds'.format(e.e.code, wait_period), file=sys.stderr)
            time.sleep(wait_period)
            wait_period *= 1.5
            return wait_period
        else:
            raise e

    # End of nested helper function

    wait_period = 2
    error_count = 0

    while True:
        try:
            return twitter_api_func(*args, **kw)
        except twitter.api.TwitterHTTPError as e:
            error_count = 0
            wait_period = handle_twitter_http_error(e, wait_period)
            if wait_period is None:
                return
        except URLError as e:
            error_count += 1
            time.sleep(wait_period)
            wait_period *= 1.5
            print("URLError encountered. Continuing.", file=sys.stderr)
            if error_count > max_errors:
                print("Too many consecutive errors...bailing out.", file=sys.stderr)
                raise
        except BadStatusLine as e:
            error_count += 1
            time.sleep(wait_period)
            wait_period *= 1.5
            print("BadStatusLine encountered. Continuing.", file=sys.stderr)
            if error_count > max_errors:
                print("Too many consecutive errors...bailing out.", file=sys.stderr)
                raise


# Includes modified code from the Twitter Cookbook #1
# This function takes in a list of search strings for each player, and outputs 300 tweets per player
# to the playerTweets.txt file.
def getPlayersTweets(playerNameList):  # playerNameList is a list of the search keyword for each player

    file = open("playerTweets.txt", "w", encoding='utf-8')

    for playerQuery in playerNameList:

        q = playerQuery
        count = 100

        search_results = twitter_api.search.tweets(q=q, count=count)
        statuses = search_results['statuses']

        # Iterate through 2 more batches of results by following the cursor, so 300 tweets are retrieved for the current player.
        for _ in range(2):
            print('Length of statuses', len(statuses))
            try:
                next_results = search_results['search_metadata']['next_results']

            except KeyError as e:  # No more results when next_results doesn't exist
                break

            # Create a dictionary from next_results, which has the following form:
            # ?max_id=847960489447628799&q=%23RIPSelena&count=100&include_entities=1
            kwargs = dict([kv.split('=') for kv in unquote(next_results[1:]).split("&")])

            search_results = twitter_api.search.tweets(**kwargs)
            statuses += search_results['statuses']

        print('Length of statuses', len(statuses))

        file.write("\n\nT_TTweets for searching: " + playerQuery + "T_T")
        # This is a way of accessing/printing each tweet text
        for i in range(len(statuses)):
            file.write("\n\n")
            file.write(statuses[i]['text'])

    # close file for writing
    file.close()


if __name__ == "__main__":
    twitter_api = oauth_login()

    playersList = ['Messi ballon dor', 'Haaland ballon dor', 'Mbappe ballon dor', 'Benzema ballon dor',
                   'Vinicius ballon dor']

    getPlayersTweets(playersList)
