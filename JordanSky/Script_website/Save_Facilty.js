function btnSave(id,Status) {
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
        obj.Id = id ;
    }
    obj.Name = $("#txtName").val();
    obj.Price = $("#txtPrice").val();
    obj.Number = xy;
    obj.Location = $("#txtLocation").val();
    obj.City_id = $("#ddlCity").val();
    obj.Description = $("#txtDesc").val();
    obj.Max_people = $("#txtPeople").val();
    obj.No_bedroom = $("#txtNoBedRoom").val();
    obj.Space = $("#txtSpace").val();
    obj.No_bed = $("#txtBed").val();
    obj.internet = $("#ddlIntrnet").val();
    obj.pool = $("#ddlpool").val();
    obj.Zarb = $("#ddlZarb").val();
    obj.grill = $("#ddlGrill").val();
    obj.pool_heating = $("#ddlHeating").val();
    obj.AC = $("#ddlAC").val();
    obj.Children = $("#ddlChildren").val();
    obj.View = $("#ddlView").val();
    obj.Parking = $("#ddlParking").val();
    obj.Playground = $("#ddlPlayground").val();
    obj.Ref_No = $("#txtRef").val();
    obj.Type_Product_id = $("#ddlType").val();
    debugger
    $.ajax({
        type: "Post",
        url: "/Facilty/SaveMazra3a",
        data: obj,
        dataType: "json",
        success: function (data) {
            debugger
            if (data == true) {
                window.location.href = "/Facilty/Home";
            }
            else {
                $("#lblError").show();
                
            }
        },
    });
}

