#ifndef __DICTIONARY_H__
#define __DICTIONARY_H__  

#include <map>
#include <list>
#include <string>

namespace Dictionary
{
	std::list<std::string> GetDictionary(int wordLength);
	std::map<std::string, std::list<std::string>> GetVariations(std::list<std::string> wordsToFilter, char letterToFilterBy);
}

#endif __DICTIONARY_H__
