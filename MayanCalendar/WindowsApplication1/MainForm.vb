Public Class MainForm

    Dim Day, Month, Year, Longform_Julian As Integer
    Dim Kin, Uinal, Tun, Katun, Baktun, Longform_Mayan As Integer
    Dim DayChanged, MonthChanged, YearChanged As Boolean

    Sub MainForm_Load() Handles MyBase.Load
        Day = 1
        Month = 1
        Year = 4000

        Longform_Julian = Day + MonthCalculator(Month) + YearCalculator(Year)
        txtDay.Text = Day
        txtMonth.Text = Month
        txtYear.Text = Year
        MayanFormatter()

    End Sub


    Function MonthCalculator(ByVal Input As Integer) As Integer

        Dim Leap, Tyrant As Integer

        Leap = Year Mod 400
        Leap /= 100
        If Leap Mod 2 = 0 Then
            Leap = Leap Mod 4
        Else
            Leap = Year Mod 4
        End If

        If Input > 2 Then
            If Leap = 0 Then
                Tyrant = Tyrant + 1
            End If
        End If

        If Input >= 0 Then
            If Input >= 1 Then
                If Input >= 2 Then
                    Tyrant = Tyrant + 31
                    If Input >= 3 Then
                        Tyrant = Tyrant + 28
                        If Input >= 4 Then
                            Tyrant = Tyrant + 31
                            If Input >= 5 Then
                                Tyrant = Tyrant + 30
                                If Input >= 6 Then
                                    Tyrant = Tyrant + 31
                                    If Input >= 7 Then
                                        Tyrant = Tyrant + 30
                                        If Input >= 8 Then
                                            Tyrant = Tyrant + 31
                                            If Input >= 9 Then
                                                Tyrant = Tyrant + 31
                                                If Input >= 10 Then
                                                    Tyrant = Tyrant + 30
                                                    If Input >= 11 Then
                                                        Tyrant = Tyrant + 31
                                                        If Input >= 12 Then
                                                            Tyrant = Tyrant + 30
                                                        End If
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
            Return Tyrant
        End If

        Return 0

    End Function

    Function YearCalculator(ByVal Input As Integer) As Integer

        Dim Value400, Value100, Leaps, Leaper, Result As Integer

        Value400 = Input - Input Mod 400
        Value400 = Value400 * 365.2425

        Value100 = Input Mod 400


        Leaps = Value100 - Value100 Mod 4
        Leaps = Leaps / 4

        If Value100 > 100 Then
            Leaper = Value100 - Value100 Mod 100
            Leaper = Leaper / 100
            Value100 = Value100 - Leaper
        End If

        Value100 = (Value100 - 1) * 365
        Value100 = Value100 + Leaps

        Result = Value400 + Value100

        Return Result

    End Function

    Sub MayanFormatter()

        Dim ConversionHelper As Integer

        Longform_Mayan = Longform_Julian + 1137140

        Baktun = (Longform_Mayan - Longform_Mayan Mod 144000) / 144000
        ConversionHelper = Longform_Mayan Mod 144000
        Katun = (ConversionHelper - ConversionHelper Mod 7200) / 7200
        ConversionHelper = ConversionHelper Mod 7200
        Tun = (ConversionHelper - ConversionHelper Mod 360) / 360
        ConversionHelper = ConversionHelper Mod 360
        Uinal = (ConversionHelper - ConversionHelper Mod 20) / 20
        ConversionHelper = ConversionHelper Mod 20
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
