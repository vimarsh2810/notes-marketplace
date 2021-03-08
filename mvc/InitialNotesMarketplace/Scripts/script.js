
//  Toggle Password

$(".toggle-password").click(function () {
    var input = $($(this).attr("toggle"));
    if (input.attr("type") == "password") {
        input.attr("type", "text");
    } else {
        input.attr("type", "password");
    }
});


// FAQ
$('.question').click(function() {
  $('.question h4 img').attr('src', 'images/FAQ/minus.png')
})



// ======== FAQ ================

$(document).ready(function () {

  $(".card-header").click(function() {
    $(this).toggleClass('toggle-class');

    if($(this).find('img').attr('src') == 'images/minus.png') {
      $(this).find('img').attr('src', 'images/plus.png');
    }
    else {
      $(this).find('img').attr('src', 'images/minus.png');
    }

    $(this).parent().siblings('.card').find('.card-header').removeClass('toggle-class');
  })
});

// ====== Search Note ==========

$(document).ready(function () {
    searchResult();

    $(".search-note-filter").on("click", () => {
        searchResult();
    });
    $("#search-note-input").on("click", () => {
        searchResult();
    });
});

function searchResult() {
    
    $.ajax({
        url: "/RegisteredUser/FilterPartialView",
        dataType: "html",
        method: "GET",
        data: {
            search: $("#search-note-input").val(),
            university: $("#search-note-unversity").val(),
            course: $("#search-note-course").val(),
            category: $("#search-note-category").val(),
            type: $("#search-note-type").val(),
            country: $("#search-note-country").val(),
        },
        success: function (result) {
            $("#render-partial-view").html('').html(result);
            //console.log("OK");
        }
    });
}



