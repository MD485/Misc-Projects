#pragma once
#include <map>
#include <list>
#include <string>

using std::list;
using std::string;
using std::map;

namespace Dictionary
{
	list<string> GetDictionary(int wordLength);
	map<string, list<string>> GetVariations(list<string> wordsToFilter, char letterToFilterBy);
}
