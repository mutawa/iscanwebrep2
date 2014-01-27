Partial Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Refresh_Grid()


    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        Execute_SQL("insert into users (username,password) values ('pom','do')")
        refresh_grid()
    End Sub
    Sub Refresh_Grid()
        Me.GridView1.DataSource = getTable("select count(*) from users")
        Me.GridView1.DataBind()

    End Sub
End Class