Imports System.Data.SqlClient
Imports System.Drawing

Module mdlSQL
    Public strConn As String = "Server=e5e31086-95a1-4964-ac83-a2be007da8ed.sqlserver.sequelizer.com;User Id=sjgdijabyjkoznrk;Password=5jRAfAKVVcx4n7Q6qdgwfsjb5gpykTXsnK4ShTeqTAtr8fXaNztZyssK8QE7mkKc;"

    Function Execute_SQL(ByVal SQL As String, Optional ByRef err_msg As String = "") As Boolean
        Dim retval As Boolean = True
        Dim sqlCmd As SqlCommand

        Dim sqlConn As SqlConnection = New SqlConnection(strConn)

        sqlCmd = New SqlCommand(SQL, sqlConn)

        Try
            sqlConn.Open()
            sqlCmd.ExecuteNonQuery()

        Catch ex As Exception
            retval = False
            err_msg = ex.Message
        Finally
            sqlConn.Close()
            sqlCmd.Dispose()
        End Try

        Return retval

    End Function
    Function getTable(ByVal SQL As String, Optional ByRef msg As String = "") As DataTable
        Dim retval As Boolean = True
        Dim dt As DataTable = New DataTable
        Dim sqlCmd As SqlCommand
        Dim sqlDapter As SqlDataAdapter = New SqlDataAdapter
        Dim sqlConn As SqlConnection = New SqlConnection(strConn)
        sqlCmd = New SqlCommand(SQL, sqlConn)
        sqlDapter.SelectCommand = sqlCmd

        Try
            sqlDapter.Fill(dt)
        Catch ex As Exception
            msg = ex.Message
            retval = Nothing
        Finally
            sqlConn.Close()
            sqlDapter.Dispose()
            sqlCmd.Dispose()
            sqlCmd.Dispose()
        End Try
        Return dt
    End Function
    Function Log(ByVal STR As String) As Boolean

        Return True 'Execute_SQL("insert into logger(text) values('" & STR.Replace("'", "~").Replace("""", "$") & "')")

    End Function

    Function SaveIdea(ByVal user As String, ByRef idea_id As String, ByRef errmsg As String) As Boolean
        Dim sqlConnection As New SqlConnection(strConn)

        Try

            Dim command As SqlCommand = New SqlCommand("InsertIdea", sqlConnection)

            command.CommandType = CommandType.StoredProcedure


            command.Parameters.Add("@user", SqlDbType.VarChar).Value = user
            command.Parameters.Add("@iid", SqlDbType.Int)

            command.Parameters("@iid").Direction = ParameterDirection.Output



            'command.Parameters.Add("@iid", SqlDbType.Int).Direction = ParameterDirection.Output


            sqlConnection.Open()
            command.ExecuteNonQuery()
            idea_id = command.Parameters("@iid").Value
            sqlConnection.Close()



            Return True

        Catch ex As Exception
            errmsg = ex.Message
            Return False
        Finally
            sqlConnection.Close()
        End Try

    End Function

    Function SaveComment(ByVal user As String, ByVal idea_id As Integer, ByVal comment_text As String, Optional ByRef msg As String = "") As Boolean
        Dim sqlConnection As New SqlConnection(strConn)

        Try

            Dim command As SqlCommand = New SqlCommand("InsertComment", sqlConnection)

            command.CommandType = CommandType.StoredProcedure


            command.Parameters.Add("@user", SqlDbType.VarChar).Value = user
            command.Parameters.Add("@idea_id", SqlDbType.Int).Value = idea_id
            command.Parameters.Add("@comment_text", SqlDbType.NVarChar).Value = comment_text
            command.Parameters.Add("@idea_status", SqlDbType.Int).Value = Val(getTable("select status from ideas where id=" & idea_id & " ").Rows(0).Item(0).ToString)



            sqlConnection.Open()
            command.ExecuteNonQuery()

            sqlConnection.Close()
            Return True

        Catch ex As Exception
            msg = ex.Message
            Return False
        Finally
            sqlConnection.Close()
        End Try

    End Function

    Function saveDraft(ByVal title As String, ByVal body As String, ByVal benifit As String, ByVal user As String) As Boolean
        Dim sqlConnection As New SqlConnection(strConn)

        Try

            Dim command As SqlCommand = New SqlCommand("InsertDraft", sqlConnection)

            command.CommandType = CommandType.StoredProcedure

            command.Parameters.Add("@title", SqlDbType.NVarChar).Value = title
            command.Parameters.Add("@benifit", SqlDbType.NVarChar).Value = benifit

            command.Parameters.Add("@text", SqlDbType.NVarChar).Value = body
            command.Parameters.Add("@user", SqlDbType.VarChar).Value = user


            sqlConnection.Open()
            command.ExecuteNonQuery()

            sqlConnection.Close()
            Return True

        Catch ex As Exception
            Return False
        Finally
            sqlConnection.Close()
        End Try

    End Function

    Function Load_Attachments(ByVal idea_id As String) As String
        Dim ret_value As String = ""

        Dim dt As DataTable = getTable("select docname,doctype,docuid from Docs where idea_id=" & idea_id & "")
        If dt.Rows.Count > 0 Then
            ret_value = "<div class='attachment_box'><table>"
            For i = 0 To dt.Rows.Count - 1
                ret_value &= "<tr id='" & dt.Rows(i).Item(2).ToString & "'  style='cursor:pointer;' onclick=""window.location='getfile.aspx?docuid=" & dt.Rows(i).Item(2).ToString & "'"">"
                ret_value &= "<td width=1  ><img src='images/docicon.png' /></td>"

                ret_value &= "<td>" & dt.Rows(i).Item(0).ToString & "</td>"


                ret_value &= "</tr>"
            Next
            ret_value &= "</table></div>"
        End If


        dt.Dispose()

        Return ret_value

    End Function
    Function Table_To_HTML(ByRef dt As DataTable) As String
        Dim reply As String = ""
        reply &= "<html>"
        reply &= " <meta charset='utf-8'>"

        reply &= " <meta http-equiv='Content-Type' content='text/html; charset=utf-8'> "

        reply &= "<table cellspacing='0' cellpadding='5'>"

        reply &= "<tr>"
        For i = 0 To dt.Columns.Count - 1
            reply &= "<td style='border:2px solid black;color:red;'><strong>" + dt.Columns(i).ColumnName + "</strong></td>"

        Next

        reply &= "</tr>"
        For i = 0 To dt.Rows.Count - 1
            reply &= "<tr>"
            Dim row As DataRow = dt.Rows(i)
            For j = 0 To dt.Columns.Count - 1
                Dim cont As String = row.Item(j).ToString
                If cont.Contains("ID-0") Then
                    Dim id As Integer = Val(cont.Substring(3))
                    cont = "<a href='http://hd-jhd-ims-1.sabiccorp.sabic.com/View.aspx?id=" & id.ToString & "'>" + cont + "</a>"
                End If
                reply &= "<td style='border:1px solid black;'>" + cont + "</td>"
            Next
            reply &= "</tr>"
        Next

        reply &= "</table>"
        reply &= "</html>"
        Return reply

    End Function
End Module
