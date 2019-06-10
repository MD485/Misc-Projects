#include "Dictionary.h"
#include <iostream>
#include <fstream>
#include <string>
#include <regex>
#include <list>
#include <map>

using std::map;
using std::list;
using std::string;
using std::cout;
using std::endl;
using std::cin;

//Core logic of the game, specifies what happens when a player guesses.
bool guess(char c, list<string> &wordList, string &tries, string &currentWord) {
	//Gets all the variations of a letter that exists within the wordlist, in a hash map form.
	//Look at the Dictionary file for more info.
	map<string, list<string>> possibilities = Dictionary::GetVariations(wordList, c);

	size_t largestListSize = 0;
	string largestListKey = "";

	//Add the currently guessed letter to the string of attempts.
	tries.resize(tries.size() + 1, c);

	//Iterates over the map to see what key has the most possibilties, this is used so the computer
	//can choose the outcome that gives it the most options.
	for (std::map<string, list<string>>::iterator it = possibilities.begin();
		it != possibilities.end(); ++it)
	{
		if (it->second.size() > largestListSize) {
			largestListSize = it->second.size();
			largestListKey = it->first;
		}
	}

	//Changes the list of possible words to the largest subset of words that contains some combination
	//of the character input.
	wordList = possibilities[largestListKey];

	//In the case where one or more instances of the character shows up in the largest subset, this
	//replaces '-' in the current word with that letter.
	if (largestListKey.find('X') != -1) {
		for (size_t i = 0; i < currentWord.size(); i++) {
			if (largestListKey[i] == 'X') {
				currentWord[i] = c;
			}
		}
		cout << "You hit the mark, " << c << " was in the word" << endl;
		return false;
	}
	else {
		cout << c << " not found." << endl;
		return true;
	}
}

//The main game loop
bool game_loop(int guesses, list<string> wordList) {
	bool notWon = true;
	int attempts = 0;
	//Keeps track of the letters the player has already guessed.
	string tries;
	//Keeps track of the state of the current word.
	string word;

	//Initalises the word as the length of the first word in the wordlist, because of the steps
	//taken to initalise the word list, all words should be the same length.
	//Word is the placeholder for a word being guessed, e.g. "----" for a four letter word.
	word.resize((*wordList.begin()).size(), '-');

	//Continues running till the player either runs out of attempts or wins.
	while (guesses != attempts && notWon) {
		char playerGuess;
		cout << "Word: " << word 
			 << " | Tries : " << tries 
			 << " | Attempts remaining: " << guesses - attempts << endl 
			 << "What is your guess?" << endl;
		cin >> playerGuess;
		//Checks if the letter is in the "tries" string holding all the guessed letter.
		if (tries.find(playerGuess) != -1) {
			cout << "You already guessed " << playerGuess << "." << endl;
		//If it hasn't been guessed sees whether the letter provided was in the word,
		//returns true if it wasn't in the word, and false if it was.
		} else if (guess(playerGuess, wordList, tries, word)) {
			attempts++;
		//In the case where a guess was in a word, checks whether the word variable has all
		//it's fields filled out. Meaning checks whether the word is solved.
		} else if (word.find('-') == -1) {
			notWon = false;
		}
		cout << endl;
	}

	if (notWon) {
		//To add insult to injury the application picks an arbitrary word which the player
		//didn't guess.
		cout << "You have run out of guesses! The word was: " << wordList.front();
	} else {
		//Prints the word the player guessed
		cout << "You won! The word was: " << word;
	}
	cout << endl << endl;

	return notWon;
}

//The "main menu" of the game, keeps track of win/loss, allows the player to select the word length
//and the guesses, also allows the game to run again if the player doesn't type 'n' in the final
//prompt. Currently this bugs out if unexpected input is inserted, e.g.letters on number input, I
//feel as if there are better ways to handle input TODO: Learn how to do input properly.
int main()
{
	bool notFinished = true;
	int wins = 0, losses = 0;
	while (notFinished) {
		system("CLS");
		int wordLength, attempts;

		cout << "Hello and welcome to evil hangman." << endl
			 << "Current W/L record: " << wins << "-" << losses << endl << endl;

		cout << "How long would you like your word to be?" << endl;
		cin >> wordLength;
		cout << endl;
		
		list<string> wordList = Dictionary::GetDictionary(wordLength);
		if (wordList.size() == 0) {
			cout << "No words of that length exist." << endl;
			system("pause");
			system("CLS");
		} else {
			cout << "How many attempts would you like?" << endl;
			cin >> attempts;
			cout << endl;

			if (game_loop(attempts, wordList)) {
				++losses;
			} else {
				++wins;
			}
			cout << "Would you like to play again?" << endl;
			string playAgain;
			cin >> playAgain;
			if (playAgain.find("n") != -1) {
				notFinished = false;
			}
		}
	}
	cout << "Thanks for playing!" << endl;
	system("pause");
	return 0;
}