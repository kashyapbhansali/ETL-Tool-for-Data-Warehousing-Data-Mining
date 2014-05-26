Imports System.IO
Imports System.Data.OleDb
Imports MySql.Data.MySqlClient
Imports System.Text.RegularExpressions

Public Class Main

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        If (ListBox1.SelectedIndex < 0) Then 'Verify text file
            MessageBox.Show("Select dimension")

        Else

            Dim ofd As New OpenFileDialog
            ofd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
            If ofd.ShowDialog() = DialogResult.OK Then
                Label3.Text = ofd.FileName

            End If
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        If (ListBox1.SelectedIndex < 0) Then 'Verify access file
            MessageBox.Show("Select dimension")
        Else
            Dim ofd As New OpenFileDialog
            ofd.Filter = "access files (*.accdb)|*.accdb|All files (*.*)|*.*"
            If ofd.ShowDialog() = DialogResult.OK Then
                Label4.Text = ofd.FileName
            End If
        End If

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        If (ListBox1.SelectedIndex < 0) Then 'Verify sql file
            MessageBox.Show("Select dimension")
        Else
            Dim ofd As New OpenFileDialog
            ofd.Filter = "sql files (*.sql)|*.sql|All files (*.*)|*.*"
            If ofd.ShowDialog() = DialogResult.OK Then
                Label5.Text = ofd.FileName
            End If
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Mining.StartPosition = FormStartPosition.Manual
        Mining.Location = Me.Location
        Mining.Show()
        Me.Close()

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click

        If Label3.Text = "" And Label4.Text = "" And Label5.Text = "" Then
            MessageBox.Show("No source selected")
        Else

            Dim chk1 As Boolean = Regex.IsMatch(Label3.Text, "\.+txt\b")
            Dim chk2 As Boolean = Regex.IsMatch(Label4.Text, "\.+accdb\b")
            Dim chk3 As Boolean = Regex.IsMatch(Label5.Text, "\.+sql\b")

            If chk1 = True Then 'If text

                RichTextBox1.Text += "Extracting..." & vbNewLine & vbNewLine
                Dim counterkeep As New System.IO.StreamReader("C:\Users\Kbhansali\Desktop\DWM\Count\" & ListBox1.SelectedItem().ToString() & "Text.txt")
                Dim counter As String = counterkeep.ReadLine()
                counterkeep.Close()
                Dim tracker As Integer = Convert.ToInt32(counter)
                Dim count As Integer = tracker

                Dim sr As New System.IO.StreamReader(Label3.Text)
                Dim stra As String = ""
                Dim st As String = ""

                Do While sr.Peek() <> -1

                    If tracker <> 0 Then
                        st = sr.ReadLine()
                        tracker = tracker - 1
                    Else
                        count = count + 1
                        st = sr.ReadLine() & vbNewLine
                        stra += st
                    End If

                Loop
                sr.Close()
                RichTextBox1.Text += stra
                My.Computer.FileSystem.WriteAllText("C:\Users\Kbhansali\Desktop\DWM\Count\" & ListBox1.SelectedItem().ToString() & "Text.txt", count, False)
                My.Computer.FileSystem.WriteAllText("C:\Users\Kbhansali\Desktop\DWM\Extracted\" & ListBox1.SelectedItem().ToString() & ".txt", stra, True)
                RichTextBox1.Text += vbNewLine & "Done extracting" & vbNewLine & vbNewLine
                Label3.Text = ""

            End If

            If chk2 = True Then  'If access

                RichTextBox1.Text += "Extracting..." & vbNewLine & vbNewLine
                Dim counterkeep As New System.IO.StreamReader("C:\Users\Kbhansali\Desktop\DWM\Count\" & ListBox1.SelectedItem().ToString() & "Access.txt")
                Dim counter As String = counterkeep.ReadLine()
                counterkeep.Close()
                Dim tracker As Integer = Convert.ToInt32(counter)
                Dim count As Integer = tracker

                'For new Access database connection
                Dim con As New OleDb.OleDbConnection
                Dim dbProvider As String
                Dim dbSource As String
                'For .accdb
                dbProvider = "PROVIDER=Microsoft.ACE.OLEDB.12.0;"
                dbSource = "Data Source = " & Label4.Text

                con.ConnectionString = dbProvider & dbSource
                con.Open()

                'sql query here
                Dim sql As String = "SELECT * FROM " & ListBox1.SelectedItem().ToString().ToLower
                Dim ds As New DataSet
                Dim da As New OleDb.OleDbDataAdapter
                da = New OleDb.OleDbDataAdapter(sql, con)

                'dataset variable product
                da.Fill(ds, ListBox1.SelectedItem().ToString())
                Dim records As String = ""
                Dim x As Integer = 0
                Dim y As Integer = 0

                Do While x <> ds.Tables(ListBox1.SelectedItem().ToString()).Rows.Count 'Max rows in table

                    If tracker <> 0 Then
                        tracker = tracker - 1
                    Else
                        y = 0
                        Do While y <> ds.Tables(ListBox1.SelectedItem().ToString()).Columns.Count 'Max columns in table

                            records = records & ds.Tables(ListBox1.SelectedItem().ToString()).Rows(x).Item(y) & " "
                            y = y + 1

                        Loop
                        count = count + 1
                        records = records & vbNewLine
                    End If
                    x = x + 1

                Loop
                con.Close()
                RichTextBox1.Text += records
                My.Computer.FileSystem.WriteAllText("C:\Users\Kbhansali\Desktop\DWM\Count\" & ListBox1.SelectedItem().ToString() & "Access.txt", count, False)
                My.Computer.FileSystem.WriteAllText("C:\Users\Kbhansali\Desktop\DWM\Extracted\" & ListBox1.SelectedItem().ToString() & ".txt", records, True)
                RichTextBox1.Text += vbNewLine & "Done extracting" & vbNewLine & vbNewLine
                Label4.Text = ""

            End If

            If chk3 = True Then  'If SQL

                RichTextBox1.Text += "Extracting..." & vbNewLine & vbNewLine
                Dim counterkeep As New System.IO.StreamReader("C:\Users\Kbhansali\Desktop\DWM\Count\" & ListBox1.SelectedItem().ToString() & "SQL.txt")
                Dim counter As String = counterkeep.ReadLine()
                counterkeep.Close()
                Dim tracker As Integer = Convert.ToInt32(counter)
                Dim count As Integer = tracker

                Dim rows As String = ""
                Dim query As String = "SELECT * FROM " & ListBox1.SelectedItem().ToString().ToLower
                Dim connStr As String = "server=localhost;" _
                    & "user id=root;" _
                    & "password=;" _
                    & "database=tourstravels"

                Dim connection As New MySqlConnection(connStr)
                Dim cmd As New MySqlCommand(query, connection)
                Try
                    connection.Open()

                    Dim reader As MySqlDataReader = cmd.ExecuteReader()
                    While reader.Read()

                        If tracker <> 0 Then
                            tracker = tracker - 1
                        Else
                            count = count + 1
                            Dim i As Integer = 0
                            While i < reader.FieldCount
                                rows += reader.GetString(i) & " "
                                i += 1
                            End While
                            rows += vbNewLine
                        End If

                    End While
                    reader.Close()

                Catch ex As MySqlException
                    MessageBox.Show("Error: " & ex.ToString())

                Finally
                    connection.Close()
                End Try

                RichTextBox1.Text += rows
                My.Computer.FileSystem.WriteAllText("C:\Users\Kbhansali\Desktop\DWM\Count\" & ListBox1.SelectedItem().ToString() & "SQL.txt", count, False)
                My.Computer.FileSystem.WriteAllText("C:\Users\Kbhansali\Desktop\DWM\Extracted\" & ListBox1.SelectedItem().ToString() & ".txt", rows, True)
                RichTextBox1.Text += vbNewLine & "Done extracting" & vbNewLine & vbNewLine
                Label5.Text = ""

            End If

        End If

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        If (ListBox1.SelectedIndex < 0) Then
            MessageBox.Show("Select dimension")
        Else

            If ListBox1.SelectedItem().ToString() = "Customer" Then 'Transform customer 

                RichTextBox1.Text += "Transforming..." & vbNewLine & vbNewLine

                Dim sr As New System.IO.StreamReader("C:\Users\Kbhansali\Desktop\DWM\Extracted\Customer.txt")
                Dim st As String = ""
                Dim ans As String = ""
                Dim MyArray As New ArrayList
                Dim strLine As String = ""

                Do While sr.Peek() <> -1

                    st = sr.ReadLine()
                    st = st.Trim()
                    st = Regex.Replace(st, "\s+", " ")
                    Dim stra() As String = st.Split(" ")

                    If UBound(stra) >= 1 And UBound(stra) < 8 And Regex.IsMatch(stra(0), "^[0-9]{1,3}") Then 'Handling missing values (Last name in this case)
                        stra(0) = stra(0) + " " + stra(1) + " " + "NULL" + " " + stra(2) + " " + stra(3) + " " + stra(4) + " " + stra(5) + " " + stra(6) + " " + stra(7)
                        stra = stra(0).Split(" ")
                    End If

                    If stra(4) = "0" Then 'Gender to uniform form
                        stra(4) = "F"
                    ElseIf stra(4) = "1" Then
                        stra(4) = "M"
                    End If

                    Dim chk As Boolean = Regex.IsMatch(stra(8), "-") 'Date to uniform format
                    If chk = True Then
                        stra(8) = Replace(stra(8), "-", "/")
                    End If

                    Dim ag() As String = stra(8).Split("/")  'Splitting dob and calculating age
                    Dim ye As Integer = Convert.ToInt32(DateTime.Now.Year) - Convert.ToInt32(ag(2))
                    stra(8) = stra(8) + " " + ye.ToString()

                    strLine = Join(stra, " ")
                    If MyArray.Contains(strLine) = False Then 'Removing duplicate tuples
                        MyArray.Add(strLine)
                        ans += strLine & vbNewLine
                    End If
                Loop

                sr.Close()
                RichTextBox1.Text += ans
                My.Computer.FileSystem.WriteAllText("C:\Users\Kbhansali\Desktop\DWM\Transformed\" & ListBox1.SelectedItem().ToString() & ".txt", ans, False)
                RichTextBox1.Text += vbNewLine & "Done transforming" & vbNewLine & vbNewLine

            End If

            If ListBox1.SelectedItem().ToString() = "Company" Then 'Transform company

                RichTextBox1.Text += "Transforming..." & vbNewLine & vbNewLine

                Dim sr As New System.IO.StreamReader("C:\Users\Kbhansali\Desktop\DWM\Extracted\Company.txt")
                Dim ans As String = ""
                Dim MyArray As New ArrayList
                Dim strLine As String

                Do While sr.Peek <> -1 'Removing duplicate tuples
                    strLine = sr.ReadLine()
                    strLine = strLine.Trim()
                    strLine = Regex.Replace(strLine, "\s+", " ")
                    If MyArray.Contains(strLine) = False Then
                        MyArray.Add(strLine)
                        ans += strLine & vbNewLine
                    End If
                Loop

                sr.Close()
                RichTextBox1.Text += ans
                My.Computer.FileSystem.WriteAllText("C:\Users\Kbhansali\Desktop\DWM\Transformed\" & ListBox1.SelectedItem().ToString() & ".txt", ans, False)
                RichTextBox1.Text += vbNewLine & "Done transforming" & vbNewLine & vbNewLine

            End If

            If ListBox1.SelectedItem().ToString() = "Flight" Then 'Transform flight

                RichTextBox1.Text += "Transforming..." & vbNewLine & vbNewLine

                Dim sr As New System.IO.StreamReader("C:\Users\Kbhansali\Desktop\DWM\Extracted\Flight.txt")
                Dim st As String = ""
                Dim ans As String = ""
                Dim MyArray As New ArrayList
                Dim strLine As String

                Do While sr.Peek() <> -1

                    st = sr.ReadLine()
                    st = st.Trim()
                    st = Regex.Replace(st, "\s+", " ")
                    Dim stra() As String = st.Split(" ")
                    Dim i As Integer = 0

                    If Regex.IsMatch(stra(UBound(stra)), "^[0-9]") And Regex.IsMatch(stra(0), "^[0-9]{1,4}") Then 'Handling missing values (Mealtype in this case)
                        stra(UBound(stra)) = stra(UBound(stra)) + " " + "NULL"
                    End If

                    Do While i <= UBound(stra) 'Date to uniform format

                        Dim chk As Boolean = Regex.IsMatch(stra(i), "^(\d{2})\-(\d{2})\-(\d{2})")

                        If chk = True Then
                            stra(i) = Replace(stra(i), "-", "/")
                        End If
                        i += 1

                    Loop

                    strLine = Join(stra, " ") 'Removing duplicate tuples
                    If MyArray.Contains(strLine) = False Then
                        MyArray.Add(strLine)
                        ans += strLine & vbNewLine
                    End If

                Loop

                sr.Close()
                RichTextBox1.Text += ans
                My.Computer.FileSystem.WriteAllText("C:\Users\Kbhansali\Desktop\DWM\Transformed\" & ListBox1.SelectedItem().ToString() & ".txt", ans, False)
                RichTextBox1.Text += vbNewLine & "Done transforming" & vbNewLine & vbNewLine

            End If

            If ListBox1.SelectedItem().ToString() = "Hotel" Then 'Transform hotel

                RichTextBox1.Text += "Transforming..." & vbNewLine & vbNewLine

                Dim sr As New System.IO.StreamReader("C:\Users\Kbhansali\Desktop\DWM\Extracted\Hotel.txt")
                Dim st As String = ""
                Dim ans As String = ""
                Dim MyArray As New ArrayList
                Dim strLine As String

                Do While sr.Peek() <> -1

                    st = sr.ReadLine()
                    st = st.Trim()
                    st = Regex.Replace(st, "\s+", " ")
                    Dim stra() As String = st.Split(" ")
                    Dim i As Integer = UBound(stra)

                    Do While i > UBound(stra) - 8 'Facilities to uniform format

                        If stra(i) = "0" Then
                            stra(i) = "No"
                        ElseIf stra(i) = "1" Then
                            stra(i) = "Yes"
                        End If
                        i -= 1
                    Loop

                    strLine = Join(stra, " ") 'Removing duplicate tuples
                    If MyArray.Contains(strLine) = False Then
                        MyArray.Add(strLine)
                        ans += strLine & vbNewLine
                    End If

                Loop

                sr.Close()
                RichTextBox1.Text += ans
                My.Computer.FileSystem.WriteAllText("C:\Users\Kbhansali\Desktop\DWM\Transformed\" & ListBox1.SelectedItem().ToString() & ".txt", ans, False)
                RichTextBox1.Text += vbNewLine & "Done transforming" & vbNewLine & vbNewLine

            End If

            If ListBox1.SelectedItem().ToString() = "Package" Then 'Transform package

                RichTextBox1.Text += "Transforming..." & vbNewLine & vbNewLine

                Dim sr As New System.IO.StreamReader("C:\Users\Kbhansali\Desktop\DWM\Extracted\Package.txt")
                Dim st As String = ""
                Dim ans As String = ""
                Dim MyArray As New ArrayList
                Dim strLine As String

                Do While sr.Peek() <> -1

                    st = sr.ReadLine()
                    st = st.Trim()
                    st = Regex.Replace(st, "\s+", " ")
                    Dim stra() As String = st.Split(" ")

                    If Regex.IsMatch(stra(UBound(stra)), "^[0-9]") Then 'Handling missing values (Season in this case)

                        stra(UBound(stra)) += " NULL"

                    End If

                    strLine = Join(stra, " ") 'Removing duplicate tuples
                    If MyArray.Contains(strLine) = False Then
                        MyArray.Add(strLine)
                        ans += strLine & vbNewLine
                    End If

                Loop

                sr.Close()
                RichTextBox1.Text += ans
                My.Computer.FileSystem.WriteAllText("C:\Users\Kbhansali\Desktop\DWM\Transformed\" & ListBox1.SelectedItem().ToString() & ".txt", ans, False)
                RichTextBox1.Text += vbNewLine & "Done transforming" & vbNewLine & vbNewLine

            End If

            If ListBox1.SelectedItem().ToString() = "Booking" Then 'Transform booking

                RichTextBox1.Text += "Transforming..." & vbNewLine & vbNewLine

                Dim sr As New System.IO.StreamReader("C:\Users\Kbhansali\Desktop\DWM\Extracted\Booking.txt")
                Dim st As String = ""
                Dim ans As String = ""
                Dim MyArray As New ArrayList
                Dim strLine As String

                Do While sr.Peek() <> -1

                    st = sr.ReadLine()
                    st = st.Trim()
                    st = Regex.Replace(st, "\s+", " ")
                    Dim stra() As String = st.Split(" ")
                    Dim i As Integer = 0

                    Do While i <= UBound(stra) 'Date to uniform format

                        Dim chk As Boolean = Regex.IsMatch(stra(i), "^(\d{2})\-(\d{2})\-(\d{2})")

                        If chk = True Then
                            stra(i) = Replace(stra(i), "-", "/")
                        End If
                        i += 1

                    Loop

                    strLine = Join(stra, " ") 'Removing duplicate tuples
                    If MyArray.Contains(strLine) = False Then
                        MyArray.Add(strLine)
                        ans += strLine & vbNewLine
                    End If

                Loop

                sr.Close()
                RichTextBox1.Text += ans
                My.Computer.FileSystem.WriteAllText("C:\Users\Kbhansali\Desktop\DWM\Transformed\" & ListBox1.SelectedItem().ToString() & ".txt", ans, False)
                RichTextBox1.Text += vbNewLine & "Done transforming" & vbNewLine & vbNewLine

            End If

        End If

    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click

        My.Computer.FileSystem.WriteAllText("C:\Users\Kbhansali\Desktop\DWM\Count\CustomerText.txt", "0", False)
        My.Computer.FileSystem.WriteAllText("C:\Users\Kbhansali\Desktop\DWM\Count\CompanyText.txt", "0", False)
        My.Computer.FileSystem.WriteAllText("C:\Users\Kbhansali\Desktop\DWM\Count\FlightText.txt", "0", False)
        My.Computer.FileSystem.WriteAllText("C:\Users\Kbhansali\Desktop\DWM\Count\HotelText.txt", "0", False)
        My.Computer.FileSystem.WriteAllText("C:\Users\Kbhansali\Desktop\DWM\Count\PackageText.txt", "0", False)
        My.Computer.FileSystem.WriteAllText("C:\Users\Kbhansali\Desktop\DWM\Count\BookingText.txt", "0", False)
        My.Computer.FileSystem.WriteAllText("C:\Users\Kbhansali\Desktop\DWM\Count\CustomerAccess.txt", "0", False)
        My.Computer.FileSystem.WriteAllText("C:\Users\Kbhansali\Desktop\DWM\Count\CompanyAccess.txt", "0", False)
        My.Computer.FileSystem.WriteAllText("C:\Users\Kbhansali\Desktop\DWM\Count\FlightAccess.txt", "0", False)
        My.Computer.FileSystem.WriteAllText("C:\Users\Kbhansali\Desktop\DWM\Count\HotelAccess.txt", "0", False)
        My.Computer.FileSystem.WriteAllText("C:\Users\Kbhansali\Desktop\DWM\Count\PackageAccess.txt", "0", False)
        My.Computer.FileSystem.WriteAllText("C:\Users\Kbhansali\Desktop\DWM\Count\BookingAccess.txt", "0", False)
        My.Computer.FileSystem.WriteAllText("C:\Users\Kbhansali\Desktop\DWM\Count\CustomerSQL.txt", "0", False)
        My.Computer.FileSystem.WriteAllText("C:\Users\Kbhansali\Desktop\DWM\Count\CompanySQL.txt", "0", False)
        My.Computer.FileSystem.WriteAllText("C:\Users\Kbhansali\Desktop\DWM\Count\FlightSQL.txt", "0", False)
        My.Computer.FileSystem.WriteAllText("C:\Users\Kbhansali\Desktop\DWM\Count\HotelSQL.txt", "0", False)
        My.Computer.FileSystem.WriteAllText("C:\Users\Kbhansali\Desktop\DWM\Count\PackageSQL.txt", "0", False)
        My.Computer.FileSystem.WriteAllText("C:\Users\Kbhansali\Desktop\DWM\Count\BookingSQL.txt", "0", False)

        My.Computer.FileSystem.WriteAllText("C:\Users\Kbhansali\Desktop\DWM\Extracted\Customer.txt", "", False)
        My.Computer.FileSystem.WriteAllText("C:\Users\Kbhansali\Desktop\DWM\Extracted\Company.txt", "", False)
        My.Computer.FileSystem.WriteAllText("C:\Users\Kbhansali\Desktop\DWM\Extracted\Flight.txt", "", False)
        My.Computer.FileSystem.WriteAllText("C:\Users\Kbhansali\Desktop\DWM\Extracted\Hotel.txt", "", False)
        My.Computer.FileSystem.WriteAllText("C:\Users\Kbhansali\Desktop\DWM\Extracted\Package.txt", "", False)
        My.Computer.FileSystem.WriteAllText("C:\Users\Kbhansali\Desktop\DWM\Extracted\Booking.txt", "", False)

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click

        If (ListBox1.SelectedIndex < 0) Then
            MessageBox.Show("Select dimension")
        Else

            Dim sr As New System.IO.StreamReader("C:\Users\Kbhansali\Desktop\DWM\Transformed\" & ListBox1.SelectedItem().ToString() & ".txt")
            Dim con As New OleDb.OleDbConnection
            Dim dbProvider As String
            Dim dbSource As String

            dbProvider = "PROVIDER=Microsoft.ACE.OLEDB.12.0;"
            dbSource = "Data Source = C:\Users\Kbhansali\Desktop\DWM\Loaded\" & ListBox1.SelectedItem().ToString() & ".accdb"
            con.ConnectionString = dbProvider & dbSource
            con.Open()

            Dim tblName As String = ListBox1.SelectedItem().ToString()
            Dim restrictions(3) As String
            restrictions(2) = tblName
            Dim dbTbl As DataTable = con.GetSchema("Tables", restrictions)

            Dim sqlstr As String
            Dim command As OleDbCommand
            RichTextBox1.Text += "Loading..." & vbNewLine & vbNewLine

            If ListBox1.SelectedItem().ToString() = "Customer" Then 'Increment load customer

                If dbTbl.Rows.Count = 0 Then 'Table does not exist

                    sqlstr = "CREATE TABLE Customer(CUSTID Numeric(10,0), FNAME Varchar(255), LNAME Varchar(255), PASSPORTNO Numeric(10,0), GENDER Varchar(255), INCOME Numeric(10,0), CONTACT Varchar(255), SPENT Numeric(10,0), DOB Varchar(255), AGE Numeric(10,0))"
                    command = New OleDbCommand(sqlstr, con)
                    Try
                        command.ExecuteNonQuery()
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                End If

                Dim stre() As String
                While sr.Peek() <> -1
                    stre = sr.ReadLine.Split(" ")
                    sqlstr = "INSERT INTO Customer(CUSTID, FNAME, LNAME, PASSPORTNO, GENDER, INCOME, CONTACT, SPENT, DOB, AGE) VALUES('" & stre(0) & "','" & stre(1) & "','" & stre(2) & "','" & stre(3) & "','" & stre(4) & "','" & stre(5) & "','" & stre(6) & "','" & stre(7) & "','" & stre(8) & "','" & stre(9) & "')"
                    command = New OleDbCommand(sqlstr, con)
                    Try
                        command.ExecuteNonQuery()
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                End While

            End If

            If ListBox1.SelectedItem().ToString() = "Company" Then 'Increment load company

                If dbTbl.Rows.Count = 0 Then 'Table does not exist

                    sqlstr = "CREATE TABLE Company (CID Numeric(10,0), LOCATION Varchar(255), CONTACT Varchar(255), MFNAME Varchar(255), MLNAME Varchar(255), SALES Numeric(10,0), EMPLOYEES Numeric(10,0) )"
                    command = New OleDbCommand(sqlstr, con)
                    Try
                        command.ExecuteNonQuery()
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                End If

                Dim stre() As String
                While sr.Peek() <> -1
                    stre = sr.ReadLine.Split(" ")

                    Dim ans As String = ""
                    Dim x As Integer

                    For x = 1 To UBound(stre)
                        Dim chk As Boolean = Regex.IsMatch(stre(x), "^[0-9]")
                        If chk = True Then
                            Exit For
                        End If
                        ans += stre(x) & " "
                    Next

                    sqlstr = "INSERT INTO Company(CID,LOCATION,CONTACT,MFNAME,MLNAME,SALES,EMPLOYEES) VALUES('" & stre(0) & "','" & ans & "','" & stre(UBound(stre) - 4) & "','" & stre(UBound(stre) - 3) & "','" & stre(UBound(stre) - 2) & "','" & stre(UBound(stre) - 1) & "','" & stre(UBound(stre)) & "')"
                    command = New OleDbCommand(sqlstr, con)
                    Try
                        command.ExecuteNonQuery()
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                End While

            End If

            If ListBox1.SelectedItem().ToString() = "Package" Then 'Increment load package

                If dbTbl.Rows.Count = 0 Then 'Table does not exist

                    sqlstr = "CREATE TABLE Package ( PID Numeric(10,0), PRICE Numeric(10,0), PLACE Varchar(255), DAYS Varchar(255), SEASON Varchar(255))"
                    command = New OleDbCommand(sqlstr, con)
                    Try
                        command.ExecuteNonQuery()
                    Catch ex As Exception
                        MessageBox.Show("ERROR")
                    End Try
                End If

                Dim stre() As String
                Dim x As Integer

                While sr.Peek() <> -1
                    Dim ans As String = ""
                    stre = sr.ReadLine.Split(" ")

                    For x = 2 To (UBound(stre) - 2)
                        ans += stre(x) & " "
                    Next

                    sqlstr = "INSERT INTO Package (PID,PRICE,PLACE,DAYS,SEASON) VALUES('" & stre(0) & "','" & stre(1) & "','" & ans & "','" & stre(UBound(stre) - 1) & "','" & stre(UBound(stre)) & "')"
                    command = New OleDbCommand(sqlstr, con)
                    Try
                        command.ExecuteNonQuery()
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                End While

            End If

            If ListBox1.SelectedItem().ToString() = "Hotel" Then 'Increment load hotel

                If dbTbl.Rows.Count = 0 Then 'Table does not exist

                    sqlstr = "CREATE TABLE Hotel ( HID Numeric(10,0), NAME Varchar(255), RATING Varchar(255), DISCO Varchar(255), CASINO Varchar(255), SPA Varchar(255), SWIMMING Varchar(255), GYM Varchar(255), BAR Varchar(255), WIFI Varchar(255), RESTAURANT Varchar(255))"
                    command = New OleDbCommand(sqlstr, con)
                    Try
                        command.ExecuteNonQuery()
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                End If

                Dim stre() As String
                Dim x As Integer

                While sr.Peek() <> -1
                    Dim ans As String = ""
                    stre = sr.ReadLine.Split(" ")

                    For x = 1 To (UBound(stre) - 9)
                        ans += stre(x) & " "
                    Next

                    sqlstr = "INSERT INTO Hotel (HID, NAME, RATING, DISCO, CASINO, SPA, SWIMMING, GYM, BAR, WIFI, RESTAURANT) VALUES('" & stre(0) & "','" & ans & "','" & stre(UBound(stre) - 8) & "','" & stre(UBound(stre) - 7) & "','" & stre(UBound(stre) - 6) & "','" & stre(UBound(stre) - 5) & "','" & stre(UBound(stre) - 4) & "','" & stre(UBound(stre) - 3) & "','" & stre(UBound(stre) - 2) & "','" & stre(UBound(stre) - 1) & "','" & stre(UBound(stre)) & "')"
                    command = New OleDbCommand(sqlstr, con)
                    Try
                        command.ExecuteNonQuery()
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                End While

            End If

            If ListBox1.SelectedItem().ToString() = "Flight" Then 'Increment load flight

                If dbTbl.Rows.Count = 0 Then 'Table does not exist

                    sqlstr = "CREATE TABLE Flight ( FID Numeric(10,0), SOURCE Varchar(255), DEPTDATE Date, DEPTTIME Numeric(10,0), DESTINATION Varchar(255), ARRIVALDATE Date, ARRIVALTIME Numeric(10,0), AIRLINE Varchar(255), RATING Numeric(10,0), MEALTYPE Varchar(255))"
                    command = New OleDbCommand(sqlstr, con)
                    Try
                        command.ExecuteNonQuery()
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                End If

                Dim stre() As String
                Dim x As Integer

                While sr.Peek() <> -1
                    Dim ans As String = ""
                    Dim ans1 As String = ""
                    stre = sr.ReadLine.Split(" ")

                    Dim i As Integer = 0
                    Dim index As Integer = 0
                    Do While i <= UBound(stre)

                        Dim chk As Boolean = Regex.IsMatch(stre(i), "^(\d{2})\/(\d{2})\/(\d{2})")

                        If chk = True Then
                            index = i
                            Exit Do
                        End If
                        i += 1

                    Loop

                    For x = 1 To (index - 1)
                        ans += stre(x) & " "
                    Next

                    For x = (index + 2) To ((UBound(stre) - 5))
                        ans1 += stre(x) & " "
                    Next

                    sqlstr = "INSERT INTO Flight (FID, SOURCE, DEPTDATE, DEPTTIME, DESTINATION, ARRIVALDATE, ARRIVALTIME, AIRLINE, RATING, MEALTYPE) VALUES('" & stre(0) & "','" & ans & "','" & stre(index) & "','" & stre(index + 1) & "','" & ans1 & "','" & stre(UBound(stre) - 4) & "','" & stre(UBound(stre) - 3) & "','" & stre(UBound(stre) - 2) & "','" & stre(UBound(stre) - 1) & "','" & stre(UBound(stre)) & "')"
                    command = New OleDbCommand(sqlstr, con)
                    Try
                        command.ExecuteNonQuery()
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                End While

            End If

            If ListBox1.SelectedItem().ToString() = "Booking" Then 'Increment load booking

                If dbTbl.Rows.Count = 0 Then 'Table does not exist

                    sqlstr = "CREATE TABLE Booking (BID Numeric(10,0), CID Numeric(10,0), CUSTID Numeric(10,0), FID Numeric(10,0), HID Numeric(10,0), PID Numeric(10,0), BDATE date, PEOPLE Numeric(10,0), PRICE Numeric(10,0))"
                    command = New OleDbCommand(sqlstr, con)
                    Try
                        command.ExecuteNonQuery()
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                End If

                Dim stre() As String

                While sr.Peek() <> -1
                    Dim ans As String = ""
                    Dim ans1 As String = ""
                    stre = sr.ReadLine.Split(" ")
                    sqlstr = "INSERT INTO Booking (BID, CID, CUSTID, FID, HID, PID, BDATE, PEOPLE, PRICE) VALUES('" & stre(0) & "','" & stre(1) & "','" & stre(2) & "','" & stre(3) & "','" & stre(4) & "','" & stre(5) & "','" & stre(6) & "','" & stre(7) & "','" & stre(8) & "')"
                    command = New OleDbCommand(sqlstr, con)
                    Try
                        command.ExecuteNonQuery()
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                End While

            End If

            sr.Close()
            RichTextBox1.Text += vbNewLine & "Done loading" & vbNewLine & vbNewLine
            dbTbl.Dispose()
            con.Close()

        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click 'Full refresh

        If (ListBox1.SelectedIndex < 0) Then
            MessageBox.Show("Select dimension")
        Else

            Dim con As New OleDb.OleDbConnection
            Dim dbProvider As String
            Dim dbSource As String

            dbProvider = "PROVIDER=Microsoft.ACE.OLEDB.12.0;"
            dbSource = "Data Source = C:\Users\Kbhansali\Desktop\DWM\Loaded\" & ListBox1.SelectedItem().ToString() & ".accdb"
            con.ConnectionString = dbProvider & dbSource
            con.Open()

            Dim sqlstr As String
            Dim command As OleDbCommand

            Dim tblName As String = ListBox1.SelectedItem().ToString()
            Dim restrictions(3) As String
            restrictions(2) = tblName
            Dim dbTbl As DataTable = con.GetSchema("Tables", restrictions)

            If dbTbl.Rows.Count = 0 Then 'Table does not exist

                con.Close()
                Button8_Click(sender, New System.EventArgs())

            Else

                sqlstr = "DROP TABLE " & ListBox1.SelectedItem().ToString()
                command = New OleDbCommand(sqlstr, con)
                Try
                    command.ExecuteNonQuery()
                Catch ex As Exception
                    MessageBox.Show("Error full refreshing")
                End Try
                con.Close()
                Button8_Click(sender, New System.EventArgs())

            End If
        End If
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click

        Display.StartPosition = FormStartPosition.Manual
        Display.Location = Me.Location
        Display.Show()
        Me.Close()

    End Sub
End Class