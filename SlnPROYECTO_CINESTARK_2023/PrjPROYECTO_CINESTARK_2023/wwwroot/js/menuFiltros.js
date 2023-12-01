document.addEventListener('DOMContentLoaded', function () {
    var menuItems = document.querySelectorAll('.vertical-menu > li');

    menuItems.forEach(function (item) {
        item.addEventListener('click', function (e) {
            var subMenu = this.querySelector('.sub-menu');

            if (subMenu) {
                e.preventDefault();
                subMenu.classList.toggle('open');
            }
        });
    });
});
