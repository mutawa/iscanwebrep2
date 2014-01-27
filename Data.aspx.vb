Imports System.Web.Services

Partial Public Class Data
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    <WebMethod()> Public Shared Function Increase_Users() As String
        Execute_SQL("insert into users (username,password) select username,password from users")
        Dim reply As String = getTable("select count(*) from users").Rows(0).Item(0).ToString
        Return reply

    End Function

    <WebMethod()> Public Shared Function Add_One() As String
        Execute_SQL("insert into users (username,password) values ('aa','bb')")
        Dim reply As String = getTable("select count(*) from users").Rows(0).Item(0).ToString
        Return reply

    End Function

    <WebMethod()> Public Shared Function Get_Count() As String

        Dim reply As String = getTable("select count(*) from users").Rows(0).Item(0).ToString
        Return reply

    End Function

    <WebMethod()> Public Shared Function Delete_Users() As String

        Execute_SQL("delete from users")
        Return "done"


    End Function
End Class