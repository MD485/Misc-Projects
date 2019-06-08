#A Pesky Tourist  

This is a somewhat rewritten implementation of an old VB project written in 2016, an implementation of the nifty.stanford.edu 
project "A Pesky Tourist"; seen here: http://nifty.stanford.edu/2014/nicholson-the-pesky-tourist/  

The basic idea of the project is that given multiple images from a still perspective, if you simply sort the pixels and take 
the median values, then that will remove a lot of abnormalities from a photograph. In this case the idea being, getting rid of 
tourists from scenic holiday photos.

Right now folders can be dragged and dropped into the resultant .exe or you can simply run it and you'll get a prompt to enter 
the folder of the ppm files.

It picks the first file found and then matches it to all files of the same encoding type and resolution, if there are less than 3 
files who meet those restrictions, appropriate error messages are printed, due to the algorithm not working for two or less files.

Currently the implementation is restricted to PPM files of specific formats, rather than functioning for the whole standard, the 
format is as follows:  
P3(LF)  
600 800(LF)  
382(LF)  
P3, 600 800 and 382 can all be different(P5, 480 260, 251, etc), but the spacing and line feed after each line need to be identical,
otherwise it won't work. Every input after 382 needs to follow the same structure as 382 (e.g. 475(LF)). Technically speaking the 
382 part of PPM file is part of the PPM's header and informs the viewer what data to expect, as it stands though my algorithm 
currently treats it the same as image data, due to it not really having an effect on the result except in cases of picking up 
files the same resolution and encoding. It wouldn't be too difficult to make the program more robust but after spending a few more 
hours than I'd like debugging I'd prefer to take a break from this for a while.
