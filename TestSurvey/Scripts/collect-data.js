$(document).ready(function () {
    $("#save").bind("click", function () {
        var answers = $(".textanswer"), jsonData = [];

        for (var i = 0; i < answers.length; i++) {
            jsonData.push({
                Id: answers[i].name,
                TypedAnswer: answers[i].value
            });
        }

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
                alert("There was an error posting the data to the server: " + error.responseText);
            }
        });
        console.log(answers.length);
    });
});