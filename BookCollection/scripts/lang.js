function SubForm(id) {
    $.ajax({
        url: '/BookCollection/ToggleBookOwned/' + id,
        type: 'post',
        //data: $('#myForm').serialize(),
        success: function () {
            console.log("worked");
        }
    });
}