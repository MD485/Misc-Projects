#include "Dictionary.h"
#include <fstream>

namespace Dictionary {

	//This uses the wordlength to return a list of words with the same length, from a dictionary
	//file that needs to be in the same directory as the executable.
	list<string> GetDictionary(int wordLength)
	{
		using std::ifstream;

		list<string> wordList;
		ifstream dictionary("dictionary.txt");
		string line;
		while (getline(dictionary, line)) {
			if (line.length() == wordLength) {
				wordList.push_back(line);
			}
		}
		dictionary.close();
		return wordList;
	}

	//Returns a map containing a string that represents instances of a character and a list
	//which contains all words that match those instances.
	map<string, list<string>> GetVariations(list<string> wordsToFilter,
		char letterToFilterBy)
	{
		map<string, list<string>> variations;
		//Iterating over every word in the word list
		for (list<string>::iterator currentWord = wordsToFilter.begin();
			currentWord != wordsToFilter.end(); ++currentWord) {
			string wordKey = "";
			//For some reason I was having some issues with *currentWord so I assigned it to a str.
			string tempWord = *currentWord;
			wordKey.resize(tempWord.length(), '-');
			//Generating a unique signature based off the contents of each word.
			for (size_t i = 0; i < tempWord.length(); i++) {
				//If the word at the index is the char provided replace '-' with 'X'.
				if (tempWord.at(i) == letterToFilterBy) {
					wordKey.at(i) = 'X';
				}
			}
			//Add the word that was just processed to the back of it's list.
			variations[wordKey].push_back(tempWord);
		}
		return variations;
	}
};
