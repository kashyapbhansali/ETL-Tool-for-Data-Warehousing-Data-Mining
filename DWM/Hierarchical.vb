Public Class Hierarchical

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim input(4, 4) As Integer
        input(0, 0) = 0
        input(0, 1) = 1
        input(0, 2) = 4
        input(0, 3) = 5
        input(1, 0) = 1
        input(1, 1) = 0
        input(1, 2) = 2
        input(1, 3) = 6
        input(2, 0) = 4
        input(2, 1) = 2
        input(2, 2) = 0
        input(2, 3) = 3
        input(3, 0) = 5
        input(3, 1) = 6
        input(3, 2) = 3
        input(3, 3) = 0

        Dim a As Integer = 0
        Dim b As Integer = 1
        Dim c As Integer = 2
        Dim d As Integer = 3

        Dim ans As String = ""
        Dim clust(11) As String
        Dim ref As String = "a,b,c,d"
        Dim j As Integer

        If (input(a, b) <= input(a, c) And input(a, b) <= input(a, d) And input(a, b) <= input(b, c) And input(a, b) <= input(b, d) And input(a, b) <= input(c, d)) Then
            ref = "ab,c,d"
            j = input(a, b)
        ElseIf (input(a, c) <= input(a, b) And input(a, c) <= input(a, d) And input(a, c) <= input(b, c) And input(a, c) <= input(b, d) And input(a, c) <= input(c, d)) Then
            ref = "ac,b,d"
            j = input(a, c)
        ElseIf (input(a, d) <= input(a, b) And input(a, d) <= input(a, c) And input(a, d) <= input(b, c) And input(a, d) <= input(b, d) And input(a, d) <= input(c, d)) Then
            ref = "ad,b,c"
            j = input(a, d)
        ElseIf (input(b, c) <= input(a, b) And input(b, c) <= input(a, c) And input(b, c) <= input(a, d) And input(b, c) <= input(b, d) And input(b, c) <= input(c, d)) Then
            ref = "a,bc,d"
            j = input(b, c)
        ElseIf (input(b, d) <= input(a, b) And input(b, d) <= input(a, c) And input(b, d) <= input(a, d) And input(b, d) <= input(b, c) And input(b, d) <= input(c, d)) Then
            ref = "a,bd,c"
            j = input(b, d)
        ElseIf (input(c, d) <= input(a, b) And input(c, d) <= input(a, c) And input(c, d) <= input(a, d) And input(c, d) <= input(b, c) And input(c, d) <= input(b, d)) Then
            ref = "a,b,cd"
            j = input(c, d)
        End If

        ans += "At threshold " & j & ": " & ref & vbNewLine

        j = j + 1
        For i = j To 100

            If (input(a, b) = i And Not (ref.Contains("ab"))) Then
                If ((ref.Contains("ac") And ref.Contains("bd") And input(a, b) >= input(a, d) And input(a, b) >= input(b, c) And input(a, b) >= input(c, d)) Or (ref.Contains("ad") And ref.Contains("bc") And input(a, b) >= input(a, c) And input(a, b) >= input(b, d) And input(a, b) >= input(c, d))) Then
                    ref = "abcd"
                ElseIf (ref.Contains("bcd") And input(a, b) >= input(a, c) And input(a, b) >= input(a, d)) Then
                    ref = "abcd"
                ElseIf (ref.Contains("acd") And input(a, b) >= input(b, c) And input(a, b) >= input(b, d)) Then
                    ref = "abcd"
                ElseIf (ref.Contains("ac") And Not (ref.Contains("bd")) And input(a, b) >= input(b, c)) Then
                    ref = "abc,d"
                ElseIf (ref.Contains("ad") And Not (ref.Contains("bc")) And input(a, b) >= input(b, d)) Then
                    ref = "abd,c"
                ElseIf (ref.Contains("cd")) Then
                    ref = "ab,cd"
                ElseIf (ref.Contains("bc") And Not (ref.Contains("ad")) And input(a, b) >= input(a, c)) Then
                    ref = "abc,d"
                ElseIf (ref.Contains("bd") And Not (ref.Contains("ac")) And input(a, b) >= input(a, d)) Then
                    ref = "abc,d"
                Else
                    ref = ref
                End If

            ElseIf (input(a, c) = i And Not (ref.Contains("ac"))) Then
                If ((ref.Contains("ab") And ref.Contains("cd") And input(a, c) >= input(a, d) And input(a, c) >= input(b, c) And input(a, c) >= input(c, d)) Or (ref.Contains("ad") And ref.Contains("bc") And input(a, c) >= input(a, b) And input(a, c) >= input(b, d) And input(a, c) >= input(c, d))) Then
                    ref = "abcd"
                ElseIf (ref.Contains("bcd") And input(a, c) >= input(a, b) And input(a, c) >= input(a, d)) Then
                    ref = "abcd"
                ElseIf (ref.Contains("abd") And input(a, c) >= input(b, c) And input(a, c) >= input(b, d)) Then
                    ref = "abcd"
                ElseIf (ref.Contains("ab") And Not (ref.Contains("cd")) And input(a, c) >= input(b, c)) Then
                    ref = "abc,d"
                ElseIf (ref.Contains("ad") And Not (ref.Contains("bc")) And input(a, c) >= input(c, d)) Then
                    ref = "acd,b"
                ElseIf (ref.Contains("bc") And Not (ref.Contains("ad")) And input(a, c) >= input(a, b)) Then
                    ref = "abc,d"
                ElseIf (ref.Contains("bd") And Not (ref.Contains("ac"))) Then
                    ref = "ac,bd"
                ElseIf (ref.Contains("cd") And Not (ref.Contains("ab")) And input(a, c) >= input(a, d)) Then
                    ref = "acd,b"
                Else
                    ref = ref
                End If

            ElseIf (input(a, d) = i And Not (ref.Contains("ad"))) Then
                If ((ref.Contains("ac") And ref.Contains("bd") And input(a, d) >= input(a, b) And input(a, d) >= input(b, c) And input(a, d) >= input(c, d)) Or (ref.Contains("ab") And ref.Contains("cd") And input(a, d) >= input(a, c) And input(a, d) >= input(b, d) And input(a, d) >= input(b, c))) Then
                    ref = "abcd"
                ElseIf (ref.Contains("abc") And input(a, d) >= input(a, b) And input(a, d) >= input(a, c)) Then
                    ref = "abcd"
                ElseIf (ref.Contains("bcd") And input(a, d) >= input(b, c) And input(a, d) >= input(b, d)) Then
                    ref = "abcd"
                ElseIf (ref.Contains("ab") And Not (ref.Contains("cd")) And input(a, d) >= input(b, d)) Then
                    ref = "abd,c"
                ElseIf (ref.Contains("ac") And Not (ref.Contains("bd")) And input(a, d) >= input(c, d)) Then
                    ref = "acd,b"
                ElseIf (ref.Contains("cd") And Not (ref.Contains("ab")) And input(a, d) >= input(a, c)) Then
                    ref = "acd,b"
                ElseIf (ref.Contains("bc") And Not (ref.Contains("ad"))) Then
                    ref = "bc,ad"
                ElseIf (ref.Contains("bd") And Not (ref.Contains("ac")) And input(a, d) >= input(a, b)) Then
                    ref = "abd,c"
                Else
                    ref = ref
                End If

            ElseIf (input(b, c) = i And Not (ref.Contains("bc"))) Then
                If ((ref.Contains("ac") And ref.Contains("bd") And input(b, c) >= input(a, b) And input(b, c) >= input(c, d) And input(b, c) >= input(a, d)) Or (ref.Contains("ab") And ref.Contains("cd") And input(b, c) >= input(a, c) And input(b, c) >= input(b, d) And input(b, c) >= input(a, d))) Then
                    ref = "abcd"
                ElseIf (ref.Contains("abd") And input(b, c) >= input(a, c) And input(b, c) >= input(c, d)) Then
                    ref = "abcd"
                ElseIf (ref.Contains("acd") And input(b, c) >= input(a, b) And input(b, c) >= input(b, d)) Then
                    ref = "abcd"
                ElseIf (ref.Contains("bd") And Not (ref.Contains("ac")) And input(b, c) >= input(c, d)) Then
                    ref = "bcd,a"
                ElseIf (ref.Contains("ab") And Not (ref.Contains("cd")) And input(b, c) >= input(a, c)) Then
                    ref = "abc,d"
                ElseIf (ref.Contains("ac") And Not (ref.Contains("bd")) And input(b, c) >= input(a, b)) Then
                    ref = "abc,d"
                ElseIf (ref.Contains("cd") And Not (ref.Contains("ab")) And input(b, c) >= input(b, d)) Then
                    ref = "bcd,a"
                ElseIf (ref.Contains("ad") And Not (ref.Contains("bc"))) Then
                    ref = "ad,bc"
                Else
                    ref = ref
                End If

            ElseIf (input(b, d) = i And Not (ref.Contains("bd"))) Then
                If ((ref.Contains("ab") And ref.Contains("cd") And input(b, d) >= input(a, c) And input(b, d) >= input(b, c) And input(b, d) >= input(a, d)) Or (ref.Contains("ad") And ref.Contains("bc") And input(b, d) >= input(a, b) And input(b, d) >= input(a, c) And input(b, d) >= input(c, d))) Then
                    ref = "abcd"
                ElseIf (ref.Contains("abc") And input(b, d) >= input(a, d) And input(b, d) >= input(c, d)) Then
                    ref = "abcd"
                ElseIf (ref.Contains("acd") And input(b, d) >= input(a, b) And input(b, d) >= input(b, c)) Then
                    ref = "abcd"
                ElseIf (ref.Contains("ac") And Not (ref.Contains("bd"))) Then
                    ref = "ac,bd"
                ElseIf (ref.Contains("bc") And Not (ref.Contains("ad")) And input(b, d) >= input(c, d)) Then
                    ref = "bcd,a"
                ElseIf (ref.Contains("ab") And Not (ref.Contains("cd")) And input(b, d) >= input(a, d)) Then
                    ref = "abd,c"
                ElseIf (ref.Contains("ad") And Not (ref.Contains("bc")) And input(b, d) >= input(a, b)) Then
                    ref = "abd,c"
                ElseIf (ref.Contains("cd") And Not (ref.Contains("ab")) And input(b, d) >= input(b, c)) Then
                    ref = "bcd,a"
                Else
                    ref = ref
                End If

            ElseIf (input(c, d) = i And Not (ref.Contains("cd"))) Then
                If ((ref.Contains("ac") And ref.Contains("bd") And input(c, d) >= input(a, b) And input(c, d) >= input(b, c) And input(c, d) >= input(a, d)) Or (ref.Contains("ad") And ref.Contains("bc") And input(c, d) >= input(a, b) And input(c, d) >= input(b, d) And input(c, d) >= input(a, c))) Then
                    ref = "abcd"
                ElseIf (ref.Contains("abc") And input(c, d) >= input(a, d) And input(c, d) >= input(b, d)) Then
                    ref = "abcd"
                ElseIf (ref.Contains("abd") And input(c, d) >= input(a, c) And input(c, d) >= input(b, c)) Then
                    ref = "abcd"
                ElseIf (ref.Contains("ab") And Not (ref.Contains("cd"))) Then
                    ref = "ab,cd"
                ElseIf (ref.Contains("bc") And Not (ref.Contains("ad")) And input(c, d) >= input(b, d)) Then
                    ref = "bcd,a"
                ElseIf (ref.Contains("bd") And Not (ref.Contains("ac")) And input(c, d) >= input(b, c)) Then
                    ref = "bcd,a"
                ElseIf (ref.Contains("ad") And Not (ref.Contains("bc")) And input(c, d) >= input(a, c)) Then
                    ref = "acd,b"
                ElseIf (ref.Contains("ac") And Not (ref.Contains("bd")) And input(c, d) >= input(a, d)) Then
                    ref = "acd,d"
                Else
                    ref = ref
                End If

            Else
                ref = ref
            End If

            clust(i) = ref
            ans += "At threshold " & i & ": " & clust(i) & vbNewLine
            If (ref.Contains("abcd")) Then
                Exit For
            End If

        Next i

        Mining.StartPosition = FormStartPosition.Manual
        Mining.Location = Me.Location
        Mining.Show()
        Mining.RichTextBox1.Text = ans
        Me.Close()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim input(4, 4) As Integer
        input(0, 0) = 0
        input(0, 1) = 1
        input(0, 2) = 4
        input(0, 3) = 5
        input(1, 0) = 1
        input(1, 1) = 0
        input(1, 2) = 2
        input(1, 3) = 6
        input(2, 0) = 4
        input(2, 1) = 2
        input(2, 2) = 0
        input(2, 3) = 3
        input(3, 0) = 5
        input(3, 1) = 6
        input(3, 2) = 3
        input(3, 3) = 0

        Dim a As Integer = 0
        Dim b As Integer = 1
        Dim c As Integer = 2
        Dim d As Integer = 3

        Dim ans As String = ""
        Dim clust(11) As String
        Dim ref As String = "a,b,c,d"
        Dim i As Integer

        For i = 0 To 100

            If (input(a, b) = i And Not (ref.Contains("ab"))) Then
                If (ref.Contains("ab") And ref.Contains("bd")) Or (ref.Contains("ad") And ref.Contains("bc")) Then
                    ref = "abcd"
                ElseIf (ref.Contains("bcd")) Then
                    ref = "abcd"
                ElseIf (ref.Contains("acd")) Then
                    ref = "abcd"
                ElseIf (ref.Contains("ac")) Then
                    ref = "abc,d"
                ElseIf (ref.Contains("ad")) Then
                    ref = "abd,c"
                ElseIf (ref.Contains("bc")) Then
                    ref = "abc,d"
                ElseIf (ref.Contains("bd")) Then
                    ref = "abd,c"
                ElseIf (ref.Contains("cd")) Then
                    ref = "ab,cd"
                Else
                    ref = "ab,c,d"
                End If

            ElseIf (input(a, c) = i And Not (ref.Contains("ac"))) Then
                If ((ref.Contains("ab") And ref.Contains("cd")) Or (ref.Contains("ad") And ref.Contains("bc"))) Then
                    ref = "abcd"
                ElseIf (ref.Contains("bcd")) Then
                    ref = "abcd"
                ElseIf (ref.Contains("abd")) Then
                    ref = "abcd"
                ElseIf (ref.Contains("ab")) Then
                    ref = "abc,d"
                ElseIf (ref.Contains("ad")) Then
                    ref = "acd,b"
                ElseIf (ref.Contains("bc")) Then
                    ref = "abc,d"
                ElseIf (ref.Contains("bd")) Then
                    ref = "ac,bd"
                ElseIf (ref.Contains("cd")) Then
                    ref = "acd,b"
                Else
                    ref = "ac,b,d"
                End If

            ElseIf (input(a, d) = i And Not (ref.Contains("ad"))) Then
                If ((ref.Contains("ab") And ref.Contains("cd")) Or (ref.Contains("ac") And ref.Contains("bd"))) Then
                    ref = "abcd"
                ElseIf (ref.Contains("bcd")) Then
                    ref = "abcd"
                ElseIf (ref.Contains("abc")) Then
                    ref = "abcd"
                ElseIf (ref.Contains("ab")) Then
                    ref = "abd,c"
                ElseIf (ref.Contains("ac")) Then
                    ref = "acd,b"
                ElseIf (ref.Contains("bc")) Then
                    ref = "bc,ad"
                ElseIf (ref.Contains("bd")) Then
                    ref = "abd,c"
                ElseIf (ref.Contains("cd")) Then
                    ref = "acd,b"
                Else
                    ref = "ad,b,c"
                End If

            ElseIf (input(b, c) = i And Not (ref.Contains("bc"))) Then
                If ((ref.Contains("ab") And ref.Contains("cd")) Or (ref.Contains("ac") And ref.Contains("bd"))) Then
                    ref = "abcd"
                ElseIf (ref.Contains("acd")) Then
                    ref = "abcd"
                ElseIf (ref.Contains("abd")) Then
                    ref = "abcd"
                ElseIf (ref.Contains("ab")) Then
                    ref = "abc,d"
                ElseIf (ref.Contains("ac")) Then
                    ref = "abc,d"
                ElseIf (ref.Contains("ad")) Then
                    ref = "ad,bc"
                ElseIf (ref.Contains("bd")) Then
                    ref = "bcd,a"
                ElseIf (ref.Contains("cd")) Then
                    ref = "bcd,a"
                Else
                    ref = "bc,a,d"
                End If

            ElseIf (input(b, d) = i And Not (ref.Contains("bd"))) Then
                If ((ref.Contains("ab") And ref.Contains("cd")) Or (ref.Contains("ad") And ref.Contains("bc"))) Then
                    ref = "abcd"
                ElseIf (ref.Contains("abc")) Then
                    ref = "abcd"
                ElseIf (ref.Contains("acd")) Then
                    ref = "abcd"
                ElseIf (ref.Contains("ab")) Then
                    ref = "abd,c"
                ElseIf (ref.Contains("ac")) Then
                    ref = "ac,bd"
                ElseIf (ref.Contains("ad")) Then
                    ref = "abd,c"
                ElseIf (ref.Contains("bc")) Then
                    ref = "bcd,a"
                ElseIf (ref.Contains("cd")) Then
                    ref = "bcd,a"
                Else
                    ref = "bd,a,c"
                End If

            ElseIf (input(c, d) = i And Not (ref.Contains("cd"))) Then
                If ((ref.Contains("ac") And ref.Contains("bd")) Or (ref.Contains("ad") And ref.Contains("bc"))) Then
                    ref = "abcd"
                ElseIf (ref.Contains("abd")) Then
                    ref = "abcd"
                ElseIf (ref.Contains("abc")) Then
                    ref = "abcd"
                ElseIf (ref.Contains("ab")) Then
                    ref = "ab,cd"
                ElseIf (ref.Contains("ac")) Then
                    ref = "acd,b"
                ElseIf (ref.Contains("bc")) Then
                    ref = "bcd,a"
                ElseIf (ref.Contains("bd")) Then
                    ref = "bcd,a"
                ElseIf (ref.Contains("ad")) Then
                    ref = "acd,b"
                Else
                    ref = "cd,a,b"
                End If

            Else
                ref = ref

            End If

            clust(i) = ref
            ans += "At threshold " & i & ": " & clust(i) & vbNewLine
            If (ref.Contains("abcd")) Then
                Exit For
            End If

        Next i

        Mining.StartPosition = FormStartPosition.Manual
        Mining.Location = Me.Location
        Mining.Show()
        Mining.RichTextBox1.Text = ans
        Me.Close()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Mining.StartPosition = FormStartPosition.Manual
        Mining.Location = Me.Location
        Mining.Show()
        Me.Close()

    End Sub
End Class