var burgerMenu = document.getElementById('burger-menu');
burgerMenu.addEventListener('click', function () {
    this.classList.toggle("close");
});

let mybutton = document.getElementById("myBtn");

// When the user scrolls down 20px from the top of the document, show the button
window.addEventListener("scroll", function () {
    scrollFunction();
    myFunction();
}, false);
function scrollFunction() {
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
        mybutton.style.display = "block";
    } else {
        mybutton.style.display = "none";
    }
}

// When the user clicks on the button, scroll to the top of the document
function topFunction() {
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
}

//header
var header = document.getElementById("sticky");
var nav = document.getElementById("navbar");
var sticky = header.offsetTop;
function myFunction() {
    if (window.pageYOffset > sticky) {
        nav.classList.add("sticky");
    } else {
        nav.classList.remove("sticky");
    }
}


const swiper1 = new Swiper('#swiper-1', {
    direction: 'horizontal',
    slidesPerView: 1, // Number of slides per view
    slidesPerColumn: 1, // Number of slides per column
    loop: true,
    autoplay: {
        delay: 3000, // Delay between slides in milliseconds
        disableOnInteraction: false, // Disable autoplay when user interacts with the swiper
    },

});
const swiper2 = new Swiper('#swiper-2', {
    direction: 'horizontal',
    slidesPerView: 2, // Number of slides per view
    slidesPerColumn: 2, // Number of slides per column
    loop: true,
    autoplay: {
        delay: 3000, // Delay between slides in milliseconds
        disableOnInteraction: false, // Disable autoplay when user interacts with the swiper
    },
});
const swiper3 = new Swiper('#swiper-3', {
    direction: 'horizontal',
    slidesPerView: 3, // Number of slides per view
    slidesPerColumn: 1, // Number of slides per column
    loop: true,
    autoplay: {
        delay: 3000, // Delay between slides in milliseconds
        disableOnInteraction: false, // Disable autoplay when user interacts with the swiper
    },
});
const swiper4 = new Swiper('#swiper-4', {
    direction: 'horizontal',
    slidesPerView: 2, // Number of slides per view
    slidesPerColumn: 2, // Number of slides per column
    loop: true,
    autoplay: {
        delay: 3000, // Delay between slides in milliseconds
        disableOnInteraction: false, // Disable autoplay when user interacts with the swiper
    },
    pagination: {
        el: '.swiper-pagination',
    },
});
const swiper6 = new Swiper('#swiper-6', {
    direction: 'horizontal',

    loop: true,
    autoplay: {
        delay: 3000, // Delay between slides in milliseconds
        disableOnInteraction: false, // Disable autoplay when user interacts with the swiper
    },
    pagination: {
        el: '.swiper-pagination',
    },
    breakpoints: {
        768: {
            slidesPerView: 2,
            spaceBetween: 20
        },
        1200:{
            slidesPerView: 4,
            spaceBetween: 15 
        }
}});
const swiper5 = new Swiper('#swiper-5', {
    direction: 'horizontal',
    slidesPerView: 1, // Number of slides per view
    slidesPerColumn: 1, // Number of slides per column
    loop: true,
    autoplay: {
        delay: 3000, // Delay between slides in milliseconds
        disableOnInteraction: false, // Disable autoplay when user interacts with the swiper
    },
    pagination: {
        el: '.swiper-pagination',
    },
});

new WOW().init();