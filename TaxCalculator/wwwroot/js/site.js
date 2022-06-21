$('#calculate-tax').submit(function (e) {
    e.preventDefault();
    var message = document.getElementById("result");
    const incomeValue = document.getElementById("income-value");
    let pattern = /[0-9]/
    if(!pattern.test(incomeValue.value)){
        message.innerHTML= "Income value must be number!";
    }
    else{
        $.ajax({
            url: '/Home/CalculateTax',
            type: 'post',
            data: $('#calculate-tax').serialize(),
            success: function (response) {
                message.innerHTML = response;
            }
        });
    }
    

});