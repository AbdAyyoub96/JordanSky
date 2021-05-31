function btnSave() {
    debugger
    var obj = {};
    obj.Type = $("#ddlType").val();
    obj.Car = $("#ddlTran").val();
    obj.Food = $("#ddlRestu").val();
    obj.Hotel = $("#ddlHotel").val();
    debugger
    $.ajax({
        type: "Post",
        url: "/Internal_Package/Filter",
        data: obj,
        dataType: "json",
        success: function (data) {
            debugger
            if (data == true) {
                window.location.href = "/Internal_Package/Filter";
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