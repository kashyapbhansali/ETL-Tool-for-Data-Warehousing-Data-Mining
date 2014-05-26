Public Class Mining

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Main.StartPosition = FormStartPosition.Manual
        Main.Location = Me.Location
        Main.Show()
        Me.Close()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Bayesian.StartPosition = FormStartPosition.Manual
        Bayesian.Location = Me.Location
        Bayesian.Show()

    End Sub


    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        KNN.StartPosition = FormStartPosition.Manual
        KNN.Location = Me.Location
        KNN.Show()

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        KMeans.StartPosition = FormStartPosition.Manual
        KMeans.Location = Me.Location
        KMeans.Show()

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        Hierarchical.StartPosition = FormStartPosition.Manual
        Hierarchical.Location = Me.Location
        Hierarchical.Show()

    End Sub

End Class