Imports System.Math
Imports System.IO
Public Class KMeans

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If TextBox1.Text = "" Then
            MessageBox.Show("No value entered")
        Else
            Dim ans As String = ""
            Dim cluster As Integer
            Dim dis() As Integer
            Dim mean1() As Double
            Dim mean2() As Double
            Dim pre1() As Double
            Dim pre2() As Double

            Dim sr As New System.IO.StreamReader("C:\Users\Kbhansali\Desktop\DWM\KMeans.txt")
            Dim srt As New System.IO.StreamReader("C:\Users\Kbhansali\Desktop\DWM\KMeans.txt")
            Dim stra() As String
            Dim st As String = ""

            Dim lines As Integer = 0
            cluster = TextBox1.Text
            Dim i As Integer = 0

            Do While srt.Peek() <> -1 'Counting the number to tuples

                st = srt.ReadLine()
                lines += 1

            Loop
            srt.Close()

            Dim input1(lines) As Integer
            Dim input2(lines) As Integer
            dis = New Integer(cluster - 1) {}
            mean1 = New Double(cluster - 1) {}
            mean2 = New Double(cluster - 1) {}
            pre1 = New Double(cluster - 1) {}
            pre2 = New Double(cluster - 1) {}

            Do While sr.Peek() <> -1 'Reading all tuples

                st = sr.ReadLine()
                stra = st.Split(" ")

                Dim fage As Double = Convert.ToInt32(stra(0))
                Dim fincome As Double = Convert.ToInt32(stra(1))

                input1(i) = fage
                input2(i) = fincome
                i += 1

            Loop

            Dim x(input1.Length - 1, cluster - 1) As Integer 'Creating distance matrix
            Dim y(input1.Length - 1, cluster - 1) As Integer

            i = 0
            For i = 0 To (cluster - 1) 'for calulation initial means

                mean1(i) = input1(i)
                mean2(i) = input2(i)

            Next i

            While (True)

                For i = 0 To (cluster - 1) 'Intializing distance matrix to 0
                    For j = 0 To (input1.Length - 1)
                        x(j, i) = 0
                        y(j, i) = 0
                    Next j
                Next i

                For i = 0 To (cluster - 1) 'Storing previous means
                    pre1(i) = mean1(i)
                    pre2(i) = mean2(i)
                Next i

                For i = 0 To (lines - 1) 'calculating distance of each input from all means and storing in respective array

                    Dim smallest As Integer = (Abs(input1(i) - mean1(0)) + Abs(input2(i) - mean2(0)))
                    Dim sindex As Integer = 0

                    For j = 1 To (cluster - 1)

                        dis(j) = (Abs(input1(i) - mean1(j)) + Abs((input2(i)) - mean2(j)))

                        If (dis(j) < smallest) Then
                            smallest = dis(j)
                            sindex = j
                        End If

                    Next j

                    x(i, sindex) = input1(i)
                    y(i, sindex) = input2(i)

                Next i

                For i = 0 To (cluster - 1) 'for calculating new means 
                    Dim t1 As Double = 0
                    Dim t2 As Double = 0
                    Dim count As Integer = 0

                    For h = 0 To (input1.Length - 1)

                        If (x(h, i) <> 0 Or y(h, i) <> 0) Then
                            count = count + 1
                            t1 = t1 + x(h, i)
                            t2 = t2 + y(h, i)
                        End If

                    Next h

                    mean1(i) = t1 / count
                    mean2(i) = t2 / count

                Next i


                Dim equal As Integer = 1 'for comparing previous means and new means ,if equal then stop
                For i = 0 To (cluster - 1)
                    If (pre1(i) <> mean1(i)) Then

                        equal = 0
                        Exit For

                    End If
                Next i

                If (equal = 1) Then
                    Exit While
                End If

            End While


            For i = 0 To cluster - 1 'for displaying the clusters

                ans += "Cluster" & (i + 1) & vbNewLine
                For j = 0 To input1.Length - 1

                    If (x(j, i) <> 0 Or y(j, i) <> 0) Then
                        ans += x(j, i) & "," & y(j, i) & vbNewLine
                    End If

                Next j
                ans += vbNewLine

            Next i

            Mining.StartPosition = FormStartPosition.Manual
            Mining.Location = Me.Location
            Mining.Show()
            Mining.RichTextBox1.Text = ans
            Me.Close()

        End If

    End Sub

End Class