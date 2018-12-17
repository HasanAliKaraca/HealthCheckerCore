$(document).ready(function () {

    function deleteRecord(id) {
        var res = confirm("Selected data will be deleted. Are u sure?");

        if (res) {

            $.post("/home/delete/" + id, function myfunction() {
                alert("Success!");

                location.reload();
            });

        }
    }

    $(".btnToDel").click(function () {
        var id = $(this).data("id");
        deleteRecord(id);
    });

});