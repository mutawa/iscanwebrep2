Partial Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.GridView2.DataSource = getTable("select count(*) from users")
        Me.GridView2.DataBind()

    End Sub

End Class