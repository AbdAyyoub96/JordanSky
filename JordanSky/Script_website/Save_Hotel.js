function btnSave(id, Status) {
    debugger
    var checkBoxArray = [];
    $("#check").each(function () {

        checkBoxArray.push($(this).val());;
    });
    var xy = "";
    for (var i = 0; i < checkBoxArray.length; i++) {
        if (i == 0) {
            xy = xy + checkBoxArray[i];
        }
        else {
            xy = xy + "," + checkBoxArray[i];
        }
    }
    debugger

    var obj = {};
    if (Status == "update") {
        obj.Id = id;
    }
    obj.Name = $("#txtName").val();
    obj.Price = $("#txtPrice").val();
    obj.Number = xy;
    obj.Maps = $("#txtLocation").val();
    obj.City_id = $("#ddlCity").val();
    obj.Description = $("#txtDesc").val();
    obj.Ref_No = $("#txtRef").val();
    obj.Type_Product_id = $("#ddlType").val();
    debugger
    $.ajax({
        type: "Post",
        url: "/Hotels/New_Hotel",
        data: obj,
        dataType: "json",
        success: function (data) {
            debugger
            if (data == true) {
                window.location.href = "/Hotels/All_hotel";
            }
            else {
                $("#lblError").show();

            }
        },
    });
}

