Public Class MainForm

    Dim Day, Month, Year, Longform_Julian As Integer
    Dim Kin, Uinal, Tun, Katun, Baktun, Longform_Mayan As Integer
    Dim Months() As Integer = {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31}
    Dim DayChanged, MonthChanged, YearChanged As Boolean

    Sub MainForm_Load() Handles MyBase.Load
        Day = 1
        Month = 1
        Year = 2000

        Longform_Julian = Day + MonthCalculator(Month) + YearCalculator(Year)
        txtDay.Text = Day
        txtMonth.Text = Month
        txtYear.Text = Year
        MayanFormatter()

    End Sub


    Function MonthCalculator(ByVal Input As Integer) As Integer
        'Replaces this with a double to avoid underflow
        'Dim Leap as Integer

        'Making sure to initialise the integer explicitly this time even if VB does it for you
        'For the sake of clarity if nothing else.
        Dim DayCounter As Integer = 0
        Dim Leap As Double

        'This is actually the way this code looked originally, I changed it slightly thinking it
        'was a typo but then I realised it was all intentional.  
        'You divide the year by 100 then check If it's a whole number by doing modulo 1
        '(So you'll catch 100,200,5000, etc years)
        'If it is a whole number then check whether it's a multiple of 400 by doing modulo 4
        '(Because years that are multiples of 400 are leap years but mupltiples of 100 are not)
        'If it's not a multplier of 100 check whether it's a multipler of 4.
        '(Because outside of 100,200,300,500,etc all Leap years are multiples of 4)
        'The original code shouldn't work because underflow but making Leap a double fixes this.

        Leap = Year / 100
        If Leap Mod 1 = 0.0 Then
            Leap = Leap Mod 4
        Else
            Leap = Year Mod 4
        End If

        'Now, in all instances of a year, leap will always be 0 if it is on a leap year.
        'Even with years like 300, leap will be 3 indicating that 300 is not a leap year.

        'If Input > 2 Then
        '   If Leap = 0 Then
        '       DayCounter = DayCounter + 1
        '   End If
        'End If

        'Changed to:
        If Input > 2 & Leap = 0.0 Then
            DayCounter += 1
        End If


        'This code, surprisingly, was done with full knowledge of case, Visual Basic has a quirk
        'where cases automatically break, so you can't use them for fallthrough like you would in
        'this case where it's actually necessary.
        'I don't remember if it wasn't possible at all to do this with case at the time, or whether
        'doing it with case looked equally as ugly, but I hated this code block back when I wrote
        'it as much as I do now.

        'If Input >= 0 Then
        '   If Input >= 1 Then
        '       If Input >= 2 Then
        '           DayCounter = DayCounter + 31
        '           If Input >= 3 Then
        '               DayCounter = DayCounter + 28
        '               If Input >= 4 Then
        '                   DayCounter = DayCounter + 31
        '                   If Input >= 5 Then
        '                       DayCounter = DayCounter + 30
        '                       If Input >= 6 Then
        '                           DayCounter = DayCounter + 31
        '                           If Input >= 7 Then
        '                               DayCounter = DayCounter + 30
        '                               If Input >= 8 Then
        '                                   DayCounter = DayCounter + 31
        '                                   If Input >= 9 Then
        '                                       DayCounter = DayCounter + 31
        '                                       If Input >= 10 Then
        '                                           DayCounter = DayCounter + 30
        '                                           If Input >= 11 Then
        '                                               DayCounter = DayCounter + 31
        '                                               If Input >= 12 Then
        '                                                   DayCounter = DayCounter + 30
        '                                               End If
        '                                           End If
        '                                       End If
        '                                   End If
        '                               End If
        '                           End If
        '                       End If
        '                   End If
        '               End If
        '           End If
        '       End If
        '   End If
        '   Return DayCounter
        'End If

        'Thinking about it now rewriting this code I'd simply declare a final gobal array of months
        'Then count down from one below the current month to January, so if the input is 12, you'll
        'Get a sum of the first 11 months. Realistically this could also be hard coded having
        'indexes in the array correspond to their months and if the compiler is smart it may
        'actually do that when the code is converted to assembly, but obviously writing it like
        'this helps with readability a ton.
        'Input - 2 because you want the sum of months up to but not including the number and
        'because arrays start at 0. I could have left out month 12 because it'll never be used,
        'but I feel as if that would just cause immediate confusion when opening the class.
        'You also require the following check to make sure the input doesn't go out of bounds.

        If Input > 12 Then
            Input = 12
        End If

        For Index As Integer = Input - 2 To 0 Step -1
            DayCounter += Months(Index)
        Next

        'This was here originally but with the new code daycounter will now be 0 if the input
        'is 0 anyway, so the return 0 is no longer needed.
        'Return 0
        Return DayCounter

    End Function

    Function YearCalculator(ByVal Input As Integer) As Integer

        Dim Value400, Value100, Leaps, Leaper, Result As Integer
        'Segments of day arithmetic, turning years into days, every 4th year is a leap year except
        'in the cases of the 100th year, three out of four turns of the century are not leap years
        'so 300,500,100 are no leap years, but 400,800,1200 are. Which is why the multipler is
        '365.2425 instead of 365.25, to take into account the 3 missing leap days every 400 years.
        Value400 = Input - Input Mod 400
        Value400 = Value400 * 365.2425

        'Now that the days contained in the 2000 part of 2018 has been asertained, it's time to
        'figure out how many days are left in the 18 part of the year.
        Value100 = Input Mod 400

        'Leaps is used to figure out how many leap days are between the value 400 calculation and
        'the year provided as input.
        Leaps = Value100 - Value100 Mod 4
        Leaps = Leaps / 4

        'This figures out how many hundred of years have occured since the last 400 so their days
        'can be removed from the tally.
        If Value100 > 100 Then
            Leaper = Value100 - Value100 Mod 100
            Leaper = Leaper / 100
            'Honestly not sure why I altered Value100 here and not Leaps, I'm pretty sure that's
            'a logical error and not just a superficial change.
            'Value100 = Value100 - Leaper
            Leaps -= Leaper
        End If

        'Value100 - 1 as to not include the days in the current year, similarly as to how we did
        'Current month - 1 for the month calculator. Calculated without leap days for simplicity.
        Value100 = (Value100 - 1) * 365
        'You could argue that calculating the leaps is pointless because you could simply do 
        'Value100 * 365.25 and take away the centenial years missing leap days, but relying
        'on your values being truncated correctly seems like a bad practice to me.
        Value100 = Value100 + Leaps

        Result = Value400 + Value100

        Return Result

    End Function

    Sub MayanFormatter()

        Dim ConversionHelper As Integer

        'The following magic number is derived from the amount of days required to reach the 13th
        'Baktun (1,872,000) minus the amount of days that have elapsed since day 0 year 0 in the 
        'julian calendar (734,858) up to 21/12/2019, which is the 13th baktun. 
        'This is used to get the absolute date in days since the beginning of the mayan calender. 
        'Longform_Julian should the the date format represented in days since the 0th day of the 
        '0th Year respectively. 1,872,000 - 734,858 = 1137142
        'However this is inclusive of the 21st day, which it should not be to give an accurate
        'conversion. So a value of 1 is removed from the value derived above.
        Longform_Mayan = Longform_Julian + 1137141

        'There are 144,000 days in a Baktun
        Baktun = (Longform_Mayan - Longform_Mayan Mod 144000) / 144000
        ConversionHelper = Longform_Mayan Mod 144000
        'There are 7,200 days in a Katun, 20 Katuns in a Baktun
        Katun = (ConversionHelper - ConversionHelper Mod 7200) / 7200
        ConversionHelper = ConversionHelper Mod 7200
        'There are 360 days in a Tun, 20 Tuns in a Katun
        Tun = (ConversionHelper - ConversionHelper Mod 360) / 360
        ConversionHelper = ConversionHelper Mod 360
        'There are 20 days in a Uinal, 18 Uinals in a Tun
        Uinal = (ConversionHelper - ConversionHelper Mod 20) / 20
        ConversionHelper = ConversionHelper Mod 20
        'A Kin is the Mayan word for day, a Uinal has 20 Kins
        Kin = ConversionHelper

        txtBaktun.Text = Baktun
        txtKatun.Text = Katun
        txtTun.Text = Tun
        txtUinal.Text = Uinal
        txtKin.Text = Kin

        Me.Refresh()
    End Sub

    Sub DayCalculator(ByVal Input As Integer, ByVal Parameter As String)

        Select Case Parameter
            Case "Day"
                Longform_Julian = Input + MonthCalculator(Month) + YearCalculator(Year)
                MayanFormatter()
            Case "Month"
                Longform_Julian = Day + MonthCalculator(Input) + YearCalculator(Year)
                MayanFormatter()
            Case "Year"
                Longform_Julian = Day + MonthCalculator(Month) + YearCalculator(Input)
                MayanFormatter()
        End Select

    End Sub


    Private Sub txtDay_TextChanged(sender As Object, e As EventArgs) Handles txtDay.TextChanged
        DayChanged = True
    End Sub

    Private Sub txtMonth_TextChanged(sender As Object, e As EventArgs) Handles txtMonth.TextChanged
        MonthChanged = True
    End Sub

    Private Sub txtYear_TextChanged(sender As Object, e As EventArgs) Handles txtYear.TextChanged
        YearChanged = True
    End Sub

    Private Sub btnConvert_Click(sender As Object, e As EventArgs) Handles btnConvert.Click
        If DayChanged = True Then
            Day = txtDay.Text
            DayCalculator(Day, "Day")
            DayChanged = False
        End If

        If MonthChanged = True Then
            Month = txtMonth.Text
            DayCalculator(Month, "Month")
            MonthChanged = False
        End If

        If YearChanged = True Then
            Year = txtYear.Text
            DayCalculator(Year, "Year")
            YearChanged = False
        End If

    End Sub

End Class
