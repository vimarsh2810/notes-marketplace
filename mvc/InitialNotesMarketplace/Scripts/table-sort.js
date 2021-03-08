// Sortable columns 

$(function () {
    $('.data-table').tablesorter();
})

$(document).ready(function () {
    $("#published-btn").click(function () {
        var value = $('#published-search').val().toLowerCase();
        $("tbody#published-tbody tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
});

$(document).ready(function () {
    $("#in-progress-btn").click(function () {
        var value = $('#in-progress-search').val().toLowerCase();
        $("tbody#in-progress-tbody tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
});

$(document).ready(function () {
    $(".common-btn").click(function (event) {
        //event.preventDefault();
        console.log("ok")
        var value = $('.common-search').val().toLowerCase();
        $("tbody.common-tbody tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
});