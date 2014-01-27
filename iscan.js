$(document).ready(function(){
    refresh_count();
    
    $('.btn').click(function(){
        $.ajax({
        type: "POST",
        url: "Data.aspx/Increase_Users",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            //alert(data.d);
            refresh_count();

        }
    });
        return false;
    });
});

function refresh_count()
{
    
        $.ajax({
            type: "POST",
            url: "Data.aspx/Get_Count",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                //alert(data.d);
                $('#count').html(data.d);

            }
        });
    
}