function btnSave() {
    debugger
    var obj = {};
    obj.FierstNum = $("#txtFierstNum").val();
    obj.SecondNum = $("#txtSecondNum").val();
    obj.City_value = $("#ddlCity").val();
    obj.rating_Value = $("#ddlrating").val();
    obj.Category_Value = $("#ddlCategory").val();
    debugger
    $.ajax({
        type: "Post",
        url: "/Hotels/Filter",
        data: obj,
        dataType: "json",
        success: function (data) {
            debugger
            if (data == true) {
                window.location.href = "/Hotels/Filter";
            }
            else {
                $("#lblError").show();
            }
        },
        error: function () {
            alert("Error occured!!")
        }
    });
}