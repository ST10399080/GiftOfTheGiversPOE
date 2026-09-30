
// Write your JavaScript code.
document.addEventListener("DOMContentLoaded", function () {

    const slides = document.querySelectorAll(".hero-slide");

    if (slides.length === 0) {
        return;
    }

    let currentSlide = 0;

    setInterval(function () {

        slides[currentSlide].classList.remove("active");

        currentSlide =
            (currentSlide + 1) % slides.length;

        slides[currentSlide].classList.add("active");

    }, 5000);

});