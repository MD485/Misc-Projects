# Evil Hangman
My second project based off an assignment brief from nifty.stanford.edu;  
Evil Hangman: http://nifty.stanford.edu/2011/schwarz-evil-hangman/.  
  
The premise is that you give the computer a dictionary of words and it uses that list in an attempt to make the player use up
all their guesses by repeatedly changing the word. Rather simple, doesn't use the further suggestions in the assignment brief 
like lookahead and such others to make the computer play a perfect game but it is pretty effective at making you lose.  
  
Currently the implementation has some questionable aspects, in that it crashes on any erroneous input; updates the attempts by 
resizing the tries string which holds all the attempts and doesn't cast ascii characters to the same base allowing for repeat letters.  
  
Baking the data type holding the attempts less taxing would be pretty easy, you could make it a bool array and simply print out 
the characters whose bool index is true, e.g. tries[0] = true, 0+97 = 'a', print a, etc. Or simply dimension the string the size of 
the alphabet and replace characters within it, both would have rather minimal changes to the current code other than bool requiring 
a new method to get it's string representation. Making sure the character is in the same case all the time is also pretty easy, just 
do something like tolower(playerGuess);.  
  
However getting properly validated input I'm less sure and I'll put off changing the code further till I figure that out since that's 
far more important.
