﻿Imports System.Data.OleDb

Public Class DelFood

    Dim conn As New OleDbConnection
    Dim cmd As OleDbCommand
    Dim dt As New DataTable
    Dim da As New OleDbDataAdapter(cmd)
    Private Sub DelFood_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn.ConnectionString = "Provider=Microsoft.Jet.Oledb.4.0; data source =D:\komeda_data.mdb"
        viewer()

        Button1.BackgroundImage = My.Resources.ResourceManager.GetObject("刪除")
        Button1.BackColor = Color.Transparent
        Button1.BackgroundImageLayout = ImageLayout.Stretch
        Button1.FlatStyle = FlatStyle.Flat
        Button1.FlatAppearance.BorderSize = 0

        Button2.BackgroundImage = My.Resources.ResourceManager.GetObject("返回")
        Button2.BackColor = Color.Transparent
        Button2.BackgroundImageLayout = ImageLayout.Stretch
        Button2.FlatStyle = FlatStyle.Flat
        Button2.FlatAppearance.BorderSize = 0
    End Sub
    Private Sub viewer()
        DataGridView1.DataSource = Nothing
        DataGridView1.Refresh()

        conn.Open()
        cmd = conn.CreateCommand()
        cmd.CommandType = CommandType.Text
        da = New OleDbDataAdapter("select * from 餐點 ", conn)
        da.Fill(dt)
        DataGridView1.DataSource = dt
        conn.Close()
        DataGridView1.Columns(0).Width = 80
        DataGridView1.Columns(1).Width = 80
        DataGridView1.Columns(2).Width = 80

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If String.IsNullOrWhiteSpace(FoodName.Text) Then
                MessageBox.Show("請將資料輸入完整")
                Exit Sub
            End If
            conn.Open()
            cmd = conn.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "delete * from 餐點 where 品名 = '" + FoodName.Text + "' OR 餐點編號='" + TextBox1.Text + "'"
            cmd.ExecuteNonQuery()

            dt = New DataTable()
            da = New OleDbDataAdapter("select * from 餐點 ", conn)
            da.Fill(dt)
            DataGridView1.DataSource = dt
            conn.Close()
            MessageBox.Show("已成功刪除")
            'viewer()
            FoodName.Text = ""
        Catch ex As Exception
            MessageBox.Show("所需資料請輸入完整")
        End Try
        FoodName.Text = ""
        TextBox1.Text = ""
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Try
            TextBox1.Text = DataGridView1.SelectedRows(0).Cells(0).Value.ToString()
            FoodName.Text = DataGridView1.SelectedRows(0).Cells(1).Value.ToString()
        Catch ex As Exception
            MessageBox.Show("請點擊品名前方那列")
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim fmm As New MenuManage()
        fmm.Show()
        Me.Hide()
    End Sub
End Class