
Module ImageAggregator
    'Variables made global due to their ubiquity in all methods.
    Dim folderPath As String
    Dim fileType As String
    Dim widthAndHeight As String

    Sub Main(args As String())
        'This code is to allow the input to be done by hand or by drag and drop.
        If (args.Length <> 0) Then
            folderPath = args(0)
        Else
            Console.WriteLine("Where are the PPM files for aggregation?")
            folderPath = Console.ReadLine()
        End If

        'This ensures formatting consistency in the write stage, while also allowing flexibility
        'in the format which the user can write the input address.
        If folderPath.Last <> "\" Then
            folderPath += "\"
        End If

        If (Not SetupFiles()) Then
            Console.ReadLine()
        Else
            CreateAggregate()
            Console.WriteLine("New file complete, saved under: " + folderPath + "result.ppm")
        End If

        Console.ReadLine()
    End Sub

    'This function makes sure line input is not a comment, comments in PPM files are signified 
    'as hashes. This works fine but the application will break if the comment is the last line 
    'before the end of the file.
    Function ValidLineInput(ByVal InputIndex) As String
        Dim Result As String
        Do
            Result = LineInput(InputIndex)
        Loop Until Result.First <> "#"
        Return Result
    End Function

    'Gets all ppm files from the designated folderPath, prints error messages if the program
    'cannot continue based off the contents of that path.
    'Specifically if there are no ppm files, or if the amount of files are less than three,
    'because you need at least 3 values to average an input.
    Function SetupFiles() As Boolean
        Dim files = My.Computer.FileSystem.GetFiles(
                folderPath, FileIO.SearchOption.SearchTopLevelOnly, "*.ppm")

        If files.Count = 0 Then
            Console.WriteLine("There are no PPM files at this location")
            Return False
        End If

        If (InitialiseFiles(files) < 4) Then
            Console.WriteLine("Cannot Aggregate less than 3 files")
            Return False
        End If

        Return True
    End Function

    'This method assumes that the first file alphabetically is the .ppm file you want to aggregate
    'and that all files which have the same formatting (P3 or P5 in the case of PPM) and resolution
    'are sibling timelapse photographs that can be aggregated. It then sets up all valid files in
    'the file IO system and closes all .ppm files the wrong format or resolution.
    'Finally, the return is the next free FileIO location, which communicates to SetupFiles()
    'how many FileIO instances have been opened, if the next location is 4, then that means IO
    'slots 1-3 are occupied (meaning 3 files are being read) due to the way the code is designed.
    Function InitialiseFiles(ByRef files) As Integer
        FileOpen(1, files(0), OpenMode.Input)
        fileType = ValidLineInput(1)
        widthAndHeight = ValidLineInput(1)

        For i = 1 To files.Count - 1
            Dim CurrentFile = FreeFile()
            Dim tempType As String
            Dim tempWidthAndHeight As String
            FileOpen(CurrentFile, files(i), OpenMode.Input)
            tempType = ValidLineInput(CurrentFile)
            tempWidthAndHeight = ValidLineInput(CurrentFile)
            If (tempWidthAndHeight <> widthAndHeight) OrElse (tempType <> fileType) Then
                FileClose(CurrentFile)
            End If
        Next

        Return FreeFile()
    End Function

    'This method, takes in the current 
    Sub CreateAggregate()
        'Because the files are initialised sequentially, the inputs are like an array, whose size
        'we can determine by seeing the next free file location and decrementing it, to get our
        'last initalised file location.
        Dim arraySize = FreeFile() - 1
        'Visual basic initialises arrays up to the value, e.g. Array(15) contains indexes 0 - 15
        'so you need to decrement the value again to get the right size.
        Dim currentRGBValue(arraySize - 1) As Byte
        'This holds our final PPM file.
        Dim resultPPM As New Text.StringBuilder

        'Necessary formatting for the PPM header, the filetype and resolution.
        resultPPM.Append(fileType + vbLf)
        resultPPM.Append(widthAndHeight + vbLf)

        'This is our code logic, we basically run an insertion sort for every pixel index,
        'then pick out the median value, which is the least likely to be corrupted and append
        'it to our file
        While (Not EOF(1))
            'To begin an insertion sort you need at least one value to compare it to
            Byte.TryParse(LineInput(1), currentRGBValue(0))

            For currentFile = 2 To arraySize
                'Byte data type chosen because PPM files cannot exceed byte sized inputs
                Dim tempValue As Byte
                'This is to keep track of the current index, we do this so we don't compare our
                'current input to old values that weren't wiped from the array
                Dim subCounter = currentFile - 1
                'Used to keep track of when the right position for a variable has been found
                Dim foundInsertionIndex = False

                tempValue = Byte.Parse(ValidLineInput(currentFile))
                'Standard insertion sort
                While Not foundInsertionIndex And subCounter <> 0
                    If (tempValue < currentRGBValue(subCounter - 1)) Then
                        currentRGBValue(subCounter) = currentRGBValue(subCounter - 1)
                        subCounter -= 1
                    Else
                        foundInsertionIndex = True
                    End If
                End While
                currentRGBValue(subCounter) = tempValue
            Next
            'Get the median value from the array
            resultPPM.Append(currentRGBValue(Math.Round(arraySize / 2)))
            'Formatting so the results are intelligable
            resultPPM.Append(vbLf)
        End While

        'Close all files since they'll no longer be used
        For i = 1 To FreeFile() - 1
            FileClose(i)
        Next

        'Writes all data to a resultant file.
        'This part of the code was actually the most difficult, due to a line writing issue
        'surrounding UTF-8, using most other ways to write a file Visual Basic will add three
        'propietary characters to the beginning of a file, called the Byte Order Mark.
        'This caused the resulting PPMs to not be intelligible to 90% of PPM readers, while also
        'not having any visible distinctions from any other PPMs in your standard text editor.
        FileOpen(1, folderPath + "result.ppm", OpenMode.Binary)
        FilePut(1, resultPPM.ToString(), -1, False)
        FileClose(1)

    End Sub

End Module