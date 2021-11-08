$(function () {
    $('#sidebarCollapse').on('click', function() {
        $('#sidebar, #content').toggleClass('active');
    });
});

function toggleSidebar(ref) {
    document.getElementById("sidebar").classList.toggle('active');
}