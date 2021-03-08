$(function() {
  if($('.pagination-control').length) {
      $('.pagination-control').each(function() {
          
          //paginator element
          var pageControl = $(this);

          //element to be paginated
          var paginate = $(pageControl.attr('pagination-data'));

          //number of elements per page
          var elementsShown = parseInt(paginate.attr('data-tr-per-page'),10);

          //total number of elements
          var totalElements = paginate.children();

          //total number of pages
          var numPages = Math.ceil(totalElements.length/elementsShown); 

          //Max page shown
          var maxPage = parseInt(pageControl.attr('max-page-shown'));

          totalElements.hide();

          for(i=numPages;i>0;i--){

              if(i>maxPage){
                  pageControl.find('nav ul>li:first').after('<li style="display:none;" class="page-item"><a class="page-no page-link" page="'+i+'">'+i+'</a></li>');
              } else {

                  pageControl.find('nav ul>li:first').after('<li class="page-item"><a class="page-no page-link" page="'+i+'">'+i+'</a></li>');
              }
          }

          pageControl.find('.page-no').on('click',function() {

              pageControl.find('nav ul>li a').removeClass('active');
              $(this).addClass('active');

              var page = parseInt($(this).attr('page'));
              totalElements.hide(); 

              var showStart = (page-1)*elementsShown;

              totalElements.slice(showStart,((showStart+elementsShown)<totalElements.length) ? (showStart+elementsShown) : totalElements.length).fadeIn('slow');
          });


          pageControl.find('.page-no:first').trigger('click');

          pageControl.find('nav ul>li:first').on('click',function() {

              pageControl.find('nav ul>li a.active').parent().prev().find('a.page-no').trigger('click');

          });

          pageControl.find('nav ul>li:last').on('click',function() {

              pageControl.find('nav ul>li a.active').parent().next().find('a.page-no').trigger('click');

          });
      });
  }
});