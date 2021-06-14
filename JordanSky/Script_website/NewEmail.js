function btnSave() {
    if ($("#txtEmail").val() == "") {
        if ($("#txtEmail").val() == "") {
            $("#lblEmail").text("الرجاء ادخال نص الرسالة");
            $("#lblEmail").show();
        }
        else {
            $("#lblEmail").hide();
        }
    }
        else {
            debugger
            var obj = {};
            obj.Name = $("#txtname").val();
            obj.Phone = $("#txtNumber").val();
            obj.Subject = $("#txtsubject").val();
            obj.Msg = $("#txtEmail").val();
            debugger
            $.ajax({
                type: "Post",
                url: "/Messages/Send",
                data: obj,
                dataType: "json",
                success: function (data) {
                    debugger
                    if (data == true) {
                        $("#alertMsg").show();
                        $("#txtname").val('');
                        $("#txtNumber").val('');
                        $("#txtsubject").val('');
                        $("#txtEmail").val('');

                    }
                    else {
                        alert("Error");


                    }

                },
            });
        }
}
