Imports System
Imports System.Threading
Imports System.IO.Ports
Imports System.ComponentModel

Public Class Frm1
    'Coded By Anurag Goel
    Dim myPort As Array
    Delegate Sub SetTextCallBack(ByVal [text] As String)
    Private Sub ExitToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem2.Click
        End
    End Sub

    Private Sub Frm1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Button2.Enabled = False
        myPort = IO.Ports.SerialPort.GetPortNames()
        ComboBox2.Items.AddRange(myPort)
        TabControl1.TabPages(1).Enabled = False
        TabControl1.TabPages(2).Enabled = False
        TabControl1.TabPages(3).Enabled = False

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If Button1.Text = "Start" Then
                SerialPort1.PortName = ComboBox2.Text
                SerialPort1.BaudRate = ComboBox3.Text
                SerialPort1.Open()
                Button1.Text = "Stop"
                Button2.Enabled = True
            ElseIf Button1.Text = "Stop" Then
                SerialPort1.Close()
                Button1.Text = "Start"
                Button2.Enabled = False
            End If
        Catch ex As ArgumentException
            MessageBox.Show(ex.ToString)

        End Try



    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        SerialPort1.Write(TextBox1.Text.Trim & vbCr)
    End Sub

    Private Sub ReceivedText(ByVal [text] As String)
        If Me.RichTextBox1.InvokeRequired Then
            Dim x As New SetTextCallBack(AddressOf ReceivedText)
            Me.Invoke(x, New Object() {(text)})
        Else
            Me.RichTextBox1.Text &= [text]
        End If
    End Sub

    Private Sub serialPort1_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
        ReceivedText(SerialPort1.ReadExisting())

    End Sub



    Private Sub AboutToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        Form2.ShowDialog()
    End Sub
End Class

