function collectData() {
    var jsonData = [],
        answers = $(".text-answer"),
        radios = $(".radio-answer"),
        checkboxes = $(".checkbox-answer");

    for (var i = 0; i < answers.length; i++) {
        jsonData.push({
            Question: answers[i].attributes.questionid.value,
            TextValue: answers[i].value
        });
    }

    for (var r = 0; r < radios.length; r++) {
        if (radios[r].checked) {
            jsonData.push({
                Question: radios[r].attributes.questionId.value,
                TextValue: radios[r].closest('label').innerText,
                Answer: radios[r].attributes.answerId.value
            });
        } else {
            jsonData.push({
                Question: radios[r].attributes.questionId.value,
                TextValue: radios[r].closest('label').innerText,
                Answer: radios[r].attributes.answerId.value
            });
        }
    }

    for (var c = 0; c < checkboxes.length; c++) {
        if (checkboxes[c].checked) {
            jsonData.push({
                Question: checkboxes[c].attributes.questionId.value,
                TextValue: checkboxes[c].closest('label').innerText,
                Answer: radios[r].attributes.answerId.value
            });
        } else {
            jsonData.push({
                Question: checkboxes[c].attributes.questionId.value,
                TextValue: checkboxes[c].closest('label').innerText,
                Answer: radios[r].attributes.answerId.value
            });
        }
    }

    if (jsonData.length > 0) {
        $.ajax({
            type: "POST",
            url: "/Survey/SaveData",
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
}
