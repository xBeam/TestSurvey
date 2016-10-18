$(document).ready(function () {
    $("#save").bind("click", function () {
        var answers = $(".text-answer"), jsonData = [];

        for (var i = 0; i < answers.length; i++) {
            jsonData.push({
                Id: answers[i].name,
                TypedAnswer: answers[i].value
            });
        }

        var radios = $(".radio-answer");

        for (var r = 0; r < radios.length; r++) {
            if (radios[r].checked) {
                jsonData.push({
                    Id: radios[r].name,
                    TypedAnswer: radios[r].closest('label').innerText
                });
            }
        }

        var checkboxes = $(".checkbox-answer");

        for (var c = 0; c < checkboxes.length; c++) {
            if (checkboxes[c].checked) {
                jsonData.push({
                    Id: checkboxes[c].value,
                    TypedAnswer: checkboxes[c].closest('label').innerText
                });
            }
        }

        if (jsonData.length > 0) {
            $.ajax({
                type: "POST",
                url: "/survey/create",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(jsonData),
                success: function (result) {
                    console.log(result); //log to the console to see whether it worked
                },
                error: function (error) {
                    console.log("There was an error posting the data to the server: " + error.responseText);
                }
            });
        }
    });
});