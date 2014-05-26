Public Class Start

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Main.StartPosition = FormStartPosition.Manual
        Main.Location = Me.Location
        Main.Show()
        Me.Close()

    End Sub

End Class
