$(document).ready(function () {
    $("#save").bind("click", function () {
        var answers = $(".text-answer"), 
            jsonData = [], 
            radios = $(".radio-answer"), 
            checkboxes = $(".checkbox-answer");

        for (var i = 0; i < answers.length; i++) {
            jsonData.push({
                Id: answers[i].attributes.questionid.value,
                Text: answers[i].value
            });
        }

        for (var r = 0; r < radios.length; r++) {
            if (radios[r].checked) {
                jsonData.push({
                    Id: radios[r].attributes.questionId.value,
                    Text: radios[r].closest('label').innerText,
                    IsChecked: true
                });
            } else {
                jsonData.push({
                    Id: radios[r].attributes.questionId.value,
                    Text: radios[r].closest('label').innerText,
                    IsChecked: false
                });
            }
        }

        for (var c = 0; c < checkboxes.length; c++) {
            if (checkboxes[c].checked) {
                jsonData.push({
                    Id: checkboxes[c].attributes.questionId.value,
                    Text: checkboxes[c].closest('label').innerText,
                    IsChecked: true
                });
            } else {
                jsonData.push({
                    Id: checkboxes[c].attributes.questionId.value,
                    Text: checkboxes[c].closest('label').innerText,
                    IsChecked: false
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
                    console.log(result); 
                },
                error: function (error) {
                    console.log("There was an error posting the data to the server: " + error.responseText);
                }
            });
        }
    });
});