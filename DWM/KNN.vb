Imports System.IO
Imports System.Math

Public Class KNN

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If TextBox1.Text = "" Then
            MessageBox.Show("No tuple entered")
        ElseIf TextBox2.Text = "" Then
            MessageBox.Show("Value of 'k' not entered")
        Else

            Dim sr As New System.IO.StreamReader("C:\Users\Kbhansali\Desktop\DWM\KNN.txt")
            Dim stra() As String
            Dim st As String = ""

            Dim tuple() As String = TextBox1.Text.Split(",")
            Dim age As Double = Convert.ToInt32(tuple(0))
            Dim income As Double = Convert.ToInt32(tuple(1))

            Dim k As Integer = TextBox2.Text

            Dim list As List(Of KeyValuePair(Of String, Integer)) =
        New List(Of KeyValuePair(Of String, Integer))

            Do While sr.Peek() <> -1

                st = sr.ReadLine()
                stra = st.Split(" ")

                Dim fage As Double = Convert.ToInt32(stra(2))
                Dim fincome As Double = Convert.ToInt32(stra(3))

                Dim x As Double = age - fage
                x = x * x
                Dim y As Double = income - fincome
                y = y * y
                Dim z As Double = x + y
                z = Math.Sqrt(z)

                list.Add(New KeyValuePair(Of String, Integer)(stra(4), z))
            Loop

            Dim sum As Integer = 0
            Dim win As Integer = 0
            Dim mon As Integer = 0
            Dim ans As String = "The 'K' nearest values are:" & vbNewLine
            Dim sorted = (From item In list Order By item.Value Select item).ToList
            For Each entry As KeyValuePair(Of String, Integer) In sorted

                ans += entry.Value & vbNewLine

                If entry.Key = "Summer" Then
                    sum += 1
                ElseIf entry.Key = "Winter" Then
                    win += 1
                Else
                    mon += 1
                End If

                k -= 1
                If k = 0 Then
                    Exit For
                End If

            Next

            Dim max As Integer = sum
            Dim res As String = "Summer"
            If win > max Then
                max = win
                res = "Winter"
            End If
            If mon > max Then
                max = mon
                res = "Monsoon"
            End If

            Mining.StartPosition = FormStartPosition.Manual
            Mining.Location = Me.Location
            Mining.Show()
            Mining.RichTextBox1.Text = ans & vbNewLine & "Hence, the tuple is classified to be in '" & res & "'"
            Me.Close()

        End If
    End Sub
End Class