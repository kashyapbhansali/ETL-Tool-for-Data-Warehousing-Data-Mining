Imports System.Data.OleDb
Public Class Display

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Main.StartPosition = FormStartPosition.Manual
        Main.Location = Me.Location
        Main.Show()
        Me.Close()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim connString As String = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\Users\Kbhansali\Desktop\DWM\Loaded\" & ListBox1.SelectedItem().ToString() & ".accdb"
        Dim MyConn As OleDbConnection = New OleDbConnection
        Dim da As OleDbDataAdapter
        Dim ds As DataSet
        Dim tables As DataTableCollection
        Dim source1 As New BindingSource
        Dim tab = ListBox1.SelectedItem().ToString()

        Try
            MyConn.ConnectionString = connString
            ds = New DataSet
            tables = ds.Tables
            da = New OleDbDataAdapter("Select * from " & tab, MyConn)
            da.Fill(ds, tab)
            Dim view As New DataView(tables(0))
            source1.DataSource = view
            DataGridView1.DataSource = view

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
        MyConn.Close()

    End Sub
End Class