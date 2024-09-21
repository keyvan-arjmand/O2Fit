const swiper1 = new Swiper('#swiper-1', {
    direction: 'horizontal',
    loop: true,
    breakpoints: {
        375: {
            slidesPerView: 3,
        },
        640: {
            slidesPerView: 3,
        },
        768: {
            slidesPerView: 3.5,
        },
        1280: {
            slidesPerView: 4,
        }
    },
    autoplay: {
        delay: 3000,
        disableOnInteraction: false,
    },
});
const swiper2 = new Swiper('#swiper-2', {
    direction: 'horizontal',
    loop: true,
    slidesPerView: 1.7,
    spaceBetween: 10,

    autoplay: {
        delay: 4000,
        disableOnInteraction: false,
    },
});
const swiper3 = new Swiper('.swiper-card', {
    direction: 'horizontal',
    loop: true,
    freeMode: true,
    breakpoints: {
        375: {
            slidesPerView: 1.3,
            spaceBetween: 4
        },
        640: {
            slidesPerView: 1.7,
            spaceBetween: 1
        },
        768: {
            slidesPerView: 1.7,
            spaceBetween: 1
        },
        993: {
            slidesPerView: 2.7,
            spaceBetween: 1
        },
        1280: {
            slidesPerView: 4.5,
        }
    },
    autoplay: {
        delay: 5000,
        disableOnInteraction: false,
    },
});


const swiperDetail = new Swiper('#swiperDetail', {
    direction: 'horizontal',
    loop: true,
    freeMode: true,
    breakpoints: {
        375: {
            slidesPerView: 1.3,
            spaceBetween: 4
        },
        640: {
            slidesPerView: 1.7,
            spaceBetween: 1
        },
        768: {
            slidesPerView: 1.7,
            spaceBetween: 1
        },
        993: {
            slidesPerView: 2.7,
            spaceBetween: 1
        },
        1280: {
            slidesPerView: 3.2,
            spaceBetween: 4
        }
    },
    autoplay: {
        delay: 5000,
        disableOnInteraction: false,
    },
});
function parseCookies() {
    let cookiesArray = [];
    let cookieArray = document.cookie.split('; ');
    cookieArray.forEach(function(cookie) {
        let [name, value] = cookie.split('=');
        value = decodeURIComponent(value);
        cookiesArray.push({ name: name, value: value });
    });
    return cookiesArray;
}