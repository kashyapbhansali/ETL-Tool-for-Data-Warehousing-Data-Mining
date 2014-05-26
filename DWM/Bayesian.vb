Imports System.Text.RegularExpressions
Imports System.IO
Public Class Bayesian

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If RichTextBox1.Text = "" Then
            MessageBox.Show("No tuple entered")
        Else
            Dim sr As New System.IO.StreamReader("C:\Users\Kbhansali\Desktop\DWM\Bayesian.txt")
            Dim stra() As String
            Dim lines As Double = 0
            Dim st As String = ""

            Dim sum As Double = 0
            Dim win As Double = 0
            Dim mon As Double = 0

            Dim msum As Double = 0
            Dim mwin As Double = 0
            Dim mmon As Double = 0
            Dim fsum As Double = 0
            Dim fwin As Double = 0
            Dim fmon As Double = 0

            Dim sum1 As Double = 0
            Dim win1 As Double = 0
            Dim mon1 As Double = 0
            Dim sum2 As Double = 0
            Dim win2 As Double = 0
            Dim mon2 As Double = 0
            Dim sum3 As Double = 0
            Dim win3 As Double = 0
            Dim mon3 As Double = 0
            Dim sum4 As Double = 0
            Dim win4 As Double = 0
            Dim mon4 As Double = 0

            Do While sr.Peek() <> -1

                st = sr.ReadLine()
                lines += 1
                stra = st.Split(" ")

                Dim chk1 As Boolean = Regex.IsMatch(stra(UBound(stra)), "Monsoon")
                Dim chk2 As Boolean = Regex.IsMatch(stra(UBound(stra)), "Summer")
                Dim chk3 As Boolean = Regex.IsMatch(stra(UBound(stra)), "Winter")
                Dim chk4 As Boolean = Regex.IsMatch(stra(2), "M")
                Dim chk5 As Boolean = Regex.IsMatch(stra(2), "F")
                Dim age As Double = Convert.ToInt32(stra(3))

                If chk1 = True Then
                    mon += 1
                End If

                If chk2 = True Then
                    sum += 1
                End If

                If chk3 = True Then
                    win += 1
                End If

                If chk4 = True And chk1 = True Then
                    mmon += 1
                End If

                If chk5 = True And chk1 = True Then
                    fmon += 1
                End If

                If chk4 = True And chk2 = True Then
                    msum += 1
                End If

                If chk5 = True And chk2 = True Then
                    fsum += 1
                End If

                If chk4 = True And chk3 = True Then
                    mwin += 1
                End If

                If chk5 = True And chk3 = True Then
                    fwin += 1
                End If

                If age > 0 And age <= 20 Then

                    If chk1 = True Then
                        mon1 += 1
                    End If

                    If chk2 = True Then
                        sum1 += 1
                    End If

                    If chk3 = True Then
                        win1 += 1
                    End If

                End If

                If age > 20 And age <= 40 Then

                    If chk1 = True Then
                        mon2 += 1
                    End If

                    If chk2 = True Then
                        sum2 += 1
                    End If

                    If chk3 = True Then
                        win2 += 1
                    End If

                End If

                If age > 40 And age <= 60 Then

                    If chk1 = True Then
                        mon3 += 1
                    End If

                    If chk2 = True Then
                        sum3 += 1
                    End If

                    If chk3 = True Then
                        win3 += 1
                    End If

                End If

                If age > 60 Then

                    If chk1 = True Then
                        mon4 += 1
                    End If

                    If chk2 = True Then
                        sum4 += 1
                    End If

                    If chk3 = True Then
                        win4 += 1
                    End If

                End If

            Loop

            Dim psum As Double = sum / lines
            Dim pwin As Double = win / lines
            Dim pmon As Double = mon / lines

            Dim pmsum As Double = msum / sum
            Dim pmwin As Double = mwin / win
            Dim pmmon As Double = mmon / mon

            Dim pfsum As Double = fsum / sum
            Dim pfwin As Double = fwin / win
            Dim pfmon As Double = fmon / mon

            Dim psum1 As Double = sum1 / sum
            Dim pwin1 As Double = win1 / win
            Dim pmon1 As Double = mon1 / mon
            Dim psum2 As Double = sum2 / sum
            Dim pwin2 As Double = win2 / win
            Dim pmon2 As Double = mon2 / mon
            Dim psum3 As Double = sum3 / sum
            Dim pwin3 As Double = win3 / win
            Dim pmon3 As Double = mon3 / mon
            Dim psum4 As Double = sum4 / sum
            Dim pwin4 As Double = win4 / win
            Dim pmon4 As Double = mon4 / mon

            Dim tuple() As String = RichTextBox1.Text.Split(",")
            Dim iage As Double = Convert.ToInt32(tuple(1))
            Dim cons1 As Double = 0
            Dim cons2 As Double = 0
            Dim cons3 As Double = 0

            If iage > 0 And iage <= 20 Then
                cons1 = psum1
                cons2 = pwin1
                cons3 = pmon1
            ElseIf iage > 20 And iage <= 40 Then
                cons1 = psum2
                cons2 = pwin2
                cons3 = pmon2
            ElseIf iage > 40 And iage <= 60 Then
                cons1 = psum3
                cons2 = pwin3
                cons3 = pmon3
            Else
                cons1 = psum4
                cons2 = pwin4
                cons3 = pmon4
            End If

            Dim ptsum As Double = 0
            Dim ptwin As Double = 0
            Dim ptmon As Double = 0

            If tuple(0) = "M" Then
                ptsum = pmsum * cons1
                ptwin = pmwin * cons2
                ptmon = pmmon * cons3
            Else
                ptsum = pfsum * cons1
                ptwin = pfwin * cons2
                ptmon = pfmon * cons3
            End If

            Dim lsum As Double = psum * ptsum
            Dim lwin As Double = pwin * ptwin
            Dim lmon As Double = pmon * ptmon

            Dim pt As Double = lsum + lwin + lmon

            Dim pmont As Double = lmon / pt
            Dim psumt As Double = lsum / pt
            Dim pwint As Double = lwin / pt

            Dim ans As String = "Summer"
            Dim max As Double = psumt
            If max < pwint Then
                max = pwint
                ans = "Winter"
            End If
            If max < pmont Then
                max = pmont
                ans = "Monsoon"
            End If

            Dim answer As String = ""
            answer += "By Bayes Theorem:" & vbNewLine
            answer += "Probability of the tuple being in 'Summer' = " & psumt & vbNewLine
            answer += "Probability of the tuple being in 'Winter' = " & pwint & vbNewLine
            answer += "Probability of the tuple being in 'Monsoon' = " & pmont & vbNewLine
            answer += vbNewLine & "Hence, the tuple is classified to be in '" & ans & "'"

            Mining.StartPosition = FormStartPosition.Manual
            Mining.Location = Me.Location
            Mining.Show()
            Mining.RichTextBox1.Text = answer
            Me.Close()
        End If
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged

    End Sub
End Class